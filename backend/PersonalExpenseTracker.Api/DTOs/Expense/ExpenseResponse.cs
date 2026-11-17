namespace PersonalExpenseTracker.Api.DTOs.Expense
{
    public class ExpenseResponse
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public string? Description { get; set; }

        public DateTime ExpenseDate { get; set; }

        public int? CategoryId { get; set; }
    }
}