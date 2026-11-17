
using PersonalExpenseTracker.Api.DTOs.Category;

namespace PersonalExpenseTracker.Api.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllAsync(string userId);

        Task<CategoryResponse?> CreateAsync(
            string userId,
            CreateCategoryRequest request);

        Task<CategoryResponse?> UpdateAsync(
            int id,
            string userId,
            UpdateCategoryRequest request);

        Task<bool> DeleteAsync(int id, string userId);
    }
}
