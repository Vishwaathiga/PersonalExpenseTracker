
namespace PersonalExpenseTracker.Api.DTOs.Dashboard
{
    public class DashboardResponse
    {
        public decimal TotalExpenses { get; set; }

        public decimal TodayExpenses { get; set; }

        public decimal MonthlyExpenses { get; set; }

        public int TotalTransactions { get; set; }

        public List<CategorySpendingResponse> CategorySpending { get; set; } = new();

        public List<RecentExpenseResponse> RecentExpenses { get; set; } = new();
    }
}
