using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Bokkington_Api.Entities;

public partial class BookingtonDbContext : DbContext
{
    public BookingtonDbContext()
    {
    }

    public BookingtonDbContext(DbContextOptions<BookingtonDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<CourtImage> CourtImages { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);Database=BookingtonDB;User Id=khoalnm;Password=admin;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("accounts");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .HasColumnName("full_name");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.IsConfirmed).HasColumnName("is_confirmed");
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_accounts_roles");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("comments");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.CommentWriterId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("comment_writer_id");
            entity.Property(e => e.Contents).HasColumnName("contents");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.RefCourt)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ref_court");

            entity.HasOne(d => d.CommentWriter).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CommentWriterId)
                .HasConstraintName("FK_comments_accounts");
        });

        modelBuilder.Entity<CourtImage>(entity =>
        {
            entity.ToTable("court_images");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CourtId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("court_id");
            entity.Property(e => e.Image).HasColumnName("image");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.ToTable("reports");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Contents).HasColumnName("contents");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.ReporterId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("reporter_id");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Reporter).WithMany(p => p.Reports)
                .HasForeignKey(d => d.ReporterId)
                .HasConstraintName("FK_reports_accounts");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("roles");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("role_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
