
using Microsoft.AspNetCore.Identity;
using PersonalExpenseTracker.Api.DTOs.Auth;
using PersonalExpenseTracker.Api.Models;
using PersonalExpenseTracker.Api.Services.Interfaces;

namespace PersonalExpenseTracker.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> RegisterAsync(RegisterRequest request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
            {
                return "Email already registered.";
            }

            var user = new ApplicationUser
            {
                FullName = request.FullName,
                Email = request.Email,
                UserName = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return string.Join(" ", result.Errors.Select(error => error.Description));
            }

            return "Registration successful.";
        }
    }
}
