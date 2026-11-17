using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalExpenseTracker.Api.DTOs.Expense;
using PersonalExpenseTracker.Api.Services.Interfaces;

namespace PersonalExpenseTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense(CreateExpenseRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var expense = await _expenseService.CreateAsync(userId, request);

            return CreatedAtAction(
                nameof(GetExpenseById),
                new { id = expense.Id },
                expense);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExpenses()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var expenses = await _expenseService.GetAllAsync(userId);

            return Ok(expenses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpenseById(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var expense = await _expenseService.GetByIdAsync(id, userId);

            if (expense == null)
            {
                return NotFound(new { message = "Expense not found." });
            }

            return Ok(expense);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpense(
            int id, UpdateExpenseRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var expense = await _expenseService.UpdateAsync(id, userId, request);

            if (expense == null)
            {
                return NotFound(new { message = "Expense not found." });
            }

            return Ok(new
            {
                message = "Expense updated successfully.",
                expense
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var deleted = await _expenseService.DeleteAsync(id, userId);

            if (!deleted)
            {
                return NotFound(new { message = "Expense not found." });
            }

            return Ok(new { message = "Expense deleted successfully." });
        }
    }
}