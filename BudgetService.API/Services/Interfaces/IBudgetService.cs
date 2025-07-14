using BudgetService.API.DTOs;
using BudgetService.API.Models;
using System.Security.Claims;

namespace BudgetService.API.Services.Interfaces
{
    public interface IBudgetService
    {
        Task<BudgetResponse> CreateBudgetAsync(BudgetCreateRequest request, ClaimsPrincipal user);
        Task<Budget?> GetBudgetAsync(int month, int year);
        Task<bool> DeleteBudgetAsync(Guid budgetId);
        Task<Budget?> UpdateBudgetAsync(Budget updatedBudget);
    }
}
