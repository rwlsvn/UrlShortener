using AuthService.Configuration;
using AuthService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthService.Services
{
    public class TokenBuilderService : ITokenBuilderService
    {
        private TimeSpan _expirityDuration = TimeSpan.FromSeconds(20);

        private readonly SymmetricSecurityKey _key;
        private readonly string _issuer;

        private readonly UserManager<AppUser> _userManager;

        public TokenBuilderService(IOptions<JwtConfiguration> options, 
            UserManager<AppUser> userManager)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.Key));
            _issuer = options.Value.Issuer;

            _userManager = userManager;
        }

        public async Task<string> CreateToken(AppUser user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var jwt = new JwtSecurityToken(
                claims: claims,
                issuer: _issuer,
                expires: DateTime.UtcNow.Add(_expirityDuration),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public string CreateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal GetPrincipleFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _issuer,

                ValidateAudience = false,
                ValidateLifetime = false,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _key
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null)
                throw new SecurityTokenException("Invalid Token");
            return principal;
        }
    }
}
