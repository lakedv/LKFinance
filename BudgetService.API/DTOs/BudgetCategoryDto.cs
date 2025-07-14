namespace BudgetService.API.DTOs
{
    public class BudgetCategoryDto
    {
        public string CategoryName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
