
using PersonalExpenseTracker.Api.DTOs.Dashboard;

namespace PersonalExpenseTracker.Api.Repositories.Interfaces
{
    public interface IDashboardRepository
    {
        Task<decimal> GetTotalExpensesAsync(string userId);

        Task<decimal> GetTodayExpensesAsync(
            string userId, DateTime startDate, DateTime endDate);

        Task<decimal> GetMonthlyExpensesAsync(
            string userId, DateTime startDate, DateTime endDate);

        Task<int> GetTotalTransactionsAsync(string userId);

        Task<List<CategorySpendingResponse>> GetCategorySpendingAsync(string userId);

        Task<List<RecentExpenseResponse>> GetRecentExpensesAsync(string userId);
    }
}
