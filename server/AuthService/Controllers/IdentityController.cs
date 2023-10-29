using AuthService.Controllers.Base;
using AuthService.Models;
using AuthService.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AuthService.Controllers
{
    public class IdentityController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private ITokenBuilderService _tokenBuilder;

        public IdentityController(ITokenBuilderService tokenBuilder, 
            UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _tokenBuilder = tokenBuilder;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin loginUserModel)
        {
            var user = await _userManager.FindByEmailAsync(loginUserModel.Email);
            var result = user != null 
                && await _userManager.CheckPasswordAsync(user, loginUserModel.Password);
            if (result)
            {
                var token = await _tokenBuilder
                    .CreateToken(user);

                var refreshToken = _tokenBuilder.CreateRefreshToken();
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(1);
                await _userManager.UpdateAsync(user);

                return Ok(new TokenModel
                {
                    AccessToken = token,
                    RefreshToken = refreshToken
                });
            }
            return BadRequest(new { Message = "Invalid Login or Password" });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(TokenModel tokenDto)
        {
            if (tokenDto == null)
            {
                return BadRequest(new { Message = "Invalid Client Request" });
            }
            var principal = _tokenBuilder.GetPrincipleFromExpiredToken(tokenDto.AccessToken);
            var userId = principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _userManager.Users.FirstOrDefaultAsync(_ => _.Id == userId);

            if (user == null || user.RefreshToken != tokenDto.RefreshToken
                || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest(new { Message = "Invalid Client Request" });
            }
            var newJwtToken = await _tokenBuilder
                    .CreateToken(user);
            var newRefreshToken = _tokenBuilder.CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return Ok(new TokenModel
            {
                AccessToken = newJwtToken,
                RefreshToken = newRefreshToken
            });
        }
    }
}
