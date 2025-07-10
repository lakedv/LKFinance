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
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        [Range(1, 12)]
        public int Month { get; set; }

        [Required]
        [Range(2000, 2100)]
        public int Year { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<BudgetCategory> Categories { get; set; } = new List<BudgetCategory>();
    }
}
