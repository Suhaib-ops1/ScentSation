using System;
using System.Collections.Generic;

namespace ScentSation.Server.Models;

public partial class CustomPerfumeNote
{
    public int Id { get; set; }

    public int CustomPerfumeId { get; set; }

    public int NoteId { get; set; }

    public virtual CustomPerfume CustomPerfume { get; set; } = null!;

    public virtual PerfumeNote Note { get; set; } = null!;
}
