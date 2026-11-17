
using PersonalExpenseTracker.Api.DTOs.Dashboard;
using PersonalExpenseTracker.Api.Repositories.Interfaces;
using PersonalExpenseTracker.Api.Services.Interfaces;

namespace PersonalExpenseTracker.Api.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<DashboardResponse> GetDashboardAsync(string userId)
        {
            DateTime today = DateTime.Today;
            DateTime tomorrow = today.AddDays(1);

            DateTime monthStart = new DateTime(
                today.Year, today.Month, 1);

            DateTime nextMonth = monthStart.AddMonths(1);

            var response = new DashboardResponse
            {
                TotalExpenses =
                    await _dashboardRepository.GetTotalExpensesAsync(userId),

                TodayExpenses =
                    await _dashboardRepository.GetTodayExpensesAsync(
                        userId, today, tomorrow),

                MonthlyExpenses =
                    await _dashboardRepository.GetMonthlyExpensesAsync(
                        userId, monthStart, nextMonth),

                TotalTransactions =
                    await _dashboardRepository.GetTotalTransactionsAsync(userId),

                CategorySpending =
                    await _dashboardRepository.GetCategorySpendingAsync(userId),

                RecentExpenses =
                    await _dashboardRepository.GetRecentExpensesAsync(userId)
            };

            return response;
        }
    }
}
