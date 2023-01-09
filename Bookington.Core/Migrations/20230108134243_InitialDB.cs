using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookington.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "court_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    content = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__court_ty__3213E83F78EAFB02", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "provinces",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    provincename = table.Column<string>(name: "province_name", type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__province__3213E83FC73BA5EC", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "report_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    reportname = table.Column<string>(name: "report_name", type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__report_t__3213E83F0427123E", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    rolename = table.Column<string>(name: "role_name", type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__roles__3213E83F59CB7009", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "districts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    provinceid = table.Column<int>(name: "province_id", type: "int", nullable: true),
                    districtname = table.Column<string>(name: "district_name", type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__district__3213E83F37D2778E", x => x.id);
                    table.ForeignKey(
                        name: "FK__districts__provi__2B3F6F97",
                        column: x => x.provinceid,
                        principalTable: "provinces",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    roleid = table.Column<int>(name: "role_id", type: "int", nullable: true),
                    phone = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    fullname = table.Column<string>(name: "full_name", type: "nvarchar(50)", maxLength: 50, nullable: true),
                    createat = table.Column<DateTime>(name: "create_at", type: "datetime", nullable: true),
                    isconfirmed = table.Column<bool>(name: "is_confirmed", type: "bit", nullable: true),
                    isactive = table.Column<bool>(name: "is_active", type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__accounts__3213E83F6FD15E99", x => x.id);
                    table.ForeignKey(
                        name: "FK__accounts__role_i__267ABA7A",
                        column: x => x.roleid,
                        principalTable: "roles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "courts",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    ownerid = table.Column<string>(name: "owner_id", type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    districtid = table.Column<int>(name: "district_id", type: "int", nullable: true),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    openat = table.Column<TimeSpan>(name: "open_at", type: "time", nullable: true),
                    closeat = table.Column<TimeSpan>(name: "close_at", type: "time", nullable: true),
                    createat = table.Column<DateTime>(name: "create_at", type: "datetime", nullable: true),
                    isactive = table.Column<bool>(name: "is_active", type: "bit", nullable: true),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__courts__3213E83F624A1B4C", x => x.id);
                    table.ForeignKey(
                        name: "FK__courts__district__2F10007B",
                        column: x => x.districtid,
                        principalTable: "districts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__courts__owner_id__2E1BDC42",
                        column: x => x.ownerid,
                        principalTable: "accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "reports",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    typeid = table.Column<int>(name: "type_id", type: "int", nullable: true),
                    reporterid = table.Column<string>(name: "reporter_id", type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__reports__3213E83F1AB187A1", x => x.id);
                    table.ForeignKey(
                        name: "FK__reports__reporte__3B75D760",
                        column: x => x.reporterid,
                        principalTable: "accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__reports__type_id__3A81B327",
                        column: x => x.typeid,
                        principalTable: "report_types",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    commentwriterid = table.Column<string>(name: "comment_writer_id", type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    refcourt = table.Column<string>(name: "ref_court", type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    rating = table.Column<double>(type: "float", nullable: true),
                    createat = table.Column<DateTime>(name: "create_at", type: "datetime", nullable: true),
                    isactive = table.Column<bool>(name: "is_active", type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__comments__3213E83F93EC1A00", x => x.id);
                    table.ForeignKey(
                        name: "FK__comments__commen__34C8D9D1",
                        column: x => x.commentwriterid,
                        principalTable: "accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__comments__ref_co__35BCFE0A",
                        column: x => x.refcourt,
                        principalTable: "courts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "court_images",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    courtid = table.Column<string>(name: "court_id", type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    imagebinary = table.Column<byte[]>(name: "image_binary", type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__court_im__3213E83F89DED088", x => x.id);
                    table.ForeignKey(
                        name: "FK__court_ima__court__31EC6D26",
                        column: x => x.courtid,
                        principalTable: "courts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "sub_courts",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    parentcourtid = table.Column<string>(name: "parent_court_id", type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    courttypeid = table.Column<int>(name: "court_type_id", type: "int", nullable: true),
                    createat = table.Column<DateTime>(name: "create_at", type: "datetime", nullable: true),
                    isactive = table.Column<bool>(name: "is_active", type: "bit", nullable: true),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__sub_cour__3213E83F41A47DE2", x => x.id);
                    table.ForeignKey(
                        name: "FK__sub_court__court__412EB0B6",
                        column: x => x.courttypeid,
                        principalTable: "court_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__sub_court__paren__403A8C7D",
                        column: x => x.parentcourtid,
                        principalTable: "courts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "vouchers",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    createby = table.Column<string>(name: "create_by", type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    refcourt = table.Column<string>(name: "ref_court", type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    vouchercode = table.Column<string>(name: "voucher_code", type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    discount = table.Column<double>(type: "float", nullable: true),
                    usages = table.Column<int>(type: "int", nullable: true),
                    maxquantity = table.Column<int>(name: "max_quantity", type: "int", nullable: true),
                    startdate = table.Column<DateTime>(name: "start_date", type: "datetime", nullable: true),
                    enddate = table.Column<DateTime>(name: "end_date", type: "datetime", nullable: true),
                    createat = table.Column<DateTime>(name: "create_at", type: "datetime", nullable: true),
                    isactive = table.Column<bool>(name: "is_active", type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__vouchers__3213E83F4F3F0063", x => x.id);
                    table.UniqueConstraint("AK_vouchers_voucher_code", x => x.vouchercode);
                    table.ForeignKey(
                        name: "FK__vouchers__create__47DBAE45",
                        column: x => x.createby,
                        principalTable: "accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__vouchers__ref_co__48CFD27E",
                        column: x => x.refcourt,
                        principalTable: "courts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "slots",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refsubcourt = table.Column<string>(name: "ref_sub_court", type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    starttime = table.Column<TimeSpan>(name: "start_time", type: "time", nullable: true),
                    endtime = table.Column<TimeSpan>(name: "end_time", type: "time", nullable: true),
                    isactive = table.Column<bool>(name: "is_active", type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__slots__3213E83FFD788983", x => x.id);
                    table.ForeignKey(
                        name: "FK__slots__ref_sub_c__440B1D61",
                        column: x => x.refsubcourt,
                        principalTable: "sub_courts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "bookings",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refslot = table.Column<string>(name: "ref_slot", type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    bookby = table.Column<string>(name: "book_by", type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    vouchercode = table.Column<string>(name: "voucher_code", type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    bookat = table.Column<DateTime>(name: "book_at", type: "datetime", nullable: true),
                    price = table.Column<double>(type: "float", nullable: true),
                    originalprice = table.Column<double>(name: "original_price", type: "float", nullable: true),
                    ispaid = table.Column<bool>(name: "is_paid", type: "bit", nullable: true),
                    iscanceled = table.Column<bool>(name: "is_canceled", type: "bit", nullable: true),
                    isrefunded = table.Column<bool>(name: "is_refunded", type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__bookings__3213E83F85237411", x => x.id);
                    table.ForeignKey(
                        name: "FK__bookings__book_b__4CA06362",
                        column: x => x.bookby,
                        principalTable: "accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__bookings__ref_sl__4BAC3F29",
                        column: x => x.refslot,
                        principalTable: "slots",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__bookings__vouche__4D94879B",
                        column: x => x.vouchercode,
                        principalTable: "vouchers",
                        principalColumn: "voucher_code");
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    bookingref = table.Column<string>(name: "booking_ref", type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    orderat = table.Column<DateTime>(name: "order_at", type: "datetime", nullable: true),
                    price = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__orders__3213E83FA956F20D", x => x.id);
                    table.ForeignKey(
                        name: "FK__orders__booking___5070F446",
                        column: x => x.bookingref,
                        principalTable: "bookings",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_accounts_role_id",
                table: "accounts",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_book_by",
                table: "bookings",
                column: "book_by");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_ref_slot",
                table: "bookings",
                column: "ref_slot");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_voucher_code",
                table: "bookings",
                column: "voucher_code");

            migrationBuilder.CreateIndex(
                name: "IX_comments_comment_writer_id",
                table: "comments",
                column: "comment_writer_id");

            migrationBuilder.CreateIndex(
                name: "IX_comments_ref_court",
                table: "comments",
                column: "ref_court");

            migrationBuilder.CreateIndex(
                name: "IX_court_images_court_id",
                table: "court_images",
                column: "court_id");

            migrationBuilder.CreateIndex(
                name: "IX_courts_district_id",
                table: "courts",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_courts_owner_id",
                table: "courts",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "IX_districts_province_id",
                table: "districts",
                column: "province_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_booking_ref",
                table: "orders",
                column: "booking_ref");

            migrationBuilder.CreateIndex(
                name: "IX_reports_reporter_id",
                table: "reports",
                column: "reporter_id");

            migrationBuilder.CreateIndex(
                name: "IX_reports_type_id",
                table: "reports",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "IX_slots_ref_sub_court",
                table: "slots",
                column: "ref_sub_court");

            migrationBuilder.CreateIndex(
                name: "IX_sub_courts_court_type_id",
                table: "sub_courts",
                column: "court_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_sub_courts_parent_court_id",
                table: "sub_courts",
                column: "parent_court_id");

            migrationBuilder.CreateIndex(
                name: "IX_vouchers_create_by",
                table: "vouchers",
                column: "create_by");

            migrationBuilder.CreateIndex(
                name: "IX_vouchers_ref_court",
                table: "vouchers",
                column: "ref_court");

            migrationBuilder.CreateIndex(
                name: "UQ__vouchers__2173106953AA8CF3",
                table: "vouchers",
                column: "voucher_code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "court_images");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "reports");

            migrationBuilder.DropTable(
                name: "bookings");

            migrationBuilder.DropTable(
                name: "report_types");

            migrationBuilder.DropTable(
                name: "slots");

            migrationBuilder.DropTable(
                name: "vouchers");

            migrationBuilder.DropTable(
                name: "sub_courts");

            migrationBuilder.DropTable(
                name: "court_types");

            migrationBuilder.DropTable(
                name: "courts");

            migrationBuilder.DropTable(
                name: "districts");

            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "provinces");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
