
using PersonalExpenseTracker.Api.DTOs.Expense;
using PersonalExpenseTracker.Api.Models;
using PersonalExpenseTracker.Api.Repositories.Interfaces;
using PersonalExpenseTracker.Api.Services.Interfaces;

namespace PersonalExpenseTracker.Api.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<ExpenseResponse> CreateAsync(
            string userId, CreateExpenseRequest request)
        {
            var expense = new Expense
            {
                Title = request.Title.Trim(),
                Amount = request.Amount,
                Description = request.Description,
                ExpenseDate = request.ExpenseDate,
                CategoryId = request.CategoryId,
                UserId = userId
            };

            var createdExpense = await _expenseRepository.AddAsync(expense);

            return MapToResponse(createdExpense);
        }

        public async Task<List<ExpenseResponse>> GetAllAsync(string userId)
        {
            var expenses = await _expenseRepository.GetAllByUserIdAsync(userId);

            return expenses.Select(MapToResponse).ToList();
        }

        public async Task<ExpenseResponse?> GetByIdAsync(int id, string userId)
        {
            var expense = await _expenseRepository.GetByIdAsync(id, userId);

            if (expense == null)
            {
                return null;
            }

            return MapToResponse(expense);
        }

        public async Task<ExpenseResponse?> UpdateAsync(
            int id, string userId, UpdateExpenseRequest request)
        {
            var expense = await _expenseRepository.GetByIdAsync(id, userId);

            if (expense == null)
            {
                return null;
            }

            expense.Title = request.Title.Trim();
            expense.Amount = request.Amount;
            expense.Description = request.Description;
            expense.ExpenseDate = request.ExpenseDate;
            expense.CategoryId = request.CategoryId;

            var updatedExpense = await _expenseRepository.UpdateAsync(expense);

            return MapToResponse(updatedExpense);
        }

        public async Task<bool> DeleteAsync(int id, string userId)
        {
            var expense = await _expenseRepository.GetByIdAsync(id, userId);

            if (expense == null)
            {
                return false;
            }

            await _expenseRepository.DeleteAsync(expense);

            return true;
        }

        private static ExpenseResponse MapToResponse(Expense expense)
        {
            return new ExpenseResponse
            {
                Id = expense.Id,
                Title = expense.Title,
                Amount = expense.Amount,
                Description = expense.Description,
                ExpenseDate = expense.ExpenseDate,
                CategoryId = expense.CategoryId,
                CategoryName = expense.Category?.Name
            };
        }
    }
}
