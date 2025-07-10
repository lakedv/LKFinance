using AuthService.API.DTOs;

namespace AuthService.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(UserRegisterRequest request);
        Task<string?> LoginUserAsync(UserLoginRequest request);
    }
}
