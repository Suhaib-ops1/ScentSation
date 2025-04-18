using System;
using System.Collections.Generic;

namespace ScentSation.Server.Models;

public partial class PerfumeNote
{
    public int NoteId { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public virtual ICollection<CustomPerfumeNote> CustomPerfumeNotes { get; set; } = new List<CustomPerfumeNote>();
}
