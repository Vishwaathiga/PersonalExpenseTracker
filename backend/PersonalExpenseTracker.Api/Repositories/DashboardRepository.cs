
using Microsoft.EntityFrameworkCore;
using PersonalExpenseTracker.Api.Data;
using PersonalExpenseTracker.Api.DTOs.Dashboard;
using PersonalExpenseTracker.Api.Repositories.Interfaces;

namespace PersonalExpenseTracker.Api.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;

        public DashboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> GetTotalExpensesAsync(string userId)
        {
            return await _context.Expenses
                .Where(e => e.UserId == userId)
                .SumAsync(e => (decimal?)e.Amount) ?? 0;
        }

        public async Task<decimal> GetTodayExpensesAsync(
            string userId, DateTime startDate, DateTime endDate)
        {
            return await _context.Expenses
                .Where(e => e.UserId == userId &&
                            e.ExpenseDate >= startDate &&
                            e.ExpenseDate < endDate)
                .SumAsync(e => (decimal?)e.Amount) ?? 0;
        }

        public async Task<decimal> GetMonthlyExpensesAsync(
            string userId, DateTime startDate, DateTime endDate)
        {
            return await _context.Expenses
                .Where(e => e.UserId == userId &&
                            e.ExpenseDate >= startDate &&
                            e.ExpenseDate < endDate)
                .SumAsync(e => (decimal?)e.Amount) ?? 0;
        }

        public async Task<int> GetTotalTransactionsAsync(string userId)
        {
            return await _context.Expenses
                .CountAsync(e => e.UserId == userId);
        }

        public async Task<List<CategorySpendingResponse>> GetCategorySpendingAsync(
            string userId)
        {
            return await _context.Expenses
                .Where(e => e.UserId == userId)
                .GroupBy(e => e.Category != null
                    ? e.Category.Name
                    : "Uncategorized")
                .Select(group => new CategorySpendingResponse
                {
                    CategoryName = group.Key,
                    TotalAmount = group.Sum(e => e.Amount)
                })
                .OrderByDescending(item => item.TotalAmount)
                .ToListAsync();
        }

        public async Task<List<RecentExpenseResponse>> GetRecentExpensesAsync(
            string userId)
        {
            return await _context.Expenses
                .Include(e => e.Category)
                .Where(e => e.UserId == userId)
                .OrderByDescending(e => e.ExpenseDate)
                .ThenByDescending(e => e.Id)
                .Take(5)
                .Select(e => new RecentExpenseResponse
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
        }
    }
}
