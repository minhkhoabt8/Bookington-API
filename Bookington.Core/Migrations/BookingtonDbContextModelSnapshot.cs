﻿// <auto-generated />
using System;
using Bookington.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bookington.Core.Migrations
{
    [DbContext(typeof(BookingtonDbContext))]
    partial class BookingtonDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Bookington.Core.Entities.Account", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("id");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime")
                        .HasColumnName("create_at");

                    b.Property<string>("FullName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("full_name");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("is_active");

                    b.Property<bool?>("IsConfirmed")
                        .HasColumnType("bit")
                        .HasColumnName("is_confirmed");

                    b.Property<string>("Phone")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("phone");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.HasKey("Id")
                        .HasName("PK__accounts__3213E83F6FD15E99");

                    b.HasIndex("RoleId");

                    b.ToTable("accounts", (string)null);
                });

            modelBuilder.Entity("Bookington.Core.Entities.Booking", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("id");

                    b.Property<DateTime?>("BookAt")
                        .HasColumnType("datetime")
                        .HasColumnName("book_at");

                    b.Property<string>("BookBy")
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("book_by");

                    b.Property<bool?>("IsCanceled")
                        .HasColumnType("bit")
                        .HasColumnName("is_canceled");

                    b.Property<bool?>("IsPaid")
                        .HasColumnType("bit")
                        .HasColumnName("is_paid");

                    b.Property<bool?>("IsRefunded")
                        .HasColumnType("bit")
                        .HasColumnName("is_refunded");

                    b.Property<double?>("OriginalPrice")
                        .HasColumnType("float")
                        .HasColumnName("original_price");

                    b.Property<double?>("Price")
                        .HasColumnType("float")
                        .HasColumnName("price");

                    b.Property<string>("RefSlot")
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("ref_slot");

                    b.Property<string>("VoucherCode")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("voucher_code");

                    b.HasKey("Id")
                        .HasName("PK__bookings__3213E83F85237411");

                    b.HasIndex("BookBy");

                    b.HasIndex("RefSlot");

                    b.HasIndex("VoucherCode");

                    b.ToTable("bookings", (string)null);
                });

            modelBuilder.Entity("Bookington.Core.Entities.Comment", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("id");

                    b.Property<string>("CommentWriterId")
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("comment_writer_id");

                    b.Property<string>("Content")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("content");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime")
                        .HasColumnName("create_at");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("is_active");

                    b.Property<double?>("Rating")
                        .HasColumnType("float")
                        .HasColumnName("rating");

                    b.Property<string>("RefCourt")
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("ref_court");

                    b.HasKey("Id")
                        .HasName("PK__comments__3213E83F93EC1A00");

                    b.HasIndex("CommentWriterId");

                    b.HasIndex("RefCourt");

                    b.ToTable("comments", (string)null);
                });

            modelBuilder.Entity("Bookington.Core.Entities.Court", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("address");

                    b.Property<TimeSpan?>("CloseAt")
                        .HasColumnType("time")
                        .HasColumnName("close_at");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime")
                        .HasColumnName("create_at");

                    b.Property<int?>("DistrictId")
                        .HasColumnType("int")
                        .HasColumnName("district_id");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("is_active");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.Property<TimeSpan?>("OpenAt")
                        .HasColumnType("time")
                        .HasColumnName("open_at");

                    b.Property<string>("OwnerId")
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("owner_id");

                    b.HasKey("Id")
                        .HasName("PK__courts__3213E83F624A1B4C");

                    b.HasIndex("DistrictId");

                    b.HasIndex("OwnerId");

                    b.ToTable("courts", (string)null);
                });

            modelBuilder.Entity("Bookington.Core.Entities.CourtImage", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("CourtId")
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("court_id");

                    b.Property<byte[]>("ImageBinary")
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("image_binary");

                    b.HasKey("Id")
                        .HasName("PK__court_im__3213E83F89DED088");

                    b.HasIndex("CourtId");

                    b.ToTable("court_images", (string)null);
                });

            modelBuilder.Entity("Bookington.Core.Entities.CourtType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("content");

                    b.HasKey("Id")
                        .HasName("PK__court_ty__3213E83F78EAFB02");

                    b.ToTable("court_types", (string)null);
                });

            modelBuilder.Entity("Bookington.Core.Entities.District", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("DistrictName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("district_name");

                    b.Property<int?>("ProvinceId")
                        .HasColumnType("int")
                        .HasColumnName("province_id");

                    b.HasKey("Id")
                        .HasName("PK__district__3213E83F37D2778E");

                    b.HasIndex("ProvinceId");

                    b.ToTable("districts", (string)null);
                });

            modelBuilder.Entity("Bookington.Core.Entities.Order", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("id");

                    b.Property<string>("BookingRef")
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("booking_ref");

                    b.Property<DateTime?>("OrderAt")
                        .HasColumnType("datetime")
                        .HasColumnName("order_at");

                    b.Property<double?>("Price")
                        .HasColumnType("float")
                        .HasColumnName("price");

                    b.HasKey("Id")
                        .HasName("PK__orders__3213E83FA956F20D");

                    b.HasIndex("BookingRef");

                    b.ToTable("orders", (string)null);
                });

            modelBuilder.Entity("Bookington.Core.Entities.Province", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("ProvinceName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("province_name");

                    b.HasKey("Id")
                        .HasName("PK__province__3213E83FC73BA5EC");

                    b.ToTable("provinces", (string)null);
                });

            modelBuilder.Entity("Bookington.Core.Entities.Report", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("content");

                    b.Property<string>("ReporterId")
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("reporter_id");

                    b.Property<int?>("TypeId")
                        .HasColumnType("int")
                        .HasColumnName("type_id");

                    b.HasKey("Id")
                        .HasName("PK__reports__3213E83F1AB187A1");

                    b.HasIndex("ReporterId");

                    b.HasIndex("TypeId");

                    b.ToTable("reports", (string)null);
                });

            modelBuilder.Entity("Bookington.Core.Entities.ReportType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("ReportName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("report_name");

                    b.HasKey("Id")
                        .HasName("PK__report_t__3213E83F0427123E");

                    b.ToTable("report_types", (string)null);
                });

            modelBuilder.Entity("Bookington.Core.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("RoleName")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("role_name");

                    b.HasKey("Id")
                        .HasName("PK__roles__3213E83F59CB7009");

                    b.ToTable("roles", (string)null);
                });

            modelBuilder.Entity("Bookington.Core.Entities.Slot", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("id");

                    b.Property<TimeSpan?>("EndTime")
                        .HasColumnType("time")
                        .HasColumnName("end_time");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("is_active");

                    b.Property<string>("RefSubCourt")
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("ref_sub_court");

                    b.Property<TimeSpan?>("StartTime")
                        .HasColumnType("time")
                        .HasColumnName("start_time");

                    b.HasKey("Id")
                        .HasName("PK__slots__3213E83FFD788983");

                    b.HasIndex("RefSubCourt");

                    b.ToTable("slots", (string)null);
                });

            modelBuilder.Entity("Bookington.Core.Entities.SubCourt", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("id");

                    b.Property<int?>("CourtTypeId")
                        .HasColumnType("int")
                        .HasColumnName("court_type_id");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime")
                        .HasColumnName("create_at");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("is_active");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<string>("ParentCourtId")
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("parent_court_id");

                    b.HasKey("Id")
                        .HasName("PK__sub_cour__3213E83F41A47DE2");

                    b.HasIndex("CourtTypeId");

                    b.HasIndex("ParentCourtId");

                    b.ToTable("sub_courts", (string)null);
                });

            modelBuilder.Entity("Bookington.Core.Entities.Voucher", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("id");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime")
                        .HasColumnName("create_at");

                    b.Property<string>("CreateBy")
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("create_by");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("description");

                    b.Property<double?>("Discount")
                        .HasColumnType("float")
                        .HasColumnName("discount");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime")
                        .HasColumnName("end_date");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("is_active");

                    b.Property<int?>("MaxQuantity")
                        .HasColumnType("int")
                        .HasColumnName("max_quantity");

                    b.Property<string>("RefCourt")
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("ref_court");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime")
                        .HasColumnName("start_date");

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("title");

                    b.Property<int?>("Usages")
                        .HasColumnType("int")
                        .HasColumnName("usages");

                    b.Property<string>("VoucherCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("voucher_code");

                    b.HasKey("Id")
                        .HasName("PK__vouchers__3213E83F4F3F0063");

                    b.HasIndex("CreateBy");

                    b.HasIndex("RefCourt");

                    b.HasIndex(new[] { "VoucherCode" }, "UQ__vouchers__2173106953AA8CF3")
                        .IsUnique();

                    b.ToTable("vouchers", (string)null);
                });

            modelBuilder.Entity("Bookington.Core.Entities.Account", b =>
                {
                    b.HasOne("Bookington.Core.Entities.Role", "Role")
                        .WithMany("Accounts")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK__accounts__role_i__267ABA7A");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Bookington.Core.Entities.Booking", b =>
                {
                    b.HasOne("Bookington.Core.Entities.Account", "BookByNavigation")
                        .WithMany("Bookings")
                        .HasForeignKey("BookBy")
                        .HasConstraintName("FK__bookings__book_b__4CA06362");

                    b.HasOne("Bookington.Core.Entities.Slot", "RefSlotNavigation")
                        .WithMany("Bookings")
                        .HasForeignKey("RefSlot")
                        .HasConstraintName("FK__bookings__ref_sl__4BAC3F29");

                    b.HasOne("Bookington.Core.Entities.Voucher", "VoucherCodeNavigation")
                        .WithMany("Bookings")
                        .HasForeignKey("VoucherCode")
                        .HasPrincipalKey("VoucherCode")
                        .HasConstraintName("FK__bookings__vouche__4D94879B");

                    b.Navigation("BookByNavigation");

                    b.Navigation("RefSlotNavigation");

                    b.Navigation("VoucherCodeNavigation");
                });

            modelBuilder.Entity("Bookington.Core.Entities.Comment", b =>
                {
                    b.HasOne("Bookington.Core.Entities.Account", "CommentWriter")
                        .WithMany("Comments")
                        .HasForeignKey("CommentWriterId")
                        .HasConstraintName("FK__comments__commen__34C8D9D1");

                    b.HasOne("Bookington.Core.Entities.Court", "RefCourtNavigation")
                        .WithMany("Comments")
                        .HasForeignKey("RefCourt")
                        .HasConstraintName("FK__comments__ref_co__35BCFE0A");

                    b.Navigation("CommentWriter");

                    b.Navigation("RefCourtNavigation");
                });

            modelBuilder.Entity("Bookington.Core.Entities.Court", b =>
                {
                    b.HasOne("Bookington.Core.Entities.District", "District")
                        .WithMany("Courts")
                        .HasForeignKey("DistrictId")
                        .HasConstraintName("FK__courts__district__2F10007B");

                    b.HasOne("Bookington.Core.Entities.Account", "Owner")
                        .WithMany("Courts")
                        .HasForeignKey("OwnerId")
                        .HasConstraintName("FK__courts__owner_id__2E1BDC42");

                    b.Navigation("District");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Bookington.Core.Entities.CourtImage", b =>
                {
                    b.HasOne("Bookington.Core.Entities.Court", "Court")
                        .WithMany("CourtImages")
                        .HasForeignKey("CourtId")
                        .HasConstraintName("FK__court_ima__court__31EC6D26");

                    b.Navigation("Court");
                });

            modelBuilder.Entity("Bookington.Core.Entities.District", b =>
                {
                    b.HasOne("Bookington.Core.Entities.Province", "Province")
                        .WithMany("Districts")
                        .HasForeignKey("ProvinceId")
                        .HasConstraintName("FK__districts__provi__2B3F6F97");

                    b.Navigation("Province");
                });

            modelBuilder.Entity("Bookington.Core.Entities.Order", b =>
                {
                    b.HasOne("Bookington.Core.Entities.Booking", "BookingRefNavigation")
                        .WithMany("Orders")
                        .HasForeignKey("BookingRef")
                        .HasConstraintName("FK__orders__booking___5070F446");

                    b.Navigation("BookingRefNavigation");
                });

            modelBuilder.Entity("Bookington.Core.Entities.Report", b =>
                {
                    b.HasOne("Bookington.Core.Entities.Account", "Reporter")
                        .WithMany("Reports")
                        .HasForeignKey("ReporterId")
                        .HasConstraintName("FK__reports__reporte__3B75D760");

                    b.HasOne("Bookington.Core.Entities.ReportType", "Type")
                        .WithMany("Reports")
                        .HasForeignKey("TypeId")
                        .HasConstraintName("FK__reports__type_id__3A81B327");

                    b.Navigation("Reporter");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Bookington.Core.Entities.Slot", b =>
                {
                    b.HasOne("Bookington.Core.Entities.SubCourt", "RefSubCourtNavigation")
                        .WithMany("Slots")
                        .HasForeignKey("RefSubCourt")
                        .HasConstraintName("FK__slots__ref_sub_c__440B1D61");

                    b.Navigation("RefSubCourtNavigation");
                });

            modelBuilder.Entity("Bookington.Core.Entities.SubCourt", b =>
                {
                    b.HasOne("Bookington.Core.Entities.CourtType", "CourtType")
                        .WithMany("SubCourts")
                        .HasForeignKey("CourtTypeId")
                        .HasConstraintName("FK__sub_court__court__412EB0B6");

                    b.HasOne("Bookington.Core.Entities.Court", "ParentCourt")
                        .WithMany("SubCourts")
                        .HasForeignKey("ParentCourtId")
                        .HasConstraintName("FK__sub_court__paren__403A8C7D");

                    b.Navigation("CourtType");

                    b.Navigation("ParentCourt");
                });

            modelBuilder.Entity("Bookington.Core.Entities.Voucher", b =>
                {
                    b.HasOne("Bookington.Core.Entities.Account", "CreateByNavigation")
                        .WithMany("Vouchers")
                        .HasForeignKey("CreateBy")
                        .HasConstraintName("FK__vouchers__create__47DBAE45");

                    b.HasOne("Bookington.Core.Entities.Court", "RefCourtNavigation")
                        .WithMany("Vouchers")
                        .HasForeignKey("RefCourt")
                        .HasConstraintName("FK__vouchers__ref_co__48CFD27E");

                    b.Navigation("CreateByNavigation");

                    b.Navigation("RefCourtNavigation");
                });

            modelBuilder.Entity("Bookington.Core.Entities.Account", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Comments");

                    b.Navigation("Courts");

                    b.Navigation("Reports");

                    b.Navigation("Vouchers");
                });

            modelBuilder.Entity("Bookington.Core.Entities.Booking", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Bookington.Core.Entities.Court", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("CourtImages");

                    b.Navigation("SubCourts");

                    b.Navigation("Vouchers");
                });

            modelBuilder.Entity("Bookington.Core.Entities.CourtType", b =>
                {
                    b.Navigation("SubCourts");
                });

            modelBuilder.Entity("Bookington.Core.Entities.District", b =>
                {
                    b.Navigation("Courts");
                });

            modelBuilder.Entity("Bookington.Core.Entities.Province", b =>
                {
                    b.Navigation("Districts");
                });

            modelBuilder.Entity("Bookington.Core.Entities.ReportType", b =>
                {
                    b.Navigation("Reports");
                });

            modelBuilder.Entity("Bookington.Core.Entities.Role", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("Bookington.Core.Entities.Slot", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("Bookington.Core.Entities.SubCourt", b =>
                {
                    b.Navigation("Slots");
                });

            modelBuilder.Entity("Bookington.Core.Entities.Voucher", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}