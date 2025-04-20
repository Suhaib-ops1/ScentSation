namespace ScentSation.Server.Controllers.suhaib.DTOs
{
    public class OrderDTO
    {
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public string DeliveryAddress { get; set; }
        public string PaymentMethod { get; set; }
        public string? PaymentProofImage { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; }
    }

    public class OrderItemDTO
    {
        public int CustomPerfumeId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class CartItemDTO
    {
        public int CartId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string PerfumeName { get; set; }
        public string BottleName { get; set; }
        public string BottleImage { get; set; }
        public List<string> Notes { get; set; }
    }

}
