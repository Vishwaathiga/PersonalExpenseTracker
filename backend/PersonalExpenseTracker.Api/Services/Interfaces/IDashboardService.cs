
using PersonalExpenseTracker.Api.DTOs.Dashboard;

namespace PersonalExpenseTracker.Api.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardResponse> GetDashboardAsync(string userId);
    }
}
