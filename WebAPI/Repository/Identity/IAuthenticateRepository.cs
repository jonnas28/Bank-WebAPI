using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebAPI.Models;

namespace WebAPI.Repository.Identity
{
    public interface IAuthenticateRepository
    {
        Task<JwtSecurityToken> Login(User user);
        string GenerateRefreshToken();
    }
}
