namespace BudgetService.API.DTOs
{
    public class IncomeDto
    {
        public string Source { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "ARS";
    }
}
