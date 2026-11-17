
using PersonalExpenseTracker.Api.Models;

namespace PersonalExpenseTracker.Api.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllByUserIdAsync(string userId);

        Task<Category?> GetByIdAsync(int id);

        Task<bool> ExistsByNameAsync(string name, string userId, int? excludeId = null);

        Task<Category> AddAsync(Category category);

        Task<Category> UpdateAsync(Category category);

        Task DeleteAsync(Category category);
    }
}
