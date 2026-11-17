
using Microsoft.EntityFrameworkCore;
using PersonalExpenseTracker.Api.Data;
using PersonalExpenseTracker.Api.Models;
using PersonalExpenseTracker.Api.Repositories.Interfaces;

namespace PersonalExpenseTracker.Api.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly ApplicationDbContext _context;

        public BudgetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Budget?> GetByUserAndMonthAsync(
            string userId, int year, int month)
        {
            return await _context.Budgets.FirstOrDefaultAsync(b =>
                b.UserId == userId &&
                b.Year == year &&
                b.Month == month);
        }

        public async Task<Budget?> GetByIdAsync(int id, string userId)
        {
            return await _context.Budgets.FirstOrDefaultAsync(b =>
                b.Id == id && b.UserId == userId);
        }

        public async Task<Budget> AddAsync(Budget budget)
        {
            _context.Budgets.Add(budget);
            await _context.SaveChangesAsync();

            return budget;
        }

        public async Task<Budget> UpdateAsync(Budget budget)
        {
            await _context.SaveChangesAsync();

            return budget;
        }

        public async Task<decimal> GetTotalSpentAsync(
            string userId, DateTime startDate, DateTime endDate)
        {
            return await _context.Expenses
                .Where(e => e.UserId == userId &&
                            e.ExpenseDate >= startDate &&
                            e.ExpenseDate < endDate)
                .SumAsync(e => (decimal?)e.Amount) ?? 0;
        }
    }
}
