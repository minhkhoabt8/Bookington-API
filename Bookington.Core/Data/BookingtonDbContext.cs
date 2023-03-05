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

    public virtual DbSet<AccountAvatar> AccountAvatars { get; set; }

    public virtual DbSet<AccountOtp> AccountOtps { get; set; }

    public virtual DbSet<Ban> Bans { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<ChatMessage> ChatMessages { get; set; }

    public virtual DbSet<ChatRoom> ChatRooms { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Court> Courts { get; set; }

    public virtual DbSet<CourtImage> CourtImages { get; set; }

    public virtual DbSet<CourtReport> CourtReports { get; set; }

    public virtual DbSet<CourtReportResponse> CourtReportResponses { get; set; }

    public virtual DbSet<CourtType> CourtTypes { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<LoginToken> LoginTokens { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Slot> Slots { get; set; }

    public virtual DbSet<SubCourt> SubCourts { get; set; }

    public virtual DbSet<TransactionHistory> TransactionHistories { get; set; }

    public virtual DbSet<UserBalance> UserBalances { get; set; }

    public virtual DbSet<UserReport> UserReports { get; set; }

    public virtual DbSet<UserReportResponse> UserReportResponses { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server =(local); database = BookingtonDB;uid=khoalnm;pwd=admin;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__accounts__3213E83FC82E5A90");

            entity.ToTable("accounts");

            entity.HasIndex(e => e.Phone, "UQ__accounts__B43B145F4D261CD4").IsUnique();

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
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.RefAvatar)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_avatar");
            entity.Property(e => e.RoleId)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("role_id");

            entity.HasOne(d => d.RefAvatarNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RefAvatar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__accounts__ref_av__2A4B4B5E");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__accounts__role_i__29572725");
        });

        modelBuilder.Entity<AccountAvatar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__account___3213E83F3425597B");

            entity.ToTable("account_avatars");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.RefImage)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_image");
        });

        modelBuilder.Entity<AccountOtp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__account___3213E83F4C00CC5F");

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
            entity.Property(e => e.RefAccount)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_account");

            entity.HasOne(d => d.RefAccountNavigation).WithMany(p => p.AccountOtps)
                .HasForeignKey(d => d.RefAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__account_o__ref_a__2D27B809");
        });

        modelBuilder.Entity<Ban>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__bans__3213E83FF8A75CE5");

            entity.ToTable("bans");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.BanUntil)
                .HasColumnType("datetime")
                .HasColumnName("ban_until");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.IsAccountBan).HasColumnName("is_account_ban");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.IsCourtBan).HasColumnName("is_court_ban");
            entity.Property(e => e.Reason)
                .HasMaxLength(500)
                .HasColumnName("reason");
            entity.Property(e => e.RefAccount)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_account");
            entity.Property(e => e.RefCourt)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_court");

            entity.HasOne(d => d.RefAccountNavigation).WithMany(p => p.Bans)
                .HasForeignKey(d => d.RefAccount)
                .HasConstraintName("FK__bans__ref_accoun__7A672E12");

            entity.HasOne(d => d.RefCourtNavigation).WithMany(p => p.Bans)
                .HasForeignKey(d => d.RefCourt)
                .HasConstraintName("FK__bans__ref_court__7B5B524B");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__bookings__3213E83F199C458A");

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

            entity.HasOne(d => d.BookByNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.BookBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__bookings__book_b__66603565");

            entity.HasOne(d => d.RefOrderNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.RefOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__bookings__ref_or__656C112C");

            entity.HasOne(d => d.RefSlotNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.RefSlot)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__bookings__ref_sl__6477ECF3");
        });

        modelBuilder.Entity<ChatMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__chat_mes__3213E83F03BAEB0C");

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
                .HasConstraintName("FK__chat_mess__ref_c__6D0D32F4");

            entity.HasOne(d => d.RefOwnerNavigation).WithMany(p => p.ChatMessageRefOwnerNavigations)
                .HasForeignKey(d => d.RefOwner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__chat_mess__ref_o__6E01572D");

            entity.HasOne(d => d.RefUserNavigation).WithMany(p => p.ChatMessageRefUserNavigations)
                .HasForeignKey(d => d.RefUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__chat_mess__ref_u__6EF57B66");
        });

        modelBuilder.Entity<ChatRoom>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__chat_roo__3213E83FBACFA368");

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
                .HasConstraintName("FK__chat_room__ref_o__693CA210");

            entity.HasOne(d => d.RefUserNavigation).WithMany(p => p.ChatRoomRefUserNavigations)
                .HasForeignKey(d => d.RefUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__chat_room__ref_u__6A30C649");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__comments__3213E83FAB9D61B6");

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
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.RefCourt)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_court");

            entity.HasOne(d => d.CommentWriter).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CommentWriterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__comments__commen__3E52440B");

            entity.HasOne(d => d.RefCourtNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.RefCourt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__comments__ref_co__3F466844");
        });

        modelBuilder.Entity<Court>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__courts__3213E83F918EECEC");

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
                .HasConstraintName("FK__courts__district__38996AB5");

            entity.HasOne(d => d.Owner).WithMany(p => p.Courts)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__courts__owner_id__37A5467C");
        });

        modelBuilder.Entity<CourtImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__court_im__3213E83FD178DA00");

            entity.ToTable("court_images");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.CourtId)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("court_id");
            entity.Property(e => e.RefImage)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_image");

            entity.HasOne(d => d.Court).WithMany(p => p.CourtImages)
                .HasForeignKey(d => d.CourtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__court_ima__court__3B75D760");
        });

        modelBuilder.Entity<CourtReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__court_re__3213E83F78E36AD5");

            entity.ToTable("court_reports");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(1000)
                .HasColumnName("content");
            entity.Property(e => e.IsResponded).HasColumnName("is_responded");
            entity.Property(e => e.RefCourt)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_court");
            entity.Property(e => e.RefResponse)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_response");
            entity.Property(e => e.ReporterId)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("reporter_id");

            entity.HasOne(d => d.RefCourtNavigation).WithMany(p => p.CourtReports)
                .HasForeignKey(d => d.RefCourt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__court_rep__ref_c__440B1D61");

            entity.HasOne(d => d.RefResponseNavigation).WithMany(p => p.CourtReports)
                .HasForeignKey(d => d.RefResponse)
                .HasConstraintName("FK__court_rep__ref_r__45F365D3");

            entity.HasOne(d => d.Reporter).WithMany(p => p.CourtReports)
                .HasForeignKey(d => d.ReporterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__court_rep__repor__44FF419A");
        });

        modelBuilder.Entity<CourtReportResponse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__court_re__3213E83FEC7EEBB5");

            entity.ToTable("court_report_responses");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(1000)
                .HasColumnName("content");
        });

        modelBuilder.Entity<CourtType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__court_ty__3213E83FC3A78D59");

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
            entity.HasKey(e => e.Id).HasName("PK__district__3213E83F11904467");

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
                .HasConstraintName("FK__districts__provi__34C8D9D1");
        });

        modelBuilder.Entity<LoginToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__login_to__3213E83F7DD2294A");

            entity.ToTable("login_tokens");

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
            entity.Property(e => e.IsRevoked).HasColumnName("is_revoked");
            entity.Property(e => e.RefAccount)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_account");
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(2048)
                .IsUnicode(false)
                .HasColumnName("refresh_token");
            entity.Property(e => e.Token)
                .HasMaxLength(2048)
                .IsUnicode(false)
                .HasColumnName("token");

            entity.HasOne(d => d.RefAccountNavigation).WithMany(p => p.LoginTokens)
                .HasForeignKey(d => d.RefAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__login_tok__ref_a__300424B4");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__notifica__3213E83FDDD6D746");

            entity.ToTable("notifications");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("content");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.IsRead).HasColumnName("is_read");
            entity.Property(e => e.RefAccount)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_account");

            entity.HasOne(d => d.RefAccountNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.RefAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__notificat__ref_a__74AE54BC");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__orders__3213E83FCEC5BA3F");

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
            entity.Property(e => e.OriginalPrice).HasColumnName("original_price");
            entity.Property(e => e.TotalPrice).HasColumnName("total_price");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("transaction_id");
            entity.Property(e => e.VoucherCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("voucher_code");

            entity.HasOne(d => d.Transaction).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TransactionId)
                .HasConstraintName("FK__orders__transact__60A75C0F");

            entity.HasOne(d => d.VoucherCodeNavigation).WithMany(p => p.Orders)
                .HasPrincipalKey(p => p.VoucherCode)
                .HasForeignKey(d => d.VoucherCode)
                .HasConstraintName("FK__orders__voucher___619B8048");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__promotio__3213E83FA25F0716");

            entity.ToTable("promotions");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("end_time");
            entity.Property(e => e.IsCourtPromotion).HasColumnName("is_court_promotion");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Link)
                .HasMaxLength(2048)
                .IsUnicode(false)
                .HasColumnName("link");
            entity.Property(e => e.PromotionOrder).HasColumnName("promotion_order");
            entity.Property(e => e.RefCourt)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_court");
            entity.Property(e => e.RefImage)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_image");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("start_time");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");

            entity.HasOne(d => d.RefCourtNavigation).WithMany(p => p.Promotions)
                .HasForeignKey(d => d.RefCourt)
                .HasConstraintName("FK__promotion__ref_c__778AC167");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__province__3213E83F52E362D4");

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
            entity.HasKey(e => e.Id).HasName("PK__roles__3213E83FCAA4D8CB");

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
            entity.HasKey(e => e.Id).HasName("PK__slots__3213E83FBB3BE737");

            entity.ToTable("slots");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.DaysInSchedule)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("days_in_schedule");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.RefSubCourt)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_sub_court");
            entity.Property(e => e.StartTime).HasColumnName("start_time");

            entity.HasOne(d => d.RefSubCourtNavigation).WithMany(p => p.Slots)
                .HasForeignKey(d => d.RefSubCourt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__slots__ref_sub_c__5535A963");
        });

        modelBuilder.Entity<SubCourt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sub_cour__3213E83FBEB5844F");

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
            entity.Property(e => e.SlotDuration).HasColumnName("slot_duration");

            entity.HasOne(d => d.CourtType).WithMany(p => p.SubCourts)
                .HasForeignKey(d => d.CourtTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__sub_court__court__52593CB8");

            entity.HasOne(d => d.ParentCourt).WithMany(p => p.SubCourts)
                .HasForeignKey(d => d.ParentCourtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__sub_court__paren__5165187F");
        });

        modelBuilder.Entity<TransactionHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__transact__3213E83FF04F1EA8");

            entity.ToTable("transaction_history");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.Reason)
                .HasMaxLength(500)
                .HasColumnName("reason");
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
                .HasConstraintName("FK__transacti__ref_f__5CD6CB2B");

            entity.HasOne(d => d.RefToNavigation).WithMany(p => p.TransactionHistoryRefToNavigations)
                .HasForeignKey(d => d.RefTo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__transacti__ref_t__5DCAEF64");
        });

        modelBuilder.Entity<UserBalance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_bal__3213E83FFB632C9E");

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
                .HasConstraintName("FK__user_bala__ref_u__71D1E811");
        });

        modelBuilder.Entity<UserReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_rep__3213E83F5A965BED");

            entity.ToTable("user_reports");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(1000)
                .HasColumnName("content");
            entity.Property(e => e.IsResponsed).HasColumnName("is_responsed");
            entity.Property(e => e.RefResponse)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_response");
            entity.Property(e => e.RefUser)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_user");
            entity.Property(e => e.ReporterId)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("reporter_id");

            entity.HasOne(d => d.RefResponseNavigation).WithMany(p => p.UserReports)
                .HasForeignKey(d => d.RefResponse)
                .HasConstraintName("FK__user_repo__ref_r__4CA06362");

            entity.HasOne(d => d.RefUserNavigation).WithMany(p => p.UserReportRefUserNavigations)
                .HasForeignKey(d => d.RefUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_repo__ref_u__4AB81AF0");

            entity.HasOne(d => d.Reporter).WithMany(p => p.UserReportReporters)
                .HasForeignKey(d => d.ReporterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_repo__repor__4BAC3F29");
        });

        modelBuilder.Entity<UserReportResponse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_rep__3213E83F8503C715");

            entity.ToTable("user_report_responses");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(1000)
                .HasColumnName("content");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vouchers__3213E83F08758111");

            entity.ToTable("vouchers");

            entity.HasIndex(e => e.VoucherCode, "UQ__vouchers__21731069457448AB").IsUnique();

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
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
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
                .HasConstraintName("FK__vouchers__create__59063A47");

            entity.HasOne(d => d.RefCourtNavigation).WithMany(p => p.Vouchers)
                .HasForeignKey(d => d.RefCourt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__vouchers__ref_co__59FA5E80");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
