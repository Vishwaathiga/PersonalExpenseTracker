
using System.ComponentModel.DataAnnotations;

namespace PersonalExpenseTracker.Api.Models
{
    public class Expense
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public decimal Amount { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime ExpenseDate { get; set; }

        public int? CategoryId { get; set; }

        public Category? Category { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public ApplicationUser? User { get; set; }
    }
}
