
namespace PersonalExpenseTracker.Api.DTOs.Reports
{
    public class CategoryReportResponse
    {
        public string CategoryName { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }

        public int TransactionCount { get; set; }
    }
}
