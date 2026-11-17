
using System.ComponentModel.DataAnnotations;

namespace PersonalExpenseTracker.Api.DTOs.Profile
{
    public class UpdateProfileRequest
    {
        [Required]
        [MinLength(2)]
        public string FullName { get; set; } = string.Empty;

        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
