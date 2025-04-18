using System;
using System.Collections.Generic;

namespace ScentSation.Server.Models;

public partial class BottleOption
{
    public int BottleId { get; set; }

    public string Name { get; set; } = null!;

    public string Size { get; set; } = null!;

    public string Color { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<CustomPerfume> CustomPerfumes { get; set; } = new List<CustomPerfume>();
}
