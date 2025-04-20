using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScentSation.Server.Controllers.hazem.DTOs;
using ScentSation.Server.Controllers.hazem.DTOs.ScentSation.Server.Controllers.hazem.DTOs;
using ScentSation.Server.Models;
using System.Threading.Tasks;

namespace ScentSation.Server.Controllers.hazem
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersController(MyDbContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _httpContextAccessor = accessor;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (_context.Users.Any(u => u.Email == dto.Email))
                return BadRequest("Email already exists.");

            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = dto.Password, // بدون تشفير
                PhoneNumber = dto.PhoneNumber,
                UserRole = "User",
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _httpContextAccessor.HttpContext!.Session.SetInt32("userId", user.UserId);
            _httpContextAccessor.HttpContext.Session.SetString("role", user.UserRole);

            return Ok(new { message = "Registration successful", userId = user.UserId });
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);
            if (user == null || user.PasswordHash != dto.Password)
                return Unauthorized("Invalid credentials.");

            _httpContextAccessor.HttpContext!.Session.SetInt32("userId", user.UserId);
            _httpContextAccessor.HttpContext.Session.SetString("role", user.UserRole);

            return Ok(new { message = "Login successful", userId = user.UserId, role = user.UserRole });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            _httpContextAccessor.HttpContext!.Session.Clear();
            return Ok(new { message = "Logged out successfully" });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null)
                return NotFound("User not found.");

            return Ok(new { message = "Use reset-password endpoint to set a new password." });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null)
                return NotFound("User not found.");

            user.PasswordHash = dto.NewPassword;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Password reset successful." });
        }

        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            var userId = _httpContextAccessor.HttpContext!.Session.GetInt32("userId");
            if (userId == null)
                return Unauthorized();

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound();

            return Ok(new
            {
                user.UserId,
                user.FullName,
                user.Email,
                user.PhoneNumber,
                user.ProfileImageUrl,
                user.UserRole
            });
        }
        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            return Ok(new
            {
                user.UserId,
                user.FullName,
                user.Email,
                user.PhoneNumber,
                user.ProfileImageUrl,
                user.UserRole
            });
        }
        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            user.FullName = dto.FullName;
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            user.ProfileImageUrl = dto.ProfileImageUrl;

            await _context.SaveChangesAsync();

            return Ok(new { message = "User updated successfully" });
        }

    }
}
