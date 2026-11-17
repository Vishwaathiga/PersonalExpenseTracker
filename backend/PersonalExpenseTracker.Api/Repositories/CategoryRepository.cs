
using Microsoft.EntityFrameworkCore;
using PersonalExpenseTracker.Api.Data;
using PersonalExpenseTracker.Api.Models;
using PersonalExpenseTracker.Api.Repositories.Interfaces;

namespace PersonalExpenseTracker.Api.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllByUserIdAsync(string userId)
        {
            return await _context.Categories
                .Where(c => c.IsDefault || c.UserId == userId)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> ExistsByNameAsync(
            string name,
            string userId,
            int? excludeId = null)
        {
            string normalizedName = name.Trim().ToLower();

            return await _context.Categories.AnyAsync(c =>
                c.Id != excludeId &&
                c.Name.ToLower() == normalizedName &&
                (c.IsDefault || c.UserId == userId));
        }

        public async Task<Category> AddAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task DeleteAsync(Category category)
        {
            var expenses = await _context.Expenses
                .Where(e => e.CategoryId == category.Id &&
                            e.UserId == category.UserId)
                .ToListAsync();

            foreach (var expense in expenses)
            {
                expense.CategoryId = null;
            }

            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();
        }
    }
}
