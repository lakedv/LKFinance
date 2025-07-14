using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetService.API.Models
{
    public class Budget
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [Range(1, 12)]
        public int Month { get; set; }

        [Required]
        [Range(2000, 2100)]
        public int Year { get; set; }

        public string? Notes { get; set; }

        public decimal? CurrentDebt { get; set; }
        public float? DebtPaymentPercentage { get; set; }

        public decimal? FixedExpenses {  get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public ICollection<BudgetCategory> Categories { get; set; } = new List<BudgetCategory>();

        public ICollection<Income> Incomes { get; set; } = new List<Income>();
    }
}
