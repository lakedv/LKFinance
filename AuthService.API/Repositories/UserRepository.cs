using AuthService.API.Data;
using AuthService.API.Models;
using AuthService.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthService.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext _dbcontext;

        public UserRepository(AuthDbContext dbcontext) 
        { 
            _dbcontext = dbcontext;
        }

        public async Task AddAsync(User user)
        {
            _dbcontext.Users.Add(user);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _dbcontext.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbcontext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _dbcontext.Users.FindAsync(id);
        }
    }
}
