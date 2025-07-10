using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BudgetService.API.Models
{
    public class BudgetCategory
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid BudgetId { get; set; }

        [ForeignKey("BudgetId")]
        public Budget Budget { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string CategoryName { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
    }
}
