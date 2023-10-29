using AuthService.Models;
using System.Security.Claims;

namespace AuthService.Services
{
    public interface ITokenBuilderService
    {
        Task<string> CreateToken(AppUser user);
        ClaimsPrincipal GetPrincipleFromExpiredToken(string token);
        string CreateRefreshToken();
    }
}
