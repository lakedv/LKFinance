namespace AuthService.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(string name, string email, string password);
        Task<string?> LoginAsync(string email, string password);
    }
}
