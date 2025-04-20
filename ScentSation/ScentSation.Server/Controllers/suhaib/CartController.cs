using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScentSation.Server.Models;
using ScentSation.Server.Controllers.Ali.DTOs;

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
        [HttpGet("user/{userId}")]
        public async Task<ActionResult> GetCartByUser(int userId)
        {
            var cartItems = await _context.Carts
                .Where(c => c.UserId == userId)
                .Include(c => c.CustomPerfume)
                    .ThenInclude(p => p.Bottle)
                .Include(c => c.CustomPerfume)
                    .ThenInclude(p => p.CustomPerfumeNotes)
                        .ThenInclude(n => n.Note)
                .Select(c => new {
                    c.CartId,
                    c.Quantity,
                    CustomPerfume = new
                    {
                        c.CustomPerfume.Name,
                        c.CustomPerfume.Price,
                        Bottle = new
                        {
                            c.CustomPerfume.Bottle.Name,
                            c.CustomPerfume.Bottle.ImageUrl
                        },
                        Notes = c.CustomPerfume.CustomPerfumeNotes.Select(n => new {
                            Name = n.Note.Name,
                            Type = n.Note.Type
                        }).ToList()
                    }
                }).ToListAsync();

            return Ok(cartItems);
        }


        // POST: api/cart
        [HttpPost]
        public async Task<ActionResult<Cart>> AddToCart([FromBody] AddToCart dto)
        {
            var existingItem = await _context.Carts
                .FirstOrDefaultAsync(c =>
                    c.UserId == dto.UserId &&
                    c.CustomPerfumeId == dto.CustomPerfumeId);

            if (existingItem != null)
            {
                existingItem.Quantity += dto.Quantity;
                await _context.SaveChangesAsync();

                var updatedItem = await _context.Carts
                    .Include(c => c.CustomPerfume)
                        .ThenInclude(p => p.Bottle)
                    .Include(c => c.CustomPerfume)
                        .ThenInclude(p => p.CustomPerfumeNotes)
                            .ThenInclude(n => n.Note)
                    .FirstOrDefaultAsync(c => c.CartId == existingItem.CartId);

                return Ok(updatedItem);
            }

            var cartItem = new Cart
            {
                UserId = dto.UserId,
                CustomPerfumeId = dto.CustomPerfumeId,
                Quantity = dto.Quantity
            };

            _context.Carts.Add(cartItem);
            await _context.SaveChangesAsync();

            var fullCartItem = await _context.Carts
                .Include(c => c.CustomPerfume)
                    .ThenInclude(p => p.Bottle)
                .Include(c => c.CustomPerfume)
                    .ThenInclude(p => p.CustomPerfumeNotes)
                        .ThenInclude(n => n.Note)
                .FirstOrDefaultAsync(c => c.CartId == cartItem.CartId);

            return Ok(fullCartItem);
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

        [HttpDelete("clear/{userId}")]
        public IActionResult ClearCart(int userId)
        {
            var userCart = _context.Carts.Where(c => c.UserId == userId).ToList();
            if (userCart.Any())
            {
                _context.Carts.RemoveRange(userCart);
                _context.SaveChanges();
            }

            return Ok(new { message = "Cart cleared successfully." });
        }

    }
}
