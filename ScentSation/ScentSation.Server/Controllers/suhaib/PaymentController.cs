using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScentSation.Server.Models;

namespace ScentSation.Server.Controllers.suhaib
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly MyDbContext _context;

        public PaymentController(MyDbContext context)
        {
            _context = context;
        }

        // POST: api/payment
        [HttpPost]
        public async Task<IActionResult> AddPayment([FromBody] Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return Ok(payment);
        }

        // GET: api/payment/order/5
        [HttpGet("order/{orderId}")]
        public async Task<ActionResult<Payment>> GetPaymentByOrderId(int orderId)
        {
            var payment = await _context.Payments
                .FirstOrDefaultAsync(p => p.OrderId == orderId);

            if (payment == null)
                return NotFound();

            return Ok(payment);
        }
    }
}
