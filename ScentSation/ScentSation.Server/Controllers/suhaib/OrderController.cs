using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScentSation.Server.Models;
using ScentSation.Server.Controllers.Ali.DTOs;
using ScentSation.Server.Controllers.suhaib.DTOs; // تأكد من وجود هذا الـ namespace أو غيّره حسب موقع الـ DTO

namespace ScentSation.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly MyDbContext _context;

        public OrderController(MyDbContext context)
        {
            _context = context;
        }

        // POST: api/order
        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderDTO orderDto)
        {
            if (orderDto == null || orderDto.OrderItems == null || !orderDto.OrderItems.Any())
                return BadRequest("Invalid order");

            var order = new Order
            {
                UserId = orderDto.UserId,
                TotalAmount = orderDto.TotalAmount,
                DeliveryAddress = orderDto.DeliveryAddress,
                OrderDate = DateTime.Now,
                OrderStatus = "Pending",
                OrderItems = orderDto.OrderItems.Select(i => new OrderItem
                {
                    CustomPerfumeId = i.CustomPerfumeId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Save payment details (hidden from user)
            var payment = new Payment
            {
                OrderId = order.OrderId,
                PaymentMethod = orderDto.PaymentMethod,
                PaymentStatus = "Pending", // لأنك ما عندك UserId أو ProofImage
                TransactionDate = DateTime.Now
            };

            _context.Payments.Add(payment);

            // Clear cart
            var cartItems = await _context.Carts
                .Where(c => c.UserId == orderDto.UserId)
                .ToListAsync();
            _context.Carts.RemoveRange(cartItems);

            await _context.SaveChangesAsync();
            return Ok(order.OrderId);
        }

        // GET: api/order/user/5
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByUser(int userId)
        {
            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                    .ThenInclude(i => i.CustomPerfume)
                        .ThenInclude(p => p.Bottle)
                .ToListAsync();

            return Ok(orders);
        }

        // GET: api/order/1
        [HttpGet("{orderId}")]
        public async Task<ActionResult<Order>> GetOrderDetails(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(i => i.CustomPerfume)
                        .ThenInclude(p => p.Bottle)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
                return NotFound();

            return Ok(order);
        }
    }
}
