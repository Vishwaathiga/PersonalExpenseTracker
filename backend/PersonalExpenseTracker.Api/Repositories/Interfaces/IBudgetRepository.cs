
using PersonalExpenseTracker.Api.Models;

namespace PersonalExpenseTracker.Api.Repositories.Interfaces
{
    public interface IBudgetRepository
    {
        Task<Budget?> GetByUserAndMonthAsync(
            string userId, int year, int month);

        Task<Budget?> GetByIdAsync(int id, string userId);

        Task<Budget> AddAsync(Budget budget);

        Task<Budget> UpdateAsync(Budget budget);

        Task<decimal> GetTotalSpentAsync(
            string userId, DateTime startDate, DateTime endDate);
    }
}
