using AuthService.API.Models;

namespace AuthService.API.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user, JwtOptions options);
    }
}
