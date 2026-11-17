
using PersonalExpenseTracker.Api.DTOs.Reports;

namespace PersonalExpenseTracker.Api.Services.Interfaces
{
    public interface IReportsService
    {
        Task<MonthlyReportResponse> GetMonthlyReportAsync(
            string userId, int year, int month);
    }
}
