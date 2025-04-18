using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScentSation.Server.Models;

namespace ScentSation.Server.Controllers.suhaib
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CartController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/cart/user/5
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCartByUser(int userId)
        {
            var cartItems = await _context.Carts
                .Include(c => c.CustomPerfume)
                    .ThenInclude(p => p.Bottle)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            return Ok(cartItems);
        }

        // POST: api/cart
        [HttpPost]
        public async Task<ActionResult<Cart>> AddToCart(Cart cart)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return Ok(cart);
        }

        // PUT: api/cart/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCartItem(int id, [FromBody] Cart updatedCart)
        {
            var existing = await _context.Carts.FindAsync(id);
            if (existing == null)
                return NotFound();

            existing.Quantity = updatedCart.Quantity;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/cart/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            var cartItem = await _context.Carts.FindAsync(id);
            if (cartItem == null)
                return NotFound();

            _context.Carts.Remove(cartItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
