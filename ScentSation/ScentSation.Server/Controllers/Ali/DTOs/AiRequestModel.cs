namespace ScentSation.Server.Controllers.Ali.DTOs
{
    public class AiRequestModel
    {
        public string Gender { get; set; }
        public List<string> Answers { get; set; }
        public int UserId { get; set; }

    }

    public class AiPerfumeResult
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int BottleId { get; set; }
        public List<int> NoteIds { get; set; }
        public string Description { get; set; }
    }
}
