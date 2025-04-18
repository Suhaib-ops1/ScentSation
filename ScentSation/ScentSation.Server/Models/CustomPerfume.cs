using System;
using System.Collections.Generic;

namespace ScentSation.Server.Models;

public partial class CustomPerfume
{
    public int CustomPerfumeId { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public int BottleId { get; set; }

    public decimal Price { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual BottleOption Bottle { get; set; } = null!;

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<CustomPerfumeNote> CustomPerfumeNotes { get; set; } = new List<CustomPerfumeNote>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User User { get; set; } = null!;
}
