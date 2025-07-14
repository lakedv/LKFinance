using BudgetService.API.Models;

namespace BudgetService.API.Repositories.Interfaces
{
    public interface IBudgetRepository
    {
        Task AddAsync(Budget budget);
        Task UpdateAsync(Budget budget);
        Task DeleteAsync(Guid budgetId);
        Task<bool> BudgetExistsAsync(int month, int year);
        Task<Budget?> GetByIdAsync(Guid budgetId);
        Task<Budget?> GetByUserAndPeriodAsync(int month, int year);
    }
}
