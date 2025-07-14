namespace BudgetService.API.DTOs
{
    public class BudgetResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal? CurrentDebt { get; set; }
        public float? DebtPaymentPercentage { get; set; }
        public decimal? FixedExpenses { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ICollection<IncomeDto> Incomes { get; set; } = new List<IncomeDto>();
        public ICollection<BudgetCategoryDto> Categories { get; set; } = new List<BudgetCategoryDto>();
    }
}
