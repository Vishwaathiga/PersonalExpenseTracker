
using PersonalExpenseTracker.Api.DTOs.Reports;

namespace PersonalExpenseTracker.Api.Repositories.Interfaces
{
    public interface IReportsRepository
    {
        Task<MonthlyReportResponse> GetMonthlyReportAsync(
            string userId, int year, int month);
    }
}
