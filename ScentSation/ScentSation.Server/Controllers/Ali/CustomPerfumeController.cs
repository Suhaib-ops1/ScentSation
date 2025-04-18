using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScentSation.Server.Models;

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
        public async Task<IActionResult> CreateCustomPerfume([FromBody] CustomPerfume perfume)
        {
            _context.CustomPerfumes.Add(perfume);
            await _context.SaveChangesAsync();
            return Ok(perfume);
        }

        // GET: api/customperfume/user/5
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<CustomPerfume>>> GetUserPerfumes(int userId)
        {
            return await _context.CustomPerfumes
                .Include(p => p.Bottle)
                .Include(p => p.CustomPerfumeNotes)
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }
    }
}
