using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookington.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddChatTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__accounts__role_i__267ABA7A",
                table: "accounts");

            migrationBuilder.DropForeignKey(
                name: "FK__bookings__book_b__4CA06362",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK__bookings__ref_sl__4BAC3F29",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK__bookings__vouche__4D94879B",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK__comments__commen__34C8D9D1",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK__comments__ref_co__35BCFE0A",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK__court_ima__court__31EC6D26",
                table: "court_images");

            migrationBuilder.DropForeignKey(
                name: "FK__courts__district__2F10007B",
                table: "courts");

            migrationBuilder.DropForeignKey(
                name: "FK__courts__owner_id__2E1BDC42",
                table: "courts");

            migrationBuilder.DropForeignKey(
                name: "FK__districts__provi__2B3F6F97",
                table: "districts");

            migrationBuilder.DropForeignKey(
                name: "FK__orders__booking___5070F446",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK__slots__ref_sub_c__440B1D61",
                table: "slots");

            migrationBuilder.DropForeignKey(
                name: "FK__sub_court__court__412EB0B6",
                table: "sub_courts");

            migrationBuilder.DropForeignKey(
                name: "FK__sub_court__paren__403A8C7D",
                table: "sub_courts");

            migrationBuilder.DropForeignKey(
                name: "FK__vouchers__create__47DBAE45",
                table: "vouchers");

            migrationBuilder.DropForeignKey(
                name: "FK__vouchers__ref_co__48CFD27E",
                table: "vouchers");

            migrationBuilder.DropTable(
                name: "reports");

            migrationBuilder.DropTable(
                name: "report_types");

            migrationBuilder.DropPrimaryKey(
                name: "PK__vouchers__3213E83F4F3F0063",
                table: "vouchers");

            migrationBuilder.DropPrimaryKey(
                name: "PK__sub_cour__3213E83F41A47DE2",
                table: "sub_courts");

            migrationBuilder.DropPrimaryKey(
                name: "PK__slots__3213E83FFD788983",
                table: "slots");

            migrationBuilder.DropPrimaryKey(
                name: "PK__roles__3213E83F59CB7009",
                table: "roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK__province__3213E83FC73BA5EC",
                table: "provinces");

            migrationBuilder.DropPrimaryKey(
                name: "PK__orders__3213E83FA956F20D",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_booking_ref",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK__district__3213E83F37D2778E",
                table: "districts");

            migrationBuilder.DropPrimaryKey(
                name: "PK__courts__3213E83F624A1B4C",
                table: "courts");

            migrationBuilder.DropPrimaryKey(
                name: "PK__court_ty__3213E83F78EAFB02",
                table: "court_types");

            migrationBuilder.DropPrimaryKey(
                name: "PK__court_im__3213E83F89DED088",
                table: "court_images");

            migrationBuilder.DropPrimaryKey(
                name: "PK__comments__3213E83F93EC1A00",
                table: "comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK__bookings__3213E83F85237411",
                table: "bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK__accounts__3213E83F6FD15E99",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "booking_ref",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "price",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "is_canceled",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "is_paid",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "is_refunded",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "is_confirmed",
                table: "accounts");

            migrationBuilder.RenameIndex(
                name: "UQ__vouchers__2173106953AA8CF3",
                table: "vouchers",
                newName: "UQ__vouchers__21731069488649AC");

            migrationBuilder.AlterColumn<int>(
                name: "usages",
                table: "vouchers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "vouchers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ref_court",
                table: "vouchers",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "max_quantity",
                table: "vouchers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "vouchers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "discount",
                table: "vouchers",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "create_by",
                table: "vouchers",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "create_at",
                table: "vouchers",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "parent_court_id",
                table: "sub_courts",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "is_deleted",
                table: "sub_courts",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "sub_courts",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "create_at",
                table: "sub_courts",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "court_type_id",
                table: "sub_courts",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "sub_courts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "start_time",
                table: "slots",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ref_sub_court",
                table: "slots",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "slots",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "end_time",
                table: "slots",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "price",
                table: "slots",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<string>(
                name: "role_name",
                table: "roles",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldUnicode: false,
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "roles",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "province_name",
                table: "provinces",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "provinces",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "order_at",
                table: "orders",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_canceled",
                table: "orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_paid",
                table: "orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_refunded",
                table: "orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "total_price",
                table: "orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "transaction_id",
                table: "orders",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "province_id",
                table: "districts",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "district_name",
                table: "districts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "districts",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "owner_id",
                table: "courts",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "open_at",
                table: "courts",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "courts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "is_deleted",
                table: "courts",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "courts",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "district_id",
                table: "courts",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "create_at",
                table: "courts",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "close_at",
                table: "courts",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "courts",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "court_types",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "court_types",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "court_id",
                table: "court_images",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "court_images",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ref_court",
                table: "comments",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "rating",
                table: "comments",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "comments",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "create_at",
                table: "comments",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "comments",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "comment_writer_id",
                table: "comments",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "voucher_code",
                table: "bookings",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldUnicode: false,
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ref_slot",
                table: "bookings",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "price",
                table: "bookings",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "original_price",
                table: "bookings",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "book_by",
                table: "bookings",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "book_at",
                table: "bookings",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "play_date",
                table: "bookings",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ref_order",
                table: "bookings",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "role_id",
                table: "accounts",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "phone",
                table: "accounts",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldUnicode: false,
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "accounts",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "create_at",
                table: "accounts",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "date_of_birth",
                table: "accounts",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "accounts",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK__vouchers__3213E83F3274A2E0",
                table: "vouchers",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__sub_cour__3213E83F4443E630",
                table: "sub_courts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__slots__3213E83FFC28A0F3",
                table: "slots",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__roles__3213E83FEB6E3144",
                table: "roles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__province__3213E83F9A67BBD3",
                table: "provinces",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__orders__3213E83F4FCD9A8A",
                table: "orders",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__district__3213E83F054088A0",
                table: "districts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__courts__3213E83F12391D8B",
                table: "courts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__court_ty__3213E83FB078620A",
                table: "court_types",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__court_im__3213E83F1E8B6DF6",
                table: "court_images",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__comments__3213E83F9B212B4E",
                table: "comments",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__bookings__3213E83FB874A8D5",
                table: "bookings",
                column: "id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_accounts_phone",
                table: "accounts",
                column: "phone");

            migrationBuilder.AddPrimaryKey(
                name: "PK__accounts__3213E83F22FEC304",
                table: "accounts",
                column: "id");

            migrationBuilder.CreateTable(
                name: "account_otps",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    phone = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    otpcode = table.Column<string>(name: "otp_code", type: "varchar(6)", unicode: false, maxLength: 6, nullable: false),
                    expireat = table.Column<DateTime>(name: "expire_at", type: "datetime", nullable: false),
                    createat = table.Column<DateTime>(name: "create_at", type: "datetime", nullable: false),
                    isconfirmed = table.Column<bool>(name: "is_confirmed", type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__account___3213E83FD40492D3", x => x.id);
                    table.ForeignKey(
                        name: "FK__account_o__phone__2A4B4B5E",
                        column: x => x.phone,
                        principalTable: "accounts",
                        principalColumn: "phone");
                });

            migrationBuilder.CreateTable(
                name: "chat_rooms",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refowner = table.Column<string>(name: "ref_owner", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refuser = table.Column<string>(name: "ref_user", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    isactive = table.Column<bool>(name: "is_active", type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__chat_roo__3213E83FB89BEDAD", x => x.id);
                    table.ForeignKey(
                        name: "FK__chat_room__ref_o__59063A47",
                        column: x => x.refowner,
                        principalTable: "accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__chat_room__ref_u__59FA5E80",
                        column: x => x.refuser,
                        principalTable: "accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "court_reports",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refcourt = table.Column<string>(name: "ref_court", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    reporterid = table.Column<string>(name: "reporter_id", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__court_re__3213E83F700D230C", x => x.id);
                    table.ForeignKey(
                        name: "FK__court_rep__ref_c__3C69FB99",
                        column: x => x.refcourt,
                        principalTable: "courts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__court_rep__repor__3D5E1FD2",
                        column: x => x.reporterid,
                        principalTable: "accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "transaction_history",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    reffrom = table.Column<string>(name: "ref_from", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refto = table.Column<string>(name: "ref_to", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    amount = table.Column<double>(type: "float", nullable: false),
                    createat = table.Column<DateTime>(name: "create_at", type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__transact__3213E83FA00BD87B", x => x.id);
                    table.ForeignKey(
                        name: "FK__transacti__ref_f__6477ECF3",
                        column: x => x.reffrom,
                        principalTable: "accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__transacti__ref_t__656C112C",
                        column: x => x.refto,
                        principalTable: "accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_balances",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refuser = table.Column<string>(name: "ref_user", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    balance = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__user_bal__3213E83F8707A95E", x => x.id);
                    table.ForeignKey(
                        name: "FK__user_bala__ref_u__619B8048",
                        column: x => x.refuser,
                        principalTable: "accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_reports",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refuser = table.Column<string>(name: "ref_user", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    reporterid = table.Column<string>(name: "reporter_id", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__user_rep__3213E83FFB8FE9AF", x => x.id);
                    table.ForeignKey(
                        name: "FK__user_repo__ref_u__403A8C7D",
                        column: x => x.refuser,
                        principalTable: "accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__user_repo__repor__412EB0B6",
                        column: x => x.reporterid,
                        principalTable: "accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "chat_messages",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refchatroom = table.Column<string>(name: "ref_chatroom", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refowner = table.Column<string>(name: "ref_owner", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refuser = table.Column<string>(name: "ref_user", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    createat = table.Column<DateTime>(name: "create_at", type: "datetime", nullable: false),
                    sequenceorder = table.Column<int>(name: "sequence_order", type: "int", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__chat_mes__3213E83F96A628B9", x => x.id);
                    table.ForeignKey(
                        name: "FK__chat_mess__ref_c__5CD6CB2B",
                        column: x => x.refchatroom,
                        principalTable: "chat_rooms",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__chat_mess__ref_o__5DCAEF64",
                        column: x => x.refowner,
                        principalTable: "accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__chat_mess__ref_u__5EBF139D",
                        column: x => x.refuser,
                        principalTable: "accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_bookings_ref_order",
                table: "bookings",
                column: "ref_order");

            migrationBuilder.CreateIndex(
                name: "UQ__accounts__B43B145F9C9D23A4",
                table: "accounts",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_account_otps_phone",
                table: "account_otps",
                column: "phone");

            migrationBuilder.CreateIndex(
                name: "IX_chat_messages_ref_chatroom",
                table: "chat_messages",
                column: "ref_chatroom");

            migrationBuilder.CreateIndex(
                name: "IX_chat_messages_ref_owner",
                table: "chat_messages",
                column: "ref_owner");

            migrationBuilder.CreateIndex(
                name: "IX_chat_messages_ref_user",
                table: "chat_messages",
                column: "ref_user");

            migrationBuilder.CreateIndex(
                name: "IX_chat_rooms_ref_owner",
                table: "chat_rooms",
                column: "ref_owner");

            migrationBuilder.CreateIndex(
                name: "IX_chat_rooms_ref_user",
                table: "chat_rooms",
                column: "ref_user");

            migrationBuilder.CreateIndex(
                name: "IX_court_reports_ref_court",
                table: "court_reports",
                column: "ref_court");

            migrationBuilder.CreateIndex(
                name: "IX_court_reports_reporter_id",
                table: "court_reports",
                column: "reporter_id");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_history_ref_from",
                table: "transaction_history",
                column: "ref_from");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_history_ref_to",
                table: "transaction_history",
                column: "ref_to");

            migrationBuilder.CreateIndex(
                name: "IX_user_balances_ref_user",
                table: "user_balances",
                column: "ref_user");

            migrationBuilder.CreateIndex(
                name: "IX_user_reports_ref_user",
                table: "user_reports",
                column: "ref_user");

            migrationBuilder.CreateIndex(
                name: "IX_user_reports_reporter_id",
                table: "user_reports",
                column: "reporter_id");

            migrationBuilder.AddForeignKey(
                name: "FK__accounts__role_i__276EDEB3",
                table: "accounts",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__bookings__book_b__5535A963",
                table: "bookings",
                column: "book_by",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__bookings__ref_or__5441852A",
                table: "bookings",
                column: "ref_order",
                principalTable: "orders",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__bookings__ref_sl__534D60F1",
                table: "bookings",
                column: "ref_slot",
                principalTable: "slots",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__bookings__vouche__5629CD9C",
                table: "bookings",
                column: "voucher_code",
                principalTable: "vouchers",
                principalColumn: "voucher_code");

            migrationBuilder.AddForeignKey(
                name: "FK__comments__commen__38996AB5",
                table: "comments",
                column: "comment_writer_id",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__comments__ref_co__398D8EEE",
                table: "comments",
                column: "ref_court",
                principalTable: "courts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__court_ima__court__35BCFE0A",
                table: "court_images",
                column: "court_id",
                principalTable: "courts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__courts__district__32E0915F",
                table: "courts",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__courts__owner_id__31EC6D26",
                table: "courts",
                column: "owner_id",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__districts__provi__2F10007B",
                table: "districts",
                column: "province_id",
                principalTable: "provinces",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__slots__ref_sub_c__49C3F6B7",
                table: "slots",
                column: "ref_sub_court",
                principalTable: "sub_courts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__sub_court__court__46E78A0C",
                table: "sub_courts",
                column: "court_type_id",
                principalTable: "court_types",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__sub_court__paren__45F365D3",
                table: "sub_courts",
                column: "parent_court_id",
                principalTable: "courts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__vouchers__create__4D94879B",
                table: "vouchers",
                column: "create_by",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__vouchers__ref_co__4E88ABD4",
                table: "vouchers",
                column: "ref_court",
                principalTable: "courts",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__accounts__role_i__276EDEB3",
                table: "accounts");

            migrationBuilder.DropForeignKey(
                name: "FK__bookings__book_b__5535A963",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK__bookings__ref_or__5441852A",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK__bookings__ref_sl__534D60F1",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK__bookings__vouche__5629CD9C",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK__comments__commen__38996AB5",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK__comments__ref_co__398D8EEE",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK__court_ima__court__35BCFE0A",
                table: "court_images");

            migrationBuilder.DropForeignKey(
                name: "FK__courts__district__32E0915F",
                table: "courts");

            migrationBuilder.DropForeignKey(
                name: "FK__courts__owner_id__31EC6D26",
                table: "courts");

            migrationBuilder.DropForeignKey(
                name: "FK__districts__provi__2F10007B",
                table: "districts");

            migrationBuilder.DropForeignKey(
                name: "FK__slots__ref_sub_c__49C3F6B7",
                table: "slots");

            migrationBuilder.DropForeignKey(
                name: "FK__sub_court__court__46E78A0C",
                table: "sub_courts");

            migrationBuilder.DropForeignKey(
                name: "FK__sub_court__paren__45F365D3",
                table: "sub_courts");

            migrationBuilder.DropForeignKey(
                name: "FK__vouchers__create__4D94879B",
                table: "vouchers");

            migrationBuilder.DropForeignKey(
                name: "FK__vouchers__ref_co__4E88ABD4",
                table: "vouchers");

            migrationBuilder.DropTable(
                name: "account_otps");

            migrationBuilder.DropTable(
                name: "chat_messages");

            migrationBuilder.DropTable(
                name: "court_reports");

            migrationBuilder.DropTable(
                name: "transaction_history");

            migrationBuilder.DropTable(
                name: "user_balances");

            migrationBuilder.DropTable(
                name: "user_reports");

            migrationBuilder.DropTable(
                name: "chat_rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK__vouchers__3213E83F3274A2E0",
                table: "vouchers");

            migrationBuilder.DropPrimaryKey(
                name: "PK__sub_cour__3213E83F4443E630",
                table: "sub_courts");

            migrationBuilder.DropPrimaryKey(
                name: "PK__slots__3213E83FFC28A0F3",
                table: "slots");

            migrationBuilder.DropPrimaryKey(
                name: "PK__roles__3213E83FEB6E3144",
                table: "roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK__province__3213E83F9A67BBD3",
                table: "provinces");

            migrationBuilder.DropPrimaryKey(
                name: "PK__orders__3213E83F4FCD9A8A",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK__district__3213E83F054088A0",
                table: "districts");

            migrationBuilder.DropPrimaryKey(
                name: "PK__courts__3213E83F12391D8B",
                table: "courts");

            migrationBuilder.DropPrimaryKey(
                name: "PK__court_ty__3213E83FB078620A",
                table: "court_types");

            migrationBuilder.DropPrimaryKey(
                name: "PK__court_im__3213E83F1E8B6DF6",
                table: "court_images");

            migrationBuilder.DropPrimaryKey(
                name: "PK__comments__3213E83F9B212B4E",
                table: "comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK__bookings__3213E83FB874A8D5",
                table: "bookings");

            migrationBuilder.DropIndex(
                name: "IX_bookings_ref_order",
                table: "bookings");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_accounts_phone",
                table: "accounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK__accounts__3213E83F22FEC304",
                table: "accounts");

            migrationBuilder.DropIndex(
                name: "UQ__accounts__B43B145F9C9D23A4",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "name",
                table: "sub_courts");

            migrationBuilder.DropColumn(
                name: "price",
                table: "slots");

            migrationBuilder.DropColumn(
                name: "is_canceled",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "is_paid",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "is_refunded",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "total_price",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "transaction_id",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "description",
                table: "courts");

            migrationBuilder.DropColumn(
                name: "play_date",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "ref_order",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "date_of_birth",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "password",
                table: "accounts");

            migrationBuilder.RenameIndex(
                name: "UQ__vouchers__21731069488649AC",
                table: "vouchers",
                newName: "UQ__vouchers__2173106953AA8CF3");

            migrationBuilder.AlterColumn<int>(
                name: "usages",
                table: "vouchers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "vouchers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ref_court",
                table: "vouchers",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<int>(
                name: "max_quantity",
                table: "vouchers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "vouchers",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<double>(
                name: "discount",
                table: "vouchers",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "create_by",
                table: "vouchers",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<DateTime>(
                name: "create_at",
                table: "vouchers",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<string>(
                name: "parent_court_id",
                table: "sub_courts",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<bool>(
                name: "is_deleted",
                table: "sub_courts",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "sub_courts",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "create_at",
                table: "sub_courts",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<int>(
                name: "court_type_id",
                table: "sub_courts",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "start_time",
                table: "slots",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<string>(
                name: "ref_sub_court",
                table: "slots",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "slots",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "end_time",
                table: "slots",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<string>(
                name: "role_name",
                table: "roles",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "roles",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "province_name",
                table: "provinces",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "provinces",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<DateTime>(
                name: "order_at",
                table: "orders",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddColumn<string>(
                name: "booking_ref",
                table: "orders",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "price",
                table: "orders",
                type: "float",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "province_id",
                table: "districts",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "district_name",
                table: "districts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "districts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "owner_id",
                table: "courts",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "open_at",
                table: "courts",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "courts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<bool>(
                name: "is_deleted",
                table: "courts",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "courts",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "district_id",
                table: "courts",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<DateTime>(
                name: "create_at",
                table: "courts",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "close_at",
                table: "courts",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "court_types",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "court_types",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "court_id",
                table: "court_images",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "court_images",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "ref_court",
                table: "comments",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<double>(
                name: "rating",
                table: "comments",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "comments",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "create_at",
                table: "comments",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "comments",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "comment_writer_id",
                table: "comments",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "voucher_code",
                table: "bookings",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldUnicode: false,
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "ref_slot",
                table: "bookings",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<double>(
                name: "price",
                table: "bookings",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "original_price",
                table: "bookings",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "book_by",
                table: "bookings",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<DateTime>(
                name: "book_at",
                table: "bookings",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddColumn<bool>(
                name: "is_canceled",
                table: "bookings",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_paid",
                table: "bookings",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_refunded",
                table: "bookings",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "role_id",
                table: "accounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "phone",
                table: "accounts",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldUnicode: false,
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "accounts",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "create_at",
                table: "accounts",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddColumn<bool>(
                name: "is_confirmed",
                table: "accounts",
                type: "bit",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__vouchers__3213E83F4F3F0063",
                table: "vouchers",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__sub_cour__3213E83F41A47DE2",
                table: "sub_courts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__slots__3213E83FFD788983",
                table: "slots",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__roles__3213E83F59CB7009",
                table: "roles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__province__3213E83FC73BA5EC",
                table: "provinces",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__orders__3213E83FA956F20D",
                table: "orders",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__district__3213E83F37D2778E",
                table: "districts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__courts__3213E83F624A1B4C",
                table: "courts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__court_ty__3213E83F78EAFB02",
                table: "court_types",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__court_im__3213E83F89DED088",
                table: "court_images",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__comments__3213E83F93EC1A00",
                table: "comments",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__bookings__3213E83F85237411",
                table: "bookings",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__accounts__3213E83F6FD15E99",
                table: "accounts",
                column: "id");

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
                name: "reports",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    reporterid = table.Column<string>(name: "reporter_id", type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    typeid = table.Column<int>(name: "type_id", type: "int", nullable: true),
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

            migrationBuilder.AddForeignKey(
                name: "FK__accounts__role_i__267ABA7A",
                table: "accounts",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__bookings__book_b__4CA06362",
                table: "bookings",
                column: "book_by",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__bookings__ref_sl__4BAC3F29",
                table: "bookings",
                column: "ref_slot",
                principalTable: "slots",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__bookings__vouche__4D94879B",
                table: "bookings",
                column: "voucher_code",
                principalTable: "vouchers",
                principalColumn: "voucher_code");

            migrationBuilder.AddForeignKey(
                name: "FK__comments__commen__34C8D9D1",
                table: "comments",
                column: "comment_writer_id",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__comments__ref_co__35BCFE0A",
                table: "comments",
                column: "ref_court",
                principalTable: "courts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__court_ima__court__31EC6D26",
                table: "court_images",
                column: "court_id",
                principalTable: "courts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__courts__district__2F10007B",
                table: "courts",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__courts__owner_id__2E1BDC42",
                table: "courts",
                column: "owner_id",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__districts__provi__2B3F6F97",
                table: "districts",
                column: "province_id",
                principalTable: "provinces",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__orders__booking___5070F446",
                table: "orders",
                column: "booking_ref",
                principalTable: "bookings",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__slots__ref_sub_c__440B1D61",
                table: "slots",
                column: "ref_sub_court",
                principalTable: "sub_courts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__sub_court__court__412EB0B6",
                table: "sub_courts",
                column: "court_type_id",
                principalTable: "court_types",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__sub_court__paren__403A8C7D",
                table: "sub_courts",
                column: "parent_court_id",
                principalTable: "courts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__vouchers__create__47DBAE45",
                table: "vouchers",
                column: "create_by",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__vouchers__ref_co__48CFD27E",
                table: "vouchers",
                column: "ref_court",
                principalTable: "courts",
                principalColumn: "id");
        }
    }
}
