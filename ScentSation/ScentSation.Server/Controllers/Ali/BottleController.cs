using Microsoft.AspNetCore.Mvc;
using ScentSation.Server.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class BottleController : ControllerBase
{
    private readonly MyDbContext _context;

    public BottleController(MyDbContext context)
    {
        _context = context;
    }

    // GET: api/bottle
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BottleOption>>> GetBottles()
    {
        return await _context.BottleOptions.ToListAsync();
    }
}
