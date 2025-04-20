namespace ScentSation.Server.Controllers.hazem.DTOs
{
    public class LoginDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
    public class RegisterDto
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? PhoneNumber { get; set; }
    }
    namespace ScentSation.Server.Controllers.hazem.DTOs
    {
        public class UpdateUserDto
        {
            public string FullName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string ProfileImageUrl { get; set; }
        }
    }

}
