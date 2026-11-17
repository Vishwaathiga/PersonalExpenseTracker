using PersonalExpenseTracker.Api.Models;

namespace PersonalExpenseTracker.Api.Repositories.Interfaces
{
    public interface IExpenseRepository
    {
        Task<Expense> AddAsync(Expense expense);

        Task<List<Expense>> GetAllByUserIdAsync(string userId);

        Task<Expense?> GetByIdAsync(int id, string userId);

        Task<Expense> UpdateAsync(Expense expense);

        Task DeleteAsync(Expense expense);
    }
}