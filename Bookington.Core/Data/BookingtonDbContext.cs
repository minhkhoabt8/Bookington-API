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

    public virtual DbSet<Ad> Ads { get; set; }

    public virtual DbSet<Ban> Bans { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<ChatMessage> ChatMessages { get; set; }

    public virtual DbSet<ChatRoom> ChatRooms { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Competition> Competitions { get; set; }

    public virtual DbSet<CompetitionMatch> CompetitionMatches { get; set; }

    public virtual DbSet<Court> Courts { get; set; }

    public virtual DbSet<CourtImage> CourtImages { get; set; }

    public virtual DbSet<CourtReport> CourtReports { get; set; }

    public virtual DbSet<CourtReportResponse> CourtReportResponses { get; set; }

    public virtual DbSet<CourtType> CourtTypes { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<LoginToken> LoginTokens { get; set; }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<MatchTeam> MatchTeams { get; set; }

    public virtual DbSet<MomoTransaction> MomoTransactions { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Round> Rounds { get; set; }

    public virtual DbSet<Slot> Slots { get; set; }

    public virtual DbSet<SubCourt> SubCourts { get; set; }

    public virtual DbSet<SubCourtSlot> SubCourtSlots { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamPlayer> TeamPlayers { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<UserBalance> UserBalances { get; set; }

    public virtual DbSet<UserReport> UserReports { get; set; }

    public virtual DbSet<UserReportResponse> UserReportResponses { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__accounts__3213E83F393BC9A8");

            entity.ToTable("accounts");

            entity.HasIndex(e => e.Phone, "UQ__accounts__B43B145F44FBBEF1").IsUnique();

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
            entity.Property(e => e.IsVerified).HasColumnName("is_verified");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.RefAvatar)
                .IsRequired()
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("ref_avatar");
            entity.Property(e => e.RoleId)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__accounts__role_i__1367E606");
        });

        modelBuilder.Entity<AccountOtp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__account___3213E83F6037FD48");

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
                .IsRequired()
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("otp_code");
            entity.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.RefAccount)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_account");

            entity.HasOne(d => d.RefAccountNavigation).WithMany(p => p.AccountOtps)
                .HasForeignKey(d => d.RefAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__account_o__ref_a__164452B1");
        });

        modelBuilder.Entity<Ad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ads__3213E83F86BD17F5");

            entity.ToTable("ads");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.AdLink)
                .IsRequired()
                .HasMaxLength(2048)
                .IsUnicode(false)
                .HasColumnName("ad_link");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("end_time");
            entity.Property(e => e.IsCourtAd).HasColumnName("is_court_ad");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.PromotionOrder).HasColumnName("promotion_order");
            entity.Property(e => e.RefCourt)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_court");
            entity.Property(e => e.RefImage)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_image");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("start_time");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("title");

            entity.HasOne(d => d.RefCourtNavigation).WithMany(p => p.Ads)
                .HasForeignKey(d => d.RefCourt)
                .HasConstraintName("FK__ads__ref_court__68487DD7");
        });

        modelBuilder.Entity<Ban>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__bans__3213E83FE4BD91AE");

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
                .IsRequired()
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
                .HasConstraintName("FK__bans__ref_accoun__6B24EA82");

            entity.HasOne(d => d.RefCourtNavigation).WithMany(p => p.Bans)
                .HasForeignKey(d => d.RefCourt)
                .HasConstraintName("FK__bans__ref_court__6C190EBB");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__bookings__3213E83F8ABDA863");

            entity.ToTable("bookings");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.BookAt)
                .HasColumnType("datetime")
                .HasColumnName("book_at");
            entity.Property(e => e.BookBy)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("book_by");
            entity.Property(e => e.PlayDate)
                .HasColumnType("date")
                .HasColumnName("play_date");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.RefOrder)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_order");
            entity.Property(e => e.RefSlot)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_slot");
            entity.Property(e => e.RefSubCourt)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_sub_court");

            entity.HasOne(d => d.BookByNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.BookBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__bookings__book_b__571DF1D5");

            entity.HasOne(d => d.RefOrderNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.RefOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__bookings__ref_or__5629CD9C");

            entity.HasOne(d => d.RefSlotNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.RefSlot)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__bookings__ref_sl__5441852A");

            entity.HasOne(d => d.RefSubCourtNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.RefSubCourt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__bookings__ref_su__5535A963");
        });

        modelBuilder.Entity<ChatMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__chat_mes__3213E83FB735D6A6");

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
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_chatroom");
            entity.Property(e => e.RefOwner)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_owner");
            entity.Property(e => e.RefUser)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_user");
            entity.Property(e => e.SequenceOrder).HasColumnName("sequence_order");

            entity.HasOne(d => d.RefChatroomNavigation).WithMany(p => p.ChatMessages)
                .HasForeignKey(d => d.RefChatroom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__chat_mess__ref_c__5DCAEF64");

            entity.HasOne(d => d.RefOwnerNavigation).WithMany(p => p.ChatMessageRefOwnerNavigations)
                .HasForeignKey(d => d.RefOwner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__chat_mess__ref_o__5EBF139D");

            entity.HasOne(d => d.RefUserNavigation).WithMany(p => p.ChatMessageRefUserNavigations)
                .HasForeignKey(d => d.RefUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__chat_mess__ref_u__5FB337D6");
        });

        modelBuilder.Entity<ChatRoom>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__chat_roo__3213E83FCE8DCA3E");

            entity.ToTable("chat_rooms");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.RefOwner)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_owner");
            entity.Property(e => e.RefUser)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_user");

            entity.HasOne(d => d.RefOwnerNavigation).WithMany(p => p.ChatRoomRefOwnerNavigations)
                .HasForeignKey(d => d.RefOwner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__chat_room__ref_o__59FA5E80");

            entity.HasOne(d => d.RefUserNavigation).WithMany(p => p.ChatRoomRefUserNavigations)
                .HasForeignKey(d => d.RefUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__chat_room__ref_u__5AEE82B9");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__comments__3213E83F09B12959");

            entity.ToTable("comments");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.CommentWriterId)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("comment_writer_id");
            entity.Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("content");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.RefCourt)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_court");

            entity.HasOne(d => d.CommentWriter).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CommentWriterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__comments__commen__276EDEB3");

            entity.HasOne(d => d.RefCourtNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.RefCourt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__comments__ref_co__286302EC");
        });

        modelBuilder.Entity<Competition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__competit__3213E83F2C7BD37E");

            entity.ToTable("competitions");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.CompetitionCode)
                .IsRequired()
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("competition_code");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(2500)
                .HasColumnName("description");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.HostBy)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("host_by");
            entity.Property(e => e.IsStarted).HasColumnName("is_started");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("name");
            entity.Property(e => e.NumOfTeamsAllowed).HasColumnName("num_of_teams_allowed");
            entity.Property(e => e.RegisterDeadline)
                .HasColumnType("datetime")
                .HasColumnName("register_deadline");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");

            entity.HasOne(d => d.HostByNavigation).WithMany(p => p.Competitions)
                .HasForeignKey(d => d.HostBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__competiti__host___72C60C4A");
        });

        modelBuilder.Entity<CompetitionMatch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__competit__3213E83FCA405313");

            entity.ToTable("competition_matches");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.MatchPosition).HasColumnName("match_position");
            entity.Property(e => e.RefCompetition)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_competition");
            entity.Property(e => e.RefMatch)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_match");

            entity.HasOne(d => d.RefCompetitionNavigation).WithMany(p => p.CompetitionMatches)
                .HasForeignKey(d => d.RefCompetition)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__competiti__ref_c__75A278F5");

            entity.HasOne(d => d.RefMatchNavigation).WithMany(p => p.CompetitionMatches)
                .HasForeignKey(d => d.RefMatch)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__competiti__ref_m__76969D2E");
        });

        modelBuilder.Entity<Court>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__courts__3213E83F5F44C448");

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
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("district_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnName("name");
            entity.Property(e => e.OpenAt).HasColumnName("open_at");
            entity.Property(e => e.OwnerId)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("owner_id");

            entity.HasOne(d => d.District).WithMany(p => p.Courts)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__courts__district__21B6055D");

            entity.HasOne(d => d.Owner).WithMany(p => p.Courts)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__courts__owner_id__20C1E124");
        });

        modelBuilder.Entity<CourtImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__court_im__3213E83FB758713D");

            entity.ToTable("court_images");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.CourtId)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("court_id");
            entity.Property(e => e.RefImage)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_image");

            entity.HasOne(d => d.Court).WithMany(p => p.CourtImages)
                .HasForeignKey(d => d.CourtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__court_ima__court__24927208");
        });

        modelBuilder.Entity<CourtReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__court_re__3213E83F8E09D7E2");

            entity.ToTable("court_reports");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(1000)
                .HasColumnName("content");
            entity.Property(e => e.IsResponded).HasColumnName("is_responded");
            entity.Property(e => e.RefCourt)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_court");
            entity.Property(e => e.RefResponse)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_response");
            entity.Property(e => e.ReporterId)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("reporter_id");

            entity.HasOne(d => d.RefCourtNavigation).WithMany(p => p.CourtReports)
                .HasForeignKey(d => d.RefCourt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__court_rep__ref_c__2D27B809");

            entity.HasOne(d => d.RefResponseNavigation).WithMany(p => p.CourtReports)
                .HasForeignKey(d => d.RefResponse)
                .HasConstraintName("FK__court_rep__ref_r__2F10007B");

            entity.HasOne(d => d.Reporter).WithMany(p => p.CourtReports)
                .HasForeignKey(d => d.ReporterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__court_rep__repor__2E1BDC42");
        });

        modelBuilder.Entity<CourtReportResponse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__court_re__3213E83F329F5920");

            entity.ToTable("court_report_responses");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(1000)
                .HasColumnName("content");
        });

        modelBuilder.Entity<CourtType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__court_ty__3213E83FB35D9C49");

            entity.ToTable("court_types");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("content");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__district__3213E83F606203EF");

            entity.ToTable("districts");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.DistrictName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("district_name");
            entity.Property(e => e.ProvinceId)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("province_id");

            entity.HasOne(d => d.Province).WithMany(p => p.Districts)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__districts__provi__1DE57479");
        });

        modelBuilder.Entity<LoginToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__login_to__3213E83F6000D1A1");

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
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_account");
            entity.Property(e => e.RefreshToken)
                .IsRequired()
                .HasMaxLength(2048)
                .IsUnicode(false)
                .HasColumnName("refresh_token");
            entity.Property(e => e.Token)
                .IsRequired()
                .HasMaxLength(2048)
                .IsUnicode(false)
                .HasColumnName("token");

            entity.HasOne(d => d.RefAccountNavigation).WithMany(p => p.LoginTokens)
                .HasForeignKey(d => d.RefAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__login_tok__ref_a__1920BF5C");
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__matches__3213E83FDC026F16");

            entity.ToTable("matches");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.HostBy)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("host_by");
            entity.Property(e => e.IsPaymentSplitted).HasColumnName("is_payment_splitted");
            entity.Property(e => e.MatchCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("match_code");
            entity.Property(e => e.NumOfPlayersAllowed).HasColumnName("num_of_players_allowed");
            entity.Property(e => e.NumOfRounds).HasColumnName("num_of_rounds");
            entity.Property(e => e.RefBooking)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_booking");

            entity.HasOne(d => d.HostByNavigation).WithMany(p => p.Matches)
                .HasForeignKey(d => d.HostBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__matches__host_by__6EF57B66");

            entity.HasOne(d => d.RefBookingNavigation).WithMany(p => p.Matches)
                .HasForeignKey(d => d.RefBooking)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__matches__ref_boo__6FE99F9F");
        });

        modelBuilder.Entity<MatchTeam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__match_te__3213E83F4E7B9B51");

            entity.ToTable("match_teams");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.RefMatch)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_match");
            entity.Property(e => e.RefTeam)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_team");

            entity.HasOne(d => d.RefMatchNavigation).WithMany(p => p.MatchTeams)
                .HasForeignKey(d => d.RefMatch)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__match_tea__ref_m__7D439ABD");

            entity.HasOne(d => d.RefTeamNavigation).WithMany(p => p.MatchTeams)
                .HasForeignKey(d => d.RefTeam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__match_tea__ref_t__7E37BEF6");
        });

        modelBuilder.Entity<MomoTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__momo_tra__3213E83F2ED56F05");

            entity.ToTable("momo_transactions");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(1000)
                .HasColumnName("content");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.IsSuccessful).HasColumnName("is_successful");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__notifica__3213E83FFC24849F");

            entity.ToTable("notifications");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("content");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.IsRead).HasColumnName("is_read");
            entity.Property(e => e.RefAccount)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_account");

            entity.HasOne(d => d.RefAccountNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.RefAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__notificat__ref_a__656C112C");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__orders__3213E83FE013195E");

            entity.ToTable("orders");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("create_by");
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

            entity.HasOne(d => d.CreateByNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CreateBy)
                .HasConstraintName("FK__orders__create_b__4F7CD00D");

            entity.HasOne(d => d.Transaction).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TransactionId)
                .HasConstraintName("FK__orders__transact__5070F446");

            entity.HasOne(d => d.VoucherCodeNavigation).WithMany(p => p.Orders)
                .HasPrincipalKey(p => p.VoucherCode)
                .HasForeignKey(d => d.VoucherCode)
                .HasConstraintName("FK__orders__voucher___5165187F");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__province__3213E83F92E9AB10");

            entity.ToTable("provinces");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.ProvinceName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("province_name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__roles__3213E83F46B698E0");

            entity.ToTable("roles");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.RoleName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Round>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__rounds__3213E83F8900338C");

            entity.ToTable("rounds");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Point).HasColumnName("point");
            entity.Property(e => e.RefMatch)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_match");
            entity.Property(e => e.RefTeam)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_team");
            entity.Property(e => e.RoundNum).HasColumnName("round_num");

            entity.HasOne(d => d.RefMatchNavigation).WithMany(p => p.Rounds)
                .HasForeignKey(d => d.RefMatch)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__rounds__ref_matc__01142BA1");

            entity.HasOne(d => d.RefTeamNavigation).WithMany(p => p.Rounds)
                .HasForeignKey(d => d.RefTeam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__rounds__ref_team__02084FDA");
        });

        modelBuilder.Entity<Slot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__slots__3213E83F48F38810");

            entity.ToTable("slots");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.DaysInSchedule)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("days_in_schedule");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
        });

        modelBuilder.Entity<SubCourt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sub_cour__3213E83F88C2E170");

            entity.ToTable("sub_courts");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.CourtTypeId)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("court_type_id");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.ParentCourtId)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("parent_court_id");

            entity.HasOne(d => d.CourtType).WithMany(p => p.SubCourts)
                .HasForeignKey(d => d.CourtTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__sub_court__court__3B75D760");

            entity.HasOne(d => d.ParentCourt).WithMany(p => p.SubCourts)
                .HasForeignKey(d => d.ParentCourtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__sub_court__paren__3A81B327");
        });

        modelBuilder.Entity<SubCourtSlot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sub_cour__3213E83F0AFCE4F2");

            entity.ToTable("sub_court_slots");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.RefSlot)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_slot");
            entity.Property(e => e.RefSubCourt)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_sub_court");

            entity.HasOne(d => d.RefSlotNavigation).WithMany(p => p.SubCourtSlots)
                .HasForeignKey(d => d.RefSlot)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__sub_court__ref_s__412EB0B6");

            entity.HasOne(d => d.RefSubCourtNavigation).WithMany(p => p.SubCourtSlots)
                .HasForeignKey(d => d.RefSubCourt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__sub_court__ref_s__403A8C7D");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__teams__3213E83FD47FBF42");

            entity.ToTable("teams");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.IsCompetitionTeam).HasColumnName("is_competition_team");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.RefCompetition)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_competition");
            entity.Property(e => e.RefMatch)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_match");
            entity.Property(e => e.TeamCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("team_code");

            entity.HasOne(d => d.RefCompetitionNavigation).WithMany(p => p.Teams)
                .HasForeignKey(d => d.RefCompetition)
                .HasConstraintName("FK__teams__ref_compe__7A672E12");

            entity.HasOne(d => d.RefMatchNavigation).WithMany(p => p.Teams)
                .HasForeignKey(d => d.RefMatch)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__teams__ref_match__797309D9");
        });

        modelBuilder.Entity<TeamPlayer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__team_pla__3213E83FA30C6557");

            entity.ToTable("team_players");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.RefAccount)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_account");
            entity.Property(e => e.RefTeam)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_team");

            entity.HasOne(d => d.RefAccountNavigation).WithMany(p => p.TeamPlayers)
                .HasForeignKey(d => d.RefAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__team_play__ref_a__05D8E0BE");

            entity.HasOne(d => d.RefTeamNavigation).WithMany(p => p.TeamPlayers)
                .HasForeignKey(d => d.RefTeam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__team_play__ref_t__04E4BC85");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__transact__3213E83FD7A221E7");

            entity.ToTable("transactions");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.Reason)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("reason");
            entity.Property(e => e.RefFrom)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_from");
            entity.Property(e => e.RefMomoTransaction)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_momo_transaction");
            entity.Property(e => e.RefTo)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_to");

            entity.HasOne(d => d.RefFromNavigation).WithMany(p => p.TransactionRefFromNavigations)
                .HasForeignKey(d => d.RefFrom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__transacti__ref_f__4AB81AF0");

            entity.HasOne(d => d.RefMomoTransactionNavigation).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.RefMomoTransaction)
                .HasConstraintName("FK__transacti__ref_m__4CA06362");

            entity.HasOne(d => d.RefToNavigation).WithMany(p => p.TransactionRefToNavigations)
                .HasForeignKey(d => d.RefTo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__transacti__ref_t__4BAC3F29");
        });

        modelBuilder.Entity<UserBalance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_bal__3213E83FB611A5CA");

            entity.ToTable("user_balances");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Balance).HasColumnName("balance");
            entity.Property(e => e.RefUser)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_user");

            entity.HasOne(d => d.RefUserNavigation).WithMany(p => p.UserBalances)
                .HasForeignKey(d => d.RefUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_bala__ref_u__628FA481");
        });

        modelBuilder.Entity<UserReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_rep__3213E83FD4FB5B21");

            entity.ToTable("user_reports");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(1000)
                .HasColumnName("content");
            entity.Property(e => e.IsResponded).HasColumnName("is_responded");
            entity.Property(e => e.RefResponse)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_response");
            entity.Property(e => e.RefUser)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_user");
            entity.Property(e => e.ReporterId)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("reporter_id");

            entity.HasOne(d => d.RefResponseNavigation).WithMany(p => p.UserReports)
                .HasForeignKey(d => d.RefResponse)
                .HasConstraintName("FK__user_repo__ref_r__35BCFE0A");

            entity.HasOne(d => d.RefUserNavigation).WithMany(p => p.UserReportRefUserNavigations)
                .HasForeignKey(d => d.RefUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_repo__ref_u__33D4B598");

            entity.HasOne(d => d.Reporter).WithMany(p => p.UserReportReporters)
                .HasForeignKey(d => d.ReporterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_repo__repor__34C8D9D1");
        });

        modelBuilder.Entity<UserReportResponse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_rep__3213E83FC4202445");

            entity.ToTable("user_report_responses");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(1000)
                .HasColumnName("content");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vouchers__3213E83FEBC9EDD2");

            entity.ToTable("vouchers");

            entity.HasIndex(e => e.VoucherCode, "UQ__vouchers__217310692F35667C").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.CreateBy)
                .IsRequired()
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
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ref_court");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("title");
            entity.Property(e => e.Usages).HasColumnName("usages");
            entity.Property(e => e.VoucherCode)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("voucher_code");

            entity.HasOne(d => d.CreateByNavigation).WithMany(p => p.Vouchers)
                .HasForeignKey(d => d.CreateBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__vouchers__create__44FF419A");

            entity.HasOne(d => d.RefCourtNavigation).WithMany(p => p.Vouchers)
                .HasForeignKey(d => d.RefCourt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__vouchers__ref_co__45F365D3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
