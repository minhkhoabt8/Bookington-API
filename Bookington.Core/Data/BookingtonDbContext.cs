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

    public virtual DbSet<AccountOtp> AccountOtps { get; set; }

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);Database=BookingtonDB;User Id=khoalnm;Password=admin;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__accounts__3213E83FF46FA5FD");

            entity.ToTable("accounts");

            entity.HasIndex(e => e.Phone, "UQ__accounts__B43B145F7F883CB5").IsUnique();

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
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__accounts__role_i__276EDEB3");
        });

        modelBuilder.Entity<AccountOtp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__account___3213E83FB02E2E92");

            entity.ToTable("account_otps");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.IsConfirmed).HasColumnName("is_confirmed");
            entity.Property(e => e.Otp)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("otp");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phone");

            entity.HasOne(d => d.PhoneNavigation).WithMany(p => p.AccountOtps)
                .HasPrincipalKey(p => p.Phone)
                .HasForeignKey(d => d.Phone)
                .HasConstraintName("FK__account_o__phone__2A4B4B5E");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__bookings__3213E83F77E10ED4");

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
                .HasConstraintName("FK__bookings__book_b__5070F446");

            entity.HasOne(d => d.RefSlotNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.RefSlot)
                .HasConstraintName("FK__bookings__ref_sl__4F7CD00D");

            entity.HasOne(d => d.VoucherCodeNavigation).WithMany(p => p.Bookings)
                .HasPrincipalKey(p => p.VoucherCode)
                .HasForeignKey(d => d.VoucherCode)
                .HasConstraintName("FK__bookings__vouche__5165187F");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__comments__3213E83F14EC6B71");

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
                .HasConstraintName("FK__comments__commen__38996AB5");

            entity.HasOne(d => d.RefCourtNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.RefCourt)
                .HasConstraintName("FK__comments__ref_co__398D8EEE");
        });

        modelBuilder.Entity<Court>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__courts__3213E83F3FDD9262");

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
                .HasConstraintName("FK__courts__district__32E0915F");

            entity.HasOne(d => d.Owner).WithMany(p => p.Courts)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("FK__courts__owner_id__31EC6D26");
        });

        modelBuilder.Entity<CourtImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__court_im__3213E83F7AB43B57");

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
                .HasConstraintName("FK__court_ima__court__35BCFE0A");
        });

        modelBuilder.Entity<CourtType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__court_ty__3213E83F858496CD");

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
            entity.HasKey(e => e.Id).HasName("PK__district__3213E83F393D5ADD");

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
                .HasConstraintName("FK__districts__provi__2F10007B");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__orders__3213E83F8029852E");

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
                .HasConstraintName("FK__orders__booking___5441852A");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__province__3213E83FDFC026C7");

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
            entity.HasKey(e => e.Id).HasName("PK__reports__3213E83F9D9B7027");

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
                .HasConstraintName("FK__reports__reporte__3F466844");

            entity.HasOne(d => d.Type).WithMany(p => p.Reports)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK__reports__type_id__3E52440B");
        });

        modelBuilder.Entity<ReportType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__report_t__3213E83F4890452E");

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
            entity.HasKey(e => e.Id).HasName("PK__roles__3213E83F52DAE39F");

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
            entity.HasKey(e => e.Id).HasName("PK__slots__3213E83F87D70A4E");

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
                .HasConstraintName("FK__slots__ref_sub_c__47DBAE45");
        });

        modelBuilder.Entity<SubCourt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sub_cour__3213E83F5EE76077");

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
                .HasConstraintName("FK__sub_court__court__44FF419A");

            entity.HasOne(d => d.ParentCourt).WithMany(p => p.SubCourts)
                .HasForeignKey(d => d.ParentCourtId)
                .HasConstraintName("FK__sub_court__paren__440B1D61");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vouchers__3213E83F0A4E665C");

            entity.ToTable("vouchers");

            entity.HasIndex(e => e.VoucherCode, "UQ__vouchers__2173106971035309").IsUnique();

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
                .HasConstraintName("FK__vouchers__create__4BAC3F29");

            entity.HasOne(d => d.RefCourtNavigation).WithMany(p => p.Vouchers)
                .HasForeignKey(d => d.RefCourt)
                .HasConstraintName("FK__vouchers__ref_co__4CA06362");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
