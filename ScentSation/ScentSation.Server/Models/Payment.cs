﻿using System;
using System.Collections.Generic;

namespace ScentSation.Server.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int OrderId { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string PaymentStatus { get; set; } = null!;

    public DateTime? TransactionDate { get; set; }

    public virtual Order Order { get; set; } = null!;
}
