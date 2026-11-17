
using Microsoft.EntityFrameworkCore;
using PersonalExpenseTracker.Api.Data;
using PersonalExpenseTracker.Api.DTOs.Reports;
using PersonalExpenseTracker.Api.Repositories.Interfaces;

namespace PersonalExpenseTracker.Api.Repositories
{
    public class ReportsRepository : IReportsRepository
    {
        private readonly ApplicationDbContext _context;

        public ReportsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MonthlyReportResponse> GetMonthlyReportAsync(
            string userId, int year, int month)
        {
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1);

            var monthlyExpenses = _context.Expenses
                .Where(e => e.UserId == userId &&
                            e.ExpenseDate >= startDate &&
                            e.ExpenseDate < endDate);

            var totalExpenses = await monthlyExpenses
                .SumAsync(e => (decimal?)e.Amount) ?? 0;

            var totalTransactions = await monthlyExpenses.CountAsync();

            var categoryBreakdown = await monthlyExpenses
                .GroupBy(e => e.Category != null
                    ? e.Category.Name
                    : "Uncategorized")
                .Select(group => new CategoryReportResponse
                {
                    CategoryName = group.Key,
                    TotalAmount = group.Sum(e => e.Amount),
                    TransactionCount = group.Count()
                })
                .OrderByDescending(item => item.TotalAmount)
                .ToListAsync();

            var expenses = await monthlyExpenses
                .Include(e => e.Category)
                .OrderByDescending(e => e.ExpenseDate)
                .ThenByDescending(e => e.Id)
                .Select(e => new ReportExpenseResponse
                {
                    Id = e.Id,
                    Title = e.Title,
                    Amount = e.Amount,
                    CategoryName = e.Category != null
                        ? e.Category.Name
                        : null,
                    ExpenseDate = e.ExpenseDate
                })
                .ToListAsync();

            return new MonthlyReportResponse
            {
                Year = year,
                Month = month,
                TotalExpenses = totalExpenses,
                TotalTransactions = totalTransactions,
                CategoryBreakdown = categoryBreakdown,
                Expenses = expenses
            };
        }
    }
}
