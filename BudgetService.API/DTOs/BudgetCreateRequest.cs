
namespace BudgetService.API.DTOs
{
    public class BudgetCreateRequest
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal? CurrentDebt { get; set; }
        public float? DebtPaymentPercentage { get; set; }
        public decimal? FixedExpenses { get; set; }
        public string? Notes { get; set; }

        public ICollection<IncomeDto> Incomes { get; set; } = new List<IncomeDto>();
        public ICollection<BudgetCategoryDto> Categories { get; set; } = new List<BudgetCategoryDto>();
    }
}
