using Microsoft.EntityFrameworkCore;
using PersonalExpenseTracker.Api.Data;
using PersonalExpenseTracker.Api.Models;
using PersonalExpenseTracker.Api.Repositories.Interfaces;

namespace PersonalExpenseTracker.Api.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ApplicationDbContext _context;

        public ExpenseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Expense> AddAsync(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return expense;
        }

        public async Task<List<Expense>> GetAllByUserIdAsync(string userId)
        {
            return await _context.Expenses
     .Include(e => e.Category)
     .Where(e => e.UserId == userId)
     .OrderByDescending(e => e.ExpenseDate)
     .ToListAsync();
        }

        public async Task<Expense?> GetByIdAsync(int id, string userId)
        {
            return await _context.Expenses
    .Include(e => e.Category)
    .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
        }

        public async Task<Expense> UpdateAsync(Expense expense)
        {
            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();

            return expense;
        }

        public async Task DeleteAsync(Expense expense)
        {
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
        }
    }
}