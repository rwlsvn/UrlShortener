using AuthService.Controllers.Base;
using AuthService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    public class UserController : BaseController
    {
        private UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IdentityResult> Register(RegisterUserDto registerUserModel)
        {
            var appUser = new AppUser()
            {
                UserName = registerUserModel.UserName,
                Email = registerUserModel.Email    
            };
            var result = await _userManager.CreateAsync(appUser, registerUserModel.Password);
            return result;
        }

        [HttpGet("get/username/{id}")]
        public async Task<ActionResult<string>> GetUsername(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(new { Name = user.UserName});
        }

        [HttpPost("addrole")]
        [Authorize(Roles = "admin")]
        public async Task<IdentityResult> AddRole(RoleToUserDto addRoleToUser)
        {
            var user = await _userManager.FindByNameAsync(addRoleToUser.UserName);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError() 
                { 
                    Description = $"User {addRoleToUser.UserName} was not found." 
                });
            }
            var result = await _userManager.AddToRoleAsync(user, addRoleToUser.RoleName);

            return result;
        }

        [HttpPost("removerole")]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        public async Task<IdentityResult> RemoveRole(RoleToUserDto removeRoleToUser)
        {
            var user = await _userManager.FindByNameAsync(removeRoleToUser.UserName);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError() 
                { 
                    Description = $"User {removeRoleToUser.UserName} was not found." 
                });
            }
            var result = await _userManager.RemoveFromRoleAsync(user, removeRoleToUser.RoleName);

            return result;
        }
    }
}
