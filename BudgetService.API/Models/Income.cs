using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetService.API.Models
{
    public class Income
    {
        public int Id { get; set; }
        public Guid BudgetId { get; set; }
        public Budget Budget { get; set; } = null!;

        [Required]
        public string Source { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public string Currency { get; set; } = "ARS";
    }
}
