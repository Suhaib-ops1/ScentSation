using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScentSation.Server.Models;
using ScentSation.Server.Controllers.Ali.DTOs;

namespace ScentSation.Server.Controllers.Ali
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomPerfumeController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CustomPerfumeController(MyDbContext context)
        {
            _context = context;
        }

        // POST: api/customperfume
        [HttpPost]
        public async Task<IActionResult> CreateCustomPerfume([FromBody] CustomPerfumeDTO dto)
        {
            var perfume = new CustomPerfume
            {
                UserId = dto.UserId,
                Name = dto.Name,
                BottleId = dto.BottleId,
                Price = dto.Price,
                CreatedAt = DateTime.Now
            };

            foreach (var noteId in dto.CustomPerfumeNoteIds)
            {
                perfume.CustomPerfumeNotes.Add(new CustomPerfumeNote
                {
                    NoteId = noteId
                });
            }

            _context.CustomPerfumes.Add(perfume);
            await _context.SaveChangesAsync();

            // ✅ إعادة جلب العطر مع الزجاجة والنوتات بعد الإنشاء
            var fullPerfume = await _context.CustomPerfumes
                .Include(p => p.Bottle)
                .Include(p => p.CustomPerfumeNotes)
                    .ThenInclude(n => n.Note)
                .FirstOrDefaultAsync(p => p.CustomPerfumeId == perfume.CustomPerfumeId);

            return Ok(fullPerfume);
        }

        // GET: api/customperfume/user/5
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<CustomPerfume>>> GetUserPerfumes(int userId)
        {
            return await _context.CustomPerfumes
                .Include(p => p.Bottle)
                .Include(p => p.CustomPerfumeNotes)
                    .ThenInclude(n => n.Note)
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }
        [HttpGet("by-user/{userId}")]
        public IActionResult GetOrdersByUser(int userId)
        {
            var orders = _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.Payments)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.CustomPerfume)
                        .ThenInclude(cp => cp.Bottle)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.CustomPerfume)
                        .ThenInclude(cp => cp.CustomPerfumeNotes)
                            .ThenInclude(cpn => cpn.Note)
                .ToList();

            var result = orders.Select(o => new OrderWithDetailsDTO
            {
                OrderId = o.OrderId,
                TotalAmount = o.TotalAmount,
                OrderDate = o.OrderDate,
                OrderStatus = o.OrderStatus,
                DeliveryAddress = o.DeliveryAddress,
                PaymentMethod = o.Payments.FirstOrDefault()?.PaymentMethod,
                Perfumes = o.OrderItems.Select(oi => new PerfumeSummaryDTO
                {
                    Name = oi.CustomPerfume.Name,
                    Price = oi.UnitPrice,
                    Quantity = oi.Quantity,
                    BottleName = oi.CustomPerfume.Bottle.Name,
                    Notes = oi.CustomPerfume.CustomPerfumeNotes.Select(n => n.Note.Name).ToList()
                }).ToList()
            });

            return Ok(result);
        }

    }
}
