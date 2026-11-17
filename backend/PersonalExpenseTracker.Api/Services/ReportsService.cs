
using PersonalExpenseTracker.Api.DTOs.Reports;
using PersonalExpenseTracker.Api.Repositories.Interfaces;
using PersonalExpenseTracker.Api.Services.Interfaces;

namespace PersonalExpenseTracker.Api.Services
{
    public class ReportsService : IReportsService
    {
        private readonly IReportsRepository _reportsRepository;

        public ReportsService(IReportsRepository reportsRepository)
        {
            _reportsRepository = reportsRepository;
        }

        public async Task<MonthlyReportResponse> GetMonthlyReportAsync(
            string userId, int year, int month)
        {
            if (year < 2000 || year > DateTime.Today.Year + 1)
            {
                throw new ArgumentException("Invalid report year.");
            }

            if (month < 1 || month > 12)
            {
                throw new ArgumentException("Month must be between 1 and 12.");
            }

            return await _reportsRepository.GetMonthlyReportAsync(
                userId, year, month);
        }
    }
}
