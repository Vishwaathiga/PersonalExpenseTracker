using System.ComponentModel.DataAnnotations;

namespace PersonalExpenseTracker.Api.DTOs.Expense
{
    public class UpdateExpenseRequest
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Range(0.01, 999999999)]
        public decimal Amount { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime ExpenseDate { get; set; }

        public int? CategoryId { get; set; }
    }
}