
using Microsoft.AspNetCore.Mvc;
using PersonalExpenseTracker.Api.DTOs.Auth;
using PersonalExpenseTracker.Api.Services.Interfaces;

namespace PersonalExpenseTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);

            if (result != "Registration successful.")
            {
                return BadRequest(new { message = result });
            }

            return Ok(new { message = result });
        }
    }
}
