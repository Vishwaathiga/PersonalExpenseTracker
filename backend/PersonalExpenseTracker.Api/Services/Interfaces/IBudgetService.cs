
using PersonalExpenseTracker.Api.DTOs.Budget;

namespace PersonalExpenseTracker.Api.Services.Interfaces
{
    public interface IBudgetService
    {
        Task<BudgetResponse?> GetMonthlyBudgetAsync(
            string userId, int year, int month);

        Task<BudgetResponse?> CreateAsync(
            string userId, CreateBudgetRequest request);

        Task<BudgetResponse?> UpdateAsync(
            int id, string userId, UpdateBudgetRequest request);
    }
}
