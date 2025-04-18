using System;
using System.Collections.Generic;

namespace ScentSation.Server.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public int UserId { get; set; }

    public int CustomPerfumeId { get; set; }

    public int Quantity { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual CustomPerfume CustomPerfume { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
