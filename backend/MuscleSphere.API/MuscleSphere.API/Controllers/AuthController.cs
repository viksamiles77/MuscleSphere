using Microsoft.AspNetCore.Mvc;
using MuscleSphere.DTO.Authentication;
using MuscleSphere.Services.Interfaces;

namespace MuscleSphere.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var response = await _authService.RegisterAsync(registerDto);

            if (response.Message == "User created successfully!")
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var response = await _authService.LoginAsync(loginDto);

            if (response.Token != null)
            {
                return Ok(response);
            }

            return Unauthorized(response);
        }
    }
}