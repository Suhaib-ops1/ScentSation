// File: Controllers/PerfumeNoteController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScentSation.Server.Models;

namespace ScentSation.Server.Controllers.Ali
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfumeNoteController : ControllerBase
    {
        private readonly MyDbContext _context;

        public PerfumeNoteController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/perfumenote
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PerfumeNote>>> GetNotes()
        {
            return await _context.PerfumeNotes.ToListAsync();
        }
    }
}
