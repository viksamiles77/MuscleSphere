using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MuscleSphere.DomainModels.Entities;
using MuscleSphere.DTO.Authentication;
using MuscleSphere.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MuscleSphere.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var existingUserByUsername = await _userManager.FindByNameAsync(registerDto.Username);
            if (existingUserByUsername != null)
            {
                return new AuthResponseDto { Message = "Username is already taken." };
            }

            var existingUserByEmail = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUserByEmail != null)
            {
                return new AuthResponseDto { Message = "Email is already taken." };
            }

            var user = new User { UserName = registerDto.Username, Email = registerDto.Email };
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                return new AuthResponseDto { Message = "User created successfully!" };
            }

            return new AuthResponseDto { Message = string.Join(", ", result.Errors.Select(e => e.Description)) };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var isEmail = new EmailAddressAttribute().IsValid(loginDto.UsernameOrEmail);

            User user;

            if (isEmail)
            {
                user = await _userManager.FindByEmailAsync(loginDto.UsernameOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(loginDto.UsernameOrEmail);
            }

            if (user == null)
                return new AuthResponseDto { Message = "User not found." };

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (result.Succeeded)
            {
                var token = GenerateJwtToken(user);
                return new AuthResponseDto { Token = token, Message = "User logged in successfully!" };
            }

            return new AuthResponseDto { Message = "Invalid login attempt." };
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
