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

    public virtual DbSet<ChatMessage> ChatMessages { get; set; }

    public virtual DbSet<ChatRoom> ChatRooms { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Court> Courts { get; set; }

    public virtual DbSet<CourtImage> CourtImages { get; set; }

    public virtual DbSet<CourtReport> CourtReports { get; set; }

    public virtual DbSet<CourtType> CourtTypes { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Slot> Slots { get; set; }

    public virtual DbSet<SubCourt> SubCourts { get; set; }

    public virtual DbSet<TransactionHistory> TransactionHistories { get; set; }

    public virtual DbSet<UserBalance> UserBalances { get; set; }

    public virtual DbSet<UserReport> UserReports { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__accounts__3213E83F22FEC304");

            entity.ToTable("accounts");

            entity.HasIndex(e => e.Phone, "UQ__accounts__B43B145F9C9D23A4").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("date_of_birth");
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
            entity.Property(e => e.RoleId)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__accounts__role_i__276EDEB3");
        });

        modelBuilder.Entity<AccountOtp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__account___3213E83FD40492D3");

            entity.ToTable("account_otps");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.ExpireAt)
                .HasColumnType("datetime")
                .HasColumnName("expire_at");
            entity.Property(e => e.IsConfirmed).HasColumnName("is_confirmed");
            entity.Property(e => e.OtpCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("otp_code");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phone");

            entity.HasOne(d => d.PhoneNavigation).WithMany(p => p.AccountOtps)
                .HasPrincipalKey(p => p.Phone)
                .HasForeignKey(d => d.Phone)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__account_o__phone__2A4B4B5E");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__bookings__3213E83FB874A8D5");

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
            entity.Property(e => e.OriginalPrice).HasColumnName("original_price");
            entity.Property(e => e.PlayDate)
                .HasColumnType("date")
                .HasColumnName("play_date");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.RefOrder)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_order");
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
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__bookings__book_b__5535A963");

            entity.HasOne(d => d.RefOrderNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.RefOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__bookings__ref_or__5441852A");

            entity.HasOne(d => d.RefSlotNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.RefSlot)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__bookings__ref_sl__534D60F1");

            entity.HasOne(d => d.VoucherCodeNavigation).WithMany(p => p.Bookings)
                .HasPrincipalKey(p => p.VoucherCode)
                .HasForeignKey(d => d.VoucherCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__bookings__vouche__5629CD9C");
        });

        modelBuilder.Entity<ChatMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__chat_mes__3213E83F96A628B9");

            entity.ToTable("chat_messages");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.RefChatroom)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_chatroom");
            entity.Property(e => e.RefOwner)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_owner");
            entity.Property(e => e.RefUser)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_user");
            entity.Property(e => e.SequenceOrder).HasColumnName("sequence_order");

            entity.HasOne(d => d.RefChatroomNavigation).WithMany(p => p.ChatMessages)
                .HasForeignKey(d => d.RefChatroom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__chat_mess__ref_c__5CD6CB2B");

            entity.HasOne(d => d.RefOwnerNavigation).WithMany(p => p.ChatMessageRefOwnerNavigations)
                .HasForeignKey(d => d.RefOwner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__chat_mess__ref_o__5DCAEF64");

            entity.HasOne(d => d.RefUserNavigation).WithMany(p => p.ChatMessageRefUserNavigations)
                .HasForeignKey(d => d.RefUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__chat_mess__ref_u__5EBF139D");
        });

        modelBuilder.Entity<ChatRoom>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__chat_roo__3213E83FB89BEDAD");

            entity.ToTable("chat_rooms");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.RefOwner)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_owner");
            entity.Property(e => e.RefUser)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_user");

            entity.HasOne(d => d.RefOwnerNavigation).WithMany(p => p.ChatRoomRefOwnerNavigations)
                .HasForeignKey(d => d.RefOwner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__chat_room__ref_o__59063A47");

            entity.HasOne(d => d.RefUserNavigation).WithMany(p => p.ChatRoomRefUserNavigations)
                .HasForeignKey(d => d.RefUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__chat_room__ref_u__59FA5E80");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__comments__3213E83F9B212B4E");

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
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__comments__commen__38996AB5");

            entity.HasOne(d => d.RefCourtNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.RefCourt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__comments__ref_co__398D8EEE");
        });

        modelBuilder.Entity<Court>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__courts__3213E83F12391D8B");

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
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasColumnName("description");
            entity.Property(e => e.DistrictId)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("district_id");
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
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__courts__district__32E0915F");

            entity.HasOne(d => d.Owner).WithMany(p => p.Courts)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__courts__owner_id__31EC6D26");
        });

        modelBuilder.Entity<CourtImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__court_im__3213E83F1E8B6DF6");

            entity.ToTable("court_images");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.CourtId)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("court_id");
            entity.Property(e => e.ImageBinary).HasColumnName("image_binary");

            entity.HasOne(d => d.Court).WithMany(p => p.CourtImages)
                .HasForeignKey(d => d.CourtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__court_ima__court__35BCFE0A");
        });

        modelBuilder.Entity<CourtReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__court_re__3213E83F700D230C");

            entity.ToTable("court_reports");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(500)
                .HasColumnName("content");
            entity.Property(e => e.RefCourt)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_court");
            entity.Property(e => e.ReporterId)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("reporter_id");

            entity.HasOne(d => d.RefCourtNavigation).WithMany(p => p.CourtReports)
                .HasForeignKey(d => d.RefCourt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__court_rep__ref_c__3C69FB99");

            entity.HasOne(d => d.Reporter).WithMany(p => p.CourtReports)
                .HasForeignKey(d => d.ReporterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__court_rep__repor__3D5E1FD2");
        });

        modelBuilder.Entity<CourtType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__court_ty__3213E83FB078620A");

            entity.ToTable("court_types");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(50)
                .HasColumnName("content");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__district__3213E83F054088A0");

            entity.ToTable("districts");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.DistrictName)
                .HasMaxLength(50)
                .HasColumnName("district_name");
            entity.Property(e => e.ProvinceId)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("province_id");

            entity.HasOne(d => d.Province).WithMany(p => p.Districts)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__districts__provi__2F10007B");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__orders__3213E83F4FCD9A8A");

            entity.ToTable("orders");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.IsCanceled).HasColumnName("is_canceled");
            entity.Property(e => e.IsPaid).HasColumnName("is_paid");
            entity.Property(e => e.IsRefunded).HasColumnName("is_refunded");
            entity.Property(e => e.OrderAt)
                .HasColumnType("datetime")
                .HasColumnName("order_at");
            entity.Property(e => e.TotalPrice).HasColumnName("total_price");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("transaction_id");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__province__3213E83F9A67BBD3");

            entity.ToTable("provinces");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.ProvinceName)
                .HasMaxLength(50)
                .HasColumnName("province_name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__roles__3213E83FEB6E3144");

            entity.ToTable("roles");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Slot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__slots__3213E83FFC28A0F3");

            entity.ToTable("slots");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.RefSubCourt)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_sub_court");
            entity.Property(e => e.StartTime).HasColumnName("start_time");

            entity.HasOne(d => d.RefSubCourtNavigation).WithMany(p => p.Slots)
                .HasForeignKey(d => d.RefSubCourt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__slots__ref_sub_c__49C3F6B7");
        });

        modelBuilder.Entity<SubCourt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sub_cour__3213E83F4443E630");

            entity.ToTable("sub_courts");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.CourtTypeId)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("court_type_id");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.ParentCourtId)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("parent_court_id");

            entity.HasOne(d => d.CourtType).WithMany(p => p.SubCourts)
                .HasForeignKey(d => d.CourtTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__sub_court__court__46E78A0C");

            entity.HasOne(d => d.ParentCourt).WithMany(p => p.SubCourts)
                .HasForeignKey(d => d.ParentCourtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__sub_court__paren__45F365D3");
        });

        modelBuilder.Entity<TransactionHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__transact__3213E83FA00BD87B");

            entity.ToTable("transaction_history");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.RefFrom)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_from");
            entity.Property(e => e.RefTo)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_to");

            entity.HasOne(d => d.RefFromNavigation).WithMany(p => p.TransactionHistoryRefFromNavigations)
                .HasForeignKey(d => d.RefFrom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__transacti__ref_f__6477ECF3");

            entity.HasOne(d => d.RefToNavigation).WithMany(p => p.TransactionHistoryRefToNavigations)
                .HasForeignKey(d => d.RefTo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__transacti__ref_t__656C112C");
        });

        modelBuilder.Entity<UserBalance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_bal__3213E83F8707A95E");

            entity.ToTable("user_balances");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Balance).HasColumnName("balance");
            entity.Property(e => e.RefUser)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_user");

            entity.HasOne(d => d.RefUserNavigation).WithMany(p => p.UserBalances)
                .HasForeignKey(d => d.RefUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_bala__ref_u__619B8048");
        });

        modelBuilder.Entity<UserReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_rep__3213E83FFB8FE9AF");

            entity.ToTable("user_reports");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(500)
                .HasColumnName("content");
            entity.Property(e => e.RefUser)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_user");
            entity.Property(e => e.ReporterId)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("reporter_id");

            entity.HasOne(d => d.RefUserNavigation).WithMany(p => p.UserReportRefUserNavigations)
                .HasForeignKey(d => d.RefUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_repo__ref_u__403A8C7D");

            entity.HasOne(d => d.Reporter).WithMany(p => p.UserReportReporters)
                .HasForeignKey(d => d.ReporterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_repo__repor__412EB0B6");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vouchers__3213E83F3274A2E0");

            entity.ToTable("vouchers");

            entity.HasIndex(e => e.VoucherCode, "UQ__vouchers__21731069488649AC").IsUnique();

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
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__vouchers__create__4D94879B");

            entity.HasOne(d => d.RefCourtNavigation).WithMany(p => p.Vouchers)
                .HasForeignKey(d => d.RefCourt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__vouchers__ref_co__4E88ABD4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
