namespace ScentSation.Server.Controllers.Ali.DTOs
{
    public class CustomPerfumeDTO
    {
        public int UserId { get; set; }

        public string Name { get; set; } = string.Empty;

        public int BottleId { get; set; }

        public decimal Price { get; set; }

        public List<int> CustomPerfumeNoteIds { get; set; } = new();
    }
}
