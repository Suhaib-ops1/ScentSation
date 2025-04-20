using System;
using System.Collections.Generic;

namespace ScentSation.Server.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? ProfileImageUrl { get; set; }

    public string UserRole { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<AvailableSession> AvailableSessions { get; set; } = new List<AvailableSession>();

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<ConsultationAppointment> ConsultationAppointmentStaffs { get; set; } = new List<ConsultationAppointment>();

    public virtual ICollection<ConsultationAppointment> ConsultationAppointmentUsers { get; set; } = new List<ConsultationAppointment>();

    public virtual ICollection<CustomPerfume> CustomPerfumes { get; set; } = new List<CustomPerfume>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
