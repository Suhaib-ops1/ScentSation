﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ScentSation.Server.Models;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AvailableSession> AvailableSessions { get; set; }

    public virtual DbSet<BottleOption> BottleOptions { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<ConsultationAppointment> ConsultationAppointments { get; set; }

    public virtual DbSet<ContactMessage> ContactMessages { get; set; }

    public virtual DbSet<CustomPerfume> CustomPerfumes { get; set; }

    public virtual DbSet<CustomPerfumeNote> CustomPerfumeNotes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PerfumeNote> PerfumeNotes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-F4OJHS0;Database=ScentSation;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AvailableSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Availabl__3214EC078227062D");

            entity.Property(e => e.Duration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Notes)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.SessionName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Time).HasColumnType("datetime");

            entity.HasOne(d => d.Staff).WithMany(p => p.AvailableSessions)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK_AvailableSessions_Users");
        });

        modelBuilder.Entity<BottleOption>(entity =>
        {
            entity.HasKey(e => e.BottleId).HasName("PK__BottleOp__05EC4081CF54C50A");

            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Size).HasMaxLength(20);
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Cart__51BCD7B7FD641DBD");

            entity.ToTable("Cart");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CustomPerfume).WithMany(p => p.Carts)
                .HasForeignKey(d => d.CustomPerfumeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__CustomPerf__5812160E");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__UserId__571DF1D5");
        });

        modelBuilder.Entity<ConsultationAppointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Consulta__8ECDFCC23DE25811");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");
            entity.Property(e => e.TimeSlot).HasMaxLength(20);

            entity.HasOne(d => d.Staff).WithMany(p => p.ConsultationAppointmentStaffs)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Consultat__Staff__68487DD7");

            entity.HasOne(d => d.User).WithMany(p => p.ConsultationAppointmentUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Consultat__UserI__6754599E");
        });

        modelBuilder.Entity<ContactMessage>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__ContactM__C87C0C9C6603E680");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.SentAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Subject).HasMaxLength(200);
        });

        modelBuilder.Entity<CustomPerfume>(entity =>
        {
            entity.HasKey(e => e.CustomPerfumeId).HasName("PK__CustomPe__8C533B2303F98B05");

            entity.ToTable("CustomPerfume");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Bottle).WithMany(p => p.CustomPerfumes)
                .HasForeignKey(d => d.BottleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CustomPer__Bottl__4F7CD00D");

            entity.HasOne(d => d.User).WithMany(p => p.CustomPerfumes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CustomPer__UserI__4E88ABD4");
        });

        modelBuilder.Entity<CustomPerfumeNote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CustomPe__3214EC07BF4041F4");

            entity.HasOne(d => d.CustomPerfume).WithMany(p => p.CustomPerfumeNotes)
                .HasForeignKey(d => d.CustomPerfumeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CustomPer__Custo__534D60F1");

            entity.HasOne(d => d.Note).WithMany(p => p.CustomPerfumeNotes)
                .HasForeignKey(d => d.NoteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CustomPer__NoteI__5441852A");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCF6679C107");

            entity.Property(e => e.DeliveryAddress).HasMaxLength(200);
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OrderStatus).HasMaxLength(50);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__UserId__5BE2A6F2");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__OrderIte__57ED06810FEC519B");

            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.CustomPerfume).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.CustomPerfumeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__Custo__60A75C0F");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__Order__5FB337D6");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A38582DA34C");

            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.PaymentStatus).HasMaxLength(50);
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__OrderI__6383C8BA");
        });

        modelBuilder.Entity<PerfumeNote>(entity =>
        {
            entity.HasKey(e => e.NoteId).HasName("PK__PerfumeN__EACE355F4C506F69");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Type).HasMaxLength(20);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C8C926485");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(200);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.UserRole).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
