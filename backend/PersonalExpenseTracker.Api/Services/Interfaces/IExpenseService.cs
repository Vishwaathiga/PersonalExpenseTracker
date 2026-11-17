using PersonalExpenseTracker.Api.DTOs.Expense;

namespace PersonalExpenseTracker.Api.Services.Interfaces
{
    public interface IExpenseService
    {
        Task<ExpenseResponse> CreateAsync(string userId, CreateExpenseRequest request);

        Task<List<ExpenseResponse>> GetAllAsync(string userId);

        Task<ExpenseResponse?> GetByIdAsync(int id, string userId);

        Task<ExpenseResponse?> UpdateAsync(
            int id, string userId, UpdateExpenseRequest request);

        Task<bool> DeleteAsync(int id, string userId);
    }
}