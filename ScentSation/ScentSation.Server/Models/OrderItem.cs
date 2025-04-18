using System;
using System.Collections.Generic;

namespace ScentSation.Server.Models;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int OrderId { get; set; }

    public int CustomPerfumeId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public virtual CustomPerfume CustomPerfume { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
