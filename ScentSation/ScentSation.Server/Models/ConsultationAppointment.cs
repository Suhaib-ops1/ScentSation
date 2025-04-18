using System;
using System.Collections.Generic;

namespace ScentSation.Server.Models;

public partial class ConsultationAppointment
{
    public int AppointmentId { get; set; }

    public int UserId { get; set; }

    public int StaffId { get; set; }

    public DateOnly AppointmentDate { get; set; }

    public string TimeSlot { get; set; } = null!;

    public string? Status { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User Staff { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
