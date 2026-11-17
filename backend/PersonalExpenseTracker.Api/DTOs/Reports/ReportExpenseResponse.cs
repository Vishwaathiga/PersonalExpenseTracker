
namespace PersonalExpenseTracker.Api.DTOs.Reports
{
    public class ReportExpenseResponse
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public string? CategoryName { get; set; }

        public DateTime ExpenseDate { get; set; }
    }
}
