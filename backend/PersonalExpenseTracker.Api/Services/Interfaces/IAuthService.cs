
using PersonalExpenseTracker.Api.DTOs.Auth;

namespace PersonalExpenseTracker.Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterRequest request);
    }
}
