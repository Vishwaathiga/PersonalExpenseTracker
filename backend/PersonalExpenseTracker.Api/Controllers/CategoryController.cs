
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalExpenseTracker.Api.DTOs.Category;
using PersonalExpenseTracker.Api.Services.Interfaces;

namespace PersonalExpenseTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync(GetUserId());

            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryRequest request)
        {
            var category = await _categoryService.CreateAsync(GetUserId(), request);

            if (category == null)
            {
                return BadRequest(new { message = "Category name already exists." });
            }

            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateCategoryRequest request)
        {
            var category = await _categoryService.UpdateAsync(
                id, GetUserId(), request);

            if (category == null)
            {
                return BadRequest(new
                {
                    message = "Category not found, cannot be modified, or name already exists."
                });
            }

            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _categoryService.DeleteAsync(id, GetUserId());

            if (!deleted)
            {
                return BadRequest(new
                {
                    message = "Category not found or cannot be deleted."
                });
            }

            return Ok(new { message = "Category deleted successfully." });
        }
    }
}
