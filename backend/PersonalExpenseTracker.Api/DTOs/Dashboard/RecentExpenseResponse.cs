
namespace PersonalExpenseTracker.Api.DTOs.Dashboard
{
    public class RecentExpenseResponse
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public string? CategoryName { get; set; }

        public DateTime ExpenseDate { get; set; }
    }
}
