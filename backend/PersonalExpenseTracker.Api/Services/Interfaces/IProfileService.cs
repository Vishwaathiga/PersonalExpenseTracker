
using PersonalExpenseTracker.Api.DTOs.Profile;

namespace PersonalExpenseTracker.Api.Services.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileResponse?> GetProfileAsync(string userId);

        Task<ProfileResponse?> UpdateProfileAsync(
            string userId,
            UpdateProfileRequest request);
    }
}
