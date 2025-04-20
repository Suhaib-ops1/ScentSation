namespace ScentSation.Server.Admin.DTO
{
    public class ContactMessageDTO
    {
        public int MessageId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? Subject { get; set; }
        public string Message { get; set; }
        public DateTime? SentAt { get; set; }
    }
}
