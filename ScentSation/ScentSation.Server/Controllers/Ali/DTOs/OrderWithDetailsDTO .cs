namespace ScentSation.Server.Controllers.Ali.DTOs
{
    public class OrderWithDetailsDTO
    {
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime? OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string? DeliveryAddress { get; set; }
        public string? PaymentMethod { get; set; }

        public List<PerfumeSummaryDTO> Perfumes { get; set; } = new();
    }

    public class PerfumeSummaryDTO
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string BottleName { get; set; } = string.Empty;
        public List<string> Notes { get; set; } = new();
    }
}
