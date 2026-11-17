
using PersonalExpenseTracker.Api.DTOs.Budget;
using PersonalExpenseTracker.Api.Models;
using PersonalExpenseTracker.Api.Repositories.Interfaces;
using PersonalExpenseTracker.Api.Services.Interfaces;

namespace PersonalExpenseTracker.Api.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly IBudgetRepository _budgetRepository;

        public BudgetService(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public async Task<BudgetResponse?> GetMonthlyBudgetAsync(
            string userId, int year, int month)
        {
            var budget = await _budgetRepository.GetByUserAndMonthAsync(
                userId, year, month);

            if (budget == null)
            {
                return null;
            }

            return await MapToResponseAsync(budget);
        }

        public async Task<BudgetResponse?> CreateAsync(
            string userId, CreateBudgetRequest request)
        {
            var existingBudget = await _budgetRepository.GetByUserAndMonthAsync(
                userId, request.Year, request.Month);

            if (existingBudget != null)
            {
                return null;
            }

            var budget = new Budget
            {
                Amount = request.Amount,
                Year = request.Year,
                Month = request.Month,
                UserId = userId
            };

            var createdBudget = await _budgetRepository.AddAsync(budget);

            return await MapToResponseAsync(createdBudget);
        }

        public async Task<BudgetResponse?> UpdateAsync(
            int id, string userId, UpdateBudgetRequest request)
        {
            var budget = await _budgetRepository.GetByIdAsync(id, userId);

            if (budget == null)
            {
                return null;
            }

            budget.Amount = request.Amount;

            var updatedBudget = await _budgetRepository.UpdateAsync(budget);

            return await MapToResponseAsync(updatedBudget);
        }

        private async Task<BudgetResponse> MapToResponseAsync(Budget budget)
        {
            DateTime startDate = new DateTime(
                budget.Year, budget.Month, 1);

            DateTime endDate = startDate.AddMonths(1);

            decimal totalSpent = await _budgetRepository.GetTotalSpentAsync(
                budget.UserId, startDate, endDate);

            decimal remainingAmount = budget.Amount - totalSpent;

            decimal percentageUsed = budget.Amount > 0
                ? (totalSpent / budget.Amount) * 100
                : 0;

            return new BudgetResponse
            {
                Id = budget.Id,
                Amount = budget.Amount,
                Year = budget.Year,
                Month = budget.Month,
                TotalSpent = totalSpent,
                RemainingAmount = remainingAmount,
                PercentageUsed = Math.Round(percentageUsed, 2),
                IsExceeded = totalSpent > budget.Amount
            };
        }
    }
}
