using System;
using System.Collections.Generic;
using Bookington.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookington.Core.Data;

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

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Court> Courts { get; set; }

    public virtual DbSet<CourtImage> CourtImages { get; set; }

    public virtual DbSet<CourtType> CourtTypes { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<ReportType> ReportTypes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Slot> Slots { get; set; }

    public virtual DbSet<SubCourt> SubCourts { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__accounts__3213E83F6FD15E99");

            entity.ToTable("accounts");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
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
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__accounts__role_i__267ABA7A");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__bookings__3213E83F85237411");

            entity.ToTable("bookings");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.BookAt)
                .HasColumnType("datetime")
                .HasColumnName("book_at");
            entity.Property(e => e.BookBy)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("book_by");
            entity.Property(e => e.IsCanceled).HasColumnName("is_canceled");
            entity.Property(e => e.IsPaid).HasColumnName("is_paid");
            entity.Property(e => e.IsRefunded).HasColumnName("is_refunded");
            entity.Property(e => e.OriginalPrice).HasColumnName("original_price");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.RefSlot)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_slot");
            entity.Property(e => e.VoucherCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("voucher_code");

            entity.HasOne(d => d.BookByNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.BookBy)
                .HasConstraintName("FK__bookings__book_b__4CA06362");

            entity.HasOne(d => d.RefSlotNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.RefSlot)
                .HasConstraintName("FK__bookings__ref_sl__4BAC3F29");

            entity.HasOne(d => d.VoucherCodeNavigation).WithMany(p => p.Bookings)
                .HasPrincipalKey(p => p.VoucherCode)
                .HasForeignKey(d => d.VoucherCode)
                .HasConstraintName("FK__bookings__vouche__4D94879B");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__comments__3213E83F93EC1A00");

            entity.ToTable("comments");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.CommentWriterId)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("comment_writer_id");
            entity.Property(e => e.Content)
                .HasMaxLength(500)
                .HasColumnName("content");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.RefCourt)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_court");

            entity.HasOne(d => d.CommentWriter).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CommentWriterId)
                .HasConstraintName("FK__comments__commen__34C8D9D1");

            entity.HasOne(d => d.RefCourtNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.RefCourt)
                .HasConstraintName("FK__comments__ref_co__35BCFE0A");
        });

        modelBuilder.Entity<Court>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__courts__3213E83F624A1B4C");

            entity.ToTable("courts");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasColumnName("address");
            entity.Property(e => e.CloseAt).HasColumnName("close_at");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.DistrictId).HasColumnName("district_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.OpenAt).HasColumnName("open_at");
            entity.Property(e => e.OwnerId)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("owner_id");

            entity.HasOne(d => d.District).WithMany(p => p.Courts)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("FK__courts__district__2F10007B");

            entity.HasOne(d => d.Owner).WithMany(p => p.Courts)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("FK__courts__owner_id__2E1BDC42");
        });

        modelBuilder.Entity<CourtImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__court_im__3213E83F89DED088");

            entity.ToTable("court_images");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CourtId)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("court_id");
            entity.Property(e => e.ImageBinary).HasColumnName("image_binary");

            entity.HasOne(d => d.Court).WithMany(p => p.CourtImages)
                .HasForeignKey(d => d.CourtId)
                .HasConstraintName("FK__court_ima__court__31EC6D26");
        });

        modelBuilder.Entity<CourtType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__court_ty__3213E83F78EAFB02");

            entity.ToTable("court_types");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(50)
                .HasColumnName("content");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__district__3213E83F37D2778E");

            entity.ToTable("districts");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.DistrictName)
                .HasMaxLength(50)
                .HasColumnName("district_name");
            entity.Property(e => e.ProvinceId).HasColumnName("province_id");

            entity.HasOne(d => d.Province).WithMany(p => p.Districts)
                .HasForeignKey(d => d.ProvinceId)
                .HasConstraintName("FK__districts__provi__2B3F6F97");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__orders__3213E83FA956F20D");

            entity.ToTable("orders");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.BookingRef)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("booking_ref");
            entity.Property(e => e.OrderAt)
                .HasColumnType("datetime")
                .HasColumnName("order_at");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.BookingRefNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.BookingRef)
                .HasConstraintName("FK__orders__booking___5070F446");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__province__3213E83FC73BA5EC");

            entity.ToTable("provinces");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ProvinceName)
                .HasMaxLength(50)
                .HasColumnName("province_name");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__reports__3213E83F1AB187A1");

            entity.ToTable("reports");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(500)
                .HasColumnName("content");
            entity.Property(e => e.ReporterId)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("reporter_id");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Reporter).WithMany(p => p.Reports)
                .HasForeignKey(d => d.ReporterId)
                .HasConstraintName("FK__reports__reporte__3B75D760");

            entity.HasOne(d => d.Type).WithMany(p => p.Reports)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK__reports__type_id__3A81B327");
        });

        modelBuilder.Entity<ReportType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__report_t__3213E83F0427123E");

            entity.ToTable("report_types");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ReportName)
                .HasMaxLength(50)
                .HasColumnName("report_name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__roles__3213E83F59CB7009");

            entity.ToTable("roles");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Slot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__slots__3213E83FFD788983");

            entity.ToTable("slots");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.RefSubCourt)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_sub_court");
            entity.Property(e => e.StartTime).HasColumnName("start_time");

            entity.HasOne(d => d.RefSubCourtNavigation).WithMany(p => p.Slots)
                .HasForeignKey(d => d.RefSubCourt)
                .HasConstraintName("FK__slots__ref_sub_c__440B1D61");
        });

        modelBuilder.Entity<SubCourt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sub_cour__3213E83F41A47DE2");

            entity.ToTable("sub_courts");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.CourtTypeId).HasColumnName("court_type_id");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.ParentCourtId)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("parent_court_id");

            entity.HasOne(d => d.CourtType).WithMany(p => p.SubCourts)
                .HasForeignKey(d => d.CourtTypeId)
                .HasConstraintName("FK__sub_court__court__412EB0B6");

            entity.HasOne(d => d.ParentCourt).WithMany(p => p.SubCourts)
                .HasForeignKey(d => d.ParentCourtId)
                .HasConstraintName("FK__sub_court__paren__403A8C7D");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vouchers__3213E83F4F3F0063");

            entity.ToTable("vouchers");

            entity.HasIndex(e => e.VoucherCode, "UQ__vouchers__2173106953AA8CF3").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("create_by");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .HasColumnName("description");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.MaxQuantity).HasColumnName("max_quantity");
            entity.Property(e => e.RefCourt)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_court");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
            entity.Property(e => e.Usages).HasColumnName("usages");
            entity.Property(e => e.VoucherCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("voucher_code");

            entity.HasOne(d => d.CreateByNavigation).WithMany(p => p.Vouchers)
                .HasForeignKey(d => d.CreateBy)
                .HasConstraintName("FK__vouchers__create__47DBAE45");

            entity.HasOne(d => d.RefCourtNavigation).WithMany(p => p.Vouchers)
                .HasForeignKey(d => d.RefCourt)
                .HasConstraintName("FK__vouchers__ref_co__48CFD27E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
