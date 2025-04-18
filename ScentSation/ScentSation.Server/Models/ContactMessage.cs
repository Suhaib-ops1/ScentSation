using System;
using System.Collections.Generic;

namespace ScentSation.Server.Models;

public partial class ContactMessage
{
    public int MessageId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Subject { get; set; }

    public string Message { get; set; } = null!;

    public DateTime? SentAt { get; set; }
}
