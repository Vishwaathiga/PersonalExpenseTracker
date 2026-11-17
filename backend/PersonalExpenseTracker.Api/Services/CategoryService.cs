
using PersonalExpenseTracker.Api.DTOs.Category;
using PersonalExpenseTracker.Api.Models;
using PersonalExpenseTracker.Api.Repositories.Interfaces;
using PersonalExpenseTracker.Api.Services.Interfaces;

namespace PersonalExpenseTracker.Api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryResponse>> GetAllAsync(string userId)
        {
            var categories = await _categoryRepository.GetAllByUserIdAsync(userId);

            return categories.Select(c => new CategoryResponse
            {
                Id = c.Id,
                Name = c.Name,
                IsDefault = c.IsDefault
            }).ToList();
        }

        public async Task<CategoryResponse?> CreateAsync(
            string userId,
            CreateCategoryRequest request)
        {
            string name = request.Name.Trim();

            if (await _categoryRepository.ExistsByNameAsync(name, userId))
            {
                return null;
            }

            var category = new Category
            {
                Name = name,
                IsDefault = false,
                UserId = userId
            };

            var savedCategory = await _categoryRepository.AddAsync(category);

            return new CategoryResponse
            {
                Id = savedCategory.Id,
                Name = savedCategory.Name,
                IsDefault = savedCategory.IsDefault
            };
        }

        public async Task<CategoryResponse?> UpdateAsync(
            int id,
            string userId,
            UpdateCategoryRequest request)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null || category.IsDefault || category.UserId != userId)
            {
                return null;
            }

            string name = request.Name.Trim();

            if (await _categoryRepository.ExistsByNameAsync(name, userId, id))
            {
                return null;
            }

            category.Name = name;

            var updatedCategory = await _categoryRepository.UpdateAsync(category);

            return new CategoryResponse
            {
                Id = updatedCategory.Id,
                Name = updatedCategory.Name,
                IsDefault = updatedCategory.IsDefault
            };
        }

        public async Task<bool> DeleteAsync(int id, string userId)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null || category.IsDefault || category.UserId != userId)
            {
                return false;
            }

            await _categoryRepository.DeleteAsync(category);

            return true;
        }
    }
}
