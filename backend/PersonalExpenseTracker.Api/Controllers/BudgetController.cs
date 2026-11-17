
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalExpenseTracker.Api.DTOs.Budget;
using PersonalExpenseTracker.Api.Services.Interfaces;

namespace PersonalExpenseTracker.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMonthlyBudget(
            [FromQuery] int year, [FromQuery] int month)
        {
            if (year < 2000 || year > 2100)
            {
                return BadRequest(new { message = "Invalid year." });
            }

            if (month < 1 || month > 12)
            {
                return BadRequest(new { message = "Month must be between 1 and 12." });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var budget = await _budgetService.GetMonthlyBudgetAsync(
                userId, year, month);

            if (budget == null)
            {
                return NotFound(new { message = "No budget found for this month." });
            }

            return Ok(budget);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBudget(
            CreateBudgetRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var budget = await _budgetService.CreateAsync(userId, request);

            if (budget == null)
            {
                return Conflict(new
                {
                    message = "A budget already exists for this month."
                });
            }

            return Ok(budget);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBudget(
            int id, UpdateBudgetRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var budget = await _budgetService.UpdateAsync(
                id, userId, request);

            if (budget == null)
            {
                return NotFound(new { message = "Budget not found." });
            }

            return Ok(budget);
        }
    }
}
