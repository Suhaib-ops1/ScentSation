using System;
using System.Collections.Generic;

namespace ScentSation.Server.Models;

public partial class AvailableSession
{
    public int Id { get; set; }

    public string? SessionName { get; set; }

    public int? StaffId { get; set; }

    public DateTime? Time { get; set; }

    public string? Status { get; set; }

    public string? Notes { get; set; }

    public string Duration { get; set; } = null!;

    public virtual User? Staff { get; set; }
}
