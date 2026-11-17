
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PersonalExpenseTracker.Api.DTOs.Auth;
using PersonalExpenseTracker.Api.Models;
using PersonalExpenseTracker.Api.Services.Interfaces;

namespace PersonalExpenseTracker.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
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

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            // Find user using email
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return null;
            }

            // Verify password
            var validPassword = await _userManager.CheckPasswordAsync(
                user,
                request.Password);

            if (!validPassword)
            {
                return null;
            }

            // Read JWT configuration
            var jwtKey = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            if (string.IsNullOrWhiteSpace(jwtKey) ||
                string.IsNullOrWhiteSpace(issuer) ||
                string.IsNullOrWhiteSpace(audience))
            {
                throw new InvalidOperationException(
                    "JWT configuration is missing.");
            }

            // Set token expiration
            var expiresAt = DateTime.UtcNow.AddHours(1);

            // Add user information to token
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName ?? user.Email ?? ""),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim("FullName", user.FullName)
            };

            // Create signing key from Base64 secret
            var keyBytes = Convert.FromBase64String(jwtKey);
            var key = new SymmetricSecurityKey(keyBytes);

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            // Generate JWT token
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials);

            return new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAt = expiresAt,
                FullName = user.FullName,
                Email = user.Email ?? ""
            };
        }
    }
}
