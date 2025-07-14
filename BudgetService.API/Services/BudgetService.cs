using BudgetService.API.DTOs;
using BudgetService.API.Models;
using BudgetService.API.Exceptions;
using BudgetService.API.Repositories.Interfaces;
using BudgetService.API.Services.Interfaces;
using AutoMapper;
using System.Security.Claims;

namespace BudgetService.API.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;

        public BudgetService(IBudgetRepository budgetRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        { 
           _budgetRepository = budgetRepository;
           _mapper = mapper;
           _contextAccessor = httpContextAccessor;
        }

        public async Task<BudgetResponse> CreateBudgetAsync(BudgetCreateRequest request, ClaimsPrincipal user)
        {
            //Validacion periodo
            if (await _budgetRepository.BudgetExistsAsync(request.Month, request.Year))
                throw new ConflictException("Ya existe un presupuesto registrado en este periodo.");

            //Validacion Ingresos
            var allowedCurrencies = new[] { "ARS", "USD", "EUR" };
            foreach (var income in request.Incomes)
            {
                if (income.Amount <= 0)
                    throw new ValidationException($"El Ingreso '{income.Source}' debe ser mayor a 0.");

                if (!allowedCurrencies.Contains(income.Currency.ToUpper()))
                    throw new ValidationException($"La moneda '{income.Currency}' no es valida.");
            }
            
            //Validacion de deuda
            if (request.CurrentDebt is null && request.DebtPaymentPercentage is not null)
                throw new ValidationException("No se puede definir porcentaje de pago si no hay una deuda.");

            if (request.DebtPaymentPercentage is not null &&
               (request.DebtPaymentPercentage < 0 || request.DebtPaymentPercentage > 100))
               throw new ValidationException("El porcentaje de pago de deuda debe ser de entre 0 y 100.");
            
            //Validacion de Categorias Unicas
            if (request.Categories != null)
            {
                var duplicatedNames = request.Categories
                    .GroupBy(c => c.CategoryName.Trim().ToLower())
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key);

                if (duplicatedNames.Any())
                    throw new ValidationException("Hay categorias duplicadas:" + string.Join(", ", duplicatedNames));
            }

            //Validacion Gastos fijos
            if (request.FixedExpenses is not null && request.FixedExpenses < 0)
                throw new ValidationException("Los gastos fijos no pueden ser negativos.");

            //Validacion Token
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                throw new UnauthorizedAccessException("Usuario invalido, inicie sesion para continuar.");

            var userId = Guid.Parse(userIdClaim.Value);
            var budget = _mapper.Map<Budget>(request);
            budget.UserId = userId;
            await _budgetRepository.AddAsync(budget);
            return _mapper.Map<BudgetResponse>(budget);
        }

        public async Task<bool> DeleteBudgetAsync(Guid budgetId)
        {
            var existing = await _budgetRepository.GetByIdAsync(budgetId);
            if (existing == null) return false;

            await _budgetRepository.DeleteAsync(budgetId);
            return true;
        }

        public async Task<Budget?> GetBudgetAsync(int month, int year)
        {
            return await _budgetRepository.GetByUserAndPeriodAsync(month, year);
        }

        public async Task<Budget?> UpdateBudgetAsync(Budget updatedBudget)
        {
           await _budgetRepository.UpdateAsync(updatedBudget);
           return updatedBudget;
        }
    }
}
