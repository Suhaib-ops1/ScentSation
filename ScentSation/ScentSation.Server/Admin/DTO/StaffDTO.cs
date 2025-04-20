namespace ScentSation.Server.Admin.DTO
{
    public class StaffDTO
    {
        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string? ProfileImageUrl { get; set; }
    }
}
