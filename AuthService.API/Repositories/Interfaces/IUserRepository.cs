using AuthService.API.Models;

namespace AuthService.API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(int id);
        Task AddAsync (User user);
        Task<bool> EmailExistsAsync(string  email);
    }
}
