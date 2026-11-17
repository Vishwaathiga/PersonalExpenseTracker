
namespace PersonalExpenseTracker.Api.DTOs.Reports
{
    public class MonthlyReportResponse
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public decimal TotalExpenses { get; set; }

        public int TotalTransactions { get; set; }

        public List<CategoryReportResponse> CategoryBreakdown { get; set; } = new();

        public List<ReportExpenseResponse> Expenses { get; set; } = new();
    }
}
