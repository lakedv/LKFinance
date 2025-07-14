using BudgetService.API.Data;
using BudgetService.API.Models;
using BudgetService.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetService.API.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {
         private readonly BudgetDbContext _dbContext;

        public BudgetRepository(BudgetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Budget budget)
        {
           await _dbContext.Budgets.AddAsync(budget);
           await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid budgetId)
        {
            var budget = await _dbContext.Budgets.FindAsync(budgetId);
            if (budget != null) 
            {            
                _dbContext.Budgets.Remove(budget);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Budget?> GetByIdAsync(Guid budgetId)
        {
            return await _dbContext.Budgets
                .Include(b => b.Categories)
                .Include(b => b.Incomes)
                .FirstOrDefaultAsync(b => b.Id == budgetId);
        }

        public async Task<Budget?> GetByUserAndPeriodAsync(int month, int year)
        {
            return await _dbContext.Budgets
                .Include(b => b.Categories)
                .Include(b => b.Incomes)
                .FirstOrDefaultAsync(b => b.Month == month && b.Year == year);
        }

        public async Task UpdateAsync(Budget budget)
        {
            _dbContext.Budgets.Update(budget);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> BudgetExistsAsync(int month, int year)
        {
            return await _dbContext.Budgets
                .AnyAsync(b =>  b.Month == month && b.Year == year);
        }
    }
}
