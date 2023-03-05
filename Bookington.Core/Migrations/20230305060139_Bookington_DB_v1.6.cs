using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookington.Core.Migrations
{
    /// <inheritdoc />
    public partial class BookingtonDBv16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__account_o__phone__2A4B4B5E",
                table: "account_otps");

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
                name: "FK__chat_mess__ref_c__5CD6CB2B",
                table: "chat_messages");

            migrationBuilder.DropForeignKey(
                name: "FK__chat_mess__ref_o__5DCAEF64",
                table: "chat_messages");

            migrationBuilder.DropForeignKey(
                name: "FK__chat_mess__ref_u__5EBF139D",
                table: "chat_messages");

            migrationBuilder.DropForeignKey(
                name: "FK__chat_room__ref_o__59063A47",
                table: "chat_rooms");

            migrationBuilder.DropForeignKey(
                name: "FK__chat_room__ref_u__59FA5E80",
                table: "chat_rooms");

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
                name: "FK__court_rep__ref_c__3C69FB99",
                table: "court_reports");

            migrationBuilder.DropForeignKey(
                name: "FK__court_rep__repor__3D5E1FD2",
                table: "court_reports");

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
                name: "FK__transacti__ref_f__6477ECF3",
                table: "transaction_history");

            migrationBuilder.DropForeignKey(
                name: "FK__transacti__ref_t__656C112C",
                table: "transaction_history");

            migrationBuilder.DropForeignKey(
                name: "FK__user_bala__ref_u__619B8048",
                table: "user_balances");

            migrationBuilder.DropForeignKey(
                name: "FK__user_repo__ref_u__403A8C7D",
                table: "user_reports");

            migrationBuilder.DropForeignKey(
                name: "FK__user_repo__repor__412EB0B6",
                table: "user_reports");

            migrationBuilder.DropForeignKey(
                name: "FK__vouchers__create__4D94879B",
                table: "vouchers");

            migrationBuilder.DropForeignKey(
                name: "FK__vouchers__ref_co__4E88ABD4",
                table: "vouchers");

            migrationBuilder.DropPrimaryKey(
                name: "PK__vouchers__3213E83F3274A2E0",
                table: "vouchers");

            migrationBuilder.DropPrimaryKey(
                name: "PK__user_rep__3213E83FFB8FE9AF",
                table: "user_reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK__user_bal__3213E83F8707A95E",
                table: "user_balances");

            migrationBuilder.DropPrimaryKey(
                name: "PK__transact__3213E83FA00BD87B",
                table: "transaction_history");

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
                name: "PK__court_re__3213E83F700D230C",
                table: "court_reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK__court_im__3213E83F1E8B6DF6",
                table: "court_images");

            migrationBuilder.DropPrimaryKey(
                name: "PK__comments__3213E83F9B212B4E",
                table: "comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK__chat_roo__3213E83FB89BEDAD",
                table: "chat_rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK__chat_mes__3213E83F96A628B9",
                table: "chat_messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK__bookings__3213E83FB874A8D5",
                table: "bookings");

            migrationBuilder.DropIndex(
                name: "IX_bookings_voucher_code",
                table: "bookings");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_accounts_phone",
                table: "accounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK__accounts__3213E83F22FEC304",
                table: "accounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK__account___3213E83FD40492D3",
                table: "account_otps");

            migrationBuilder.DropIndex(
                name: "IX_account_otps_phone",
                table: "account_otps");

            migrationBuilder.DropColumn(
                name: "image_binary",
                table: "court_images");

            migrationBuilder.DropColumn(
                name: "original_price",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "voucher_code",
                table: "bookings");

            migrationBuilder.RenameIndex(
                name: "UQ__vouchers__21731069488649AC",
                table: "vouchers",
                newName: "UQ__vouchers__21731069457448AB");

            migrationBuilder.RenameIndex(
                name: "UQ__accounts__B43B145F9C9D23A4",
                table: "accounts",
                newName: "UQ__accounts__B43B145F4D261CD4");

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "vouchers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "user_reports",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<bool>(
                name: "is_responsed",
                table: "user_reports",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ref_response",
                table: "user_reports",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "reason",
                table: "transaction_history",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "slot_duration",
                table: "sub_courts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "days_in_schedule",
                table: "slots",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "slots",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "transaction_id",
                table: "orders",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "original_price",
                table: "orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "voucher_code",
                table: "orders",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "court_reports",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<bool>(
                name: "is_responded",
                table: "court_reports",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ref_response",
                table: "court_reports",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ref_image",
                table: "court_images",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "comments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ref_avatar",
                table: "accounts",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ref_account",
                table: "account_otps",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK__vouchers__3213E83F08758111",
                table: "vouchers",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__user_rep__3213E83F5A965BED",
                table: "user_reports",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__user_bal__3213E83FFB632C9E",
                table: "user_balances",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__transact__3213E83FF04F1EA8",
                table: "transaction_history",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__sub_cour__3213E83FBEB5844F",
                table: "sub_courts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__slots__3213E83FBB3BE737",
                table: "slots",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__roles__3213E83FCAA4D8CB",
                table: "roles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__province__3213E83F52E362D4",
                table: "provinces",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__orders__3213E83FCEC5BA3F",
                table: "orders",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__district__3213E83F11904467",
                table: "districts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__courts__3213E83F918EECEC",
                table: "courts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__court_ty__3213E83FC3A78D59",
                table: "court_types",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__court_re__3213E83F78E36AD5",
                table: "court_reports",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__court_im__3213E83FD178DA00",
                table: "court_images",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__comments__3213E83FAB9D61B6",
                table: "comments",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__chat_roo__3213E83FBACFA368",
                table: "chat_rooms",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__chat_mes__3213E83F03BAEB0C",
                table: "chat_messages",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__bookings__3213E83F199C458A",
                table: "bookings",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__accounts__3213E83FC82E5A90",
                table: "accounts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__account___3213E83F4C00CC5F",
                table: "account_otps",
                column: "id");

            migrationBuilder.CreateTable(
                name: "account_avatars",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refimage = table.Column<string>(name: "ref_image", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__account___3213E83F3425597B", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "bans",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refaccount = table.Column<string>(name: "ref_account", type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    refcourt = table.Column<string>(name: "ref_court", type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    duration = table.Column<int>(type: "int", nullable: false),
                    banuntil = table.Column<DateTime>(name: "ban_until", type: "datetime", nullable: true),
                    createat = table.Column<DateTime>(name: "create_at", type: "datetime", nullable: false),
                    isaccountban = table.Column<bool>(name: "is_account_ban", type: "bit", nullable: false),
                    iscourtban = table.Column<bool>(name: "is_court_ban", type: "bit", nullable: false),
                    isactive = table.Column<bool>(name: "is_active", type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__bans__3213E83FF8A75CE5", x => x.id);
                    table.ForeignKey(
                        name: "FK__bans__ref_accoun__7A672E12",
                        column: x => x.refaccount,
                        principalTable: "accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__bans__ref_court__7B5B524B",
                        column: x => x.refcourt,
                        principalTable: "courts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "court_report_responses",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__court_re__3213E83FEC7EEBB5", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "login_tokens",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refaccount = table.Column<string>(name: "ref_account", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    token = table.Column<string>(type: "varchar(2048)", unicode: false, maxLength: 2048, nullable: false),
                    refreshtoken = table.Column<string>(name: "refresh_token", type: "varchar(2048)", unicode: false, maxLength: 2048, nullable: false),
                    createat = table.Column<DateTime>(name: "create_at", type: "datetime", nullable: false),
                    expireat = table.Column<DateTime>(name: "expire_at", type: "datetime", nullable: false),
                    isrevoked = table.Column<bool>(name: "is_revoked", type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__login_to__3213E83F7DD2294A", x => x.id);
                    table.ForeignKey(
                        name: "FK__login_tok__ref_a__300424B4",
                        column: x => x.refaccount,
                        principalTable: "accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refaccount = table.Column<string>(name: "ref_account", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    content = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    createat = table.Column<DateTime>(name: "create_at", type: "datetime", nullable: false),
                    isread = table.Column<bool>(name: "is_read", type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__notifica__3213E83FDDD6D746", x => x.id);
                    table.ForeignKey(
                        name: "FK__notificat__ref_a__74AE54BC",
                        column: x => x.refaccount,
                        principalTable: "accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "promotions",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    refcourt = table.Column<string>(name: "ref_court", type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    refimage = table.Column<string>(name: "ref_image", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    promotionorder = table.Column<int>(name: "promotion_order", type: "int", nullable: false),
                    starttime = table.Column<DateTime>(name: "start_time", type: "datetime", nullable: false),
                    endtime = table.Column<DateTime>(name: "end_time", type: "datetime", nullable: false),
                    link = table.Column<string>(type: "varchar(2048)", unicode: false, maxLength: 2048, nullable: false),
                    iscourtpromotion = table.Column<bool>(name: "is_court_promotion", type: "bit", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__promotio__3213E83FA25F0716", x => x.id);
                    table.ForeignKey(
                        name: "FK__promotion__ref_c__778AC167",
                        column: x => x.refcourt,
                        principalTable: "courts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_report_responses",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__user_rep__3213E83F8503C715", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_reports_ref_response",
                table: "user_reports",
                column: "ref_response");

            migrationBuilder.CreateIndex(
                name: "IX_orders_transaction_id",
                table: "orders",
                column: "transaction_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_voucher_code",
                table: "orders",
                column: "voucher_code");

            migrationBuilder.CreateIndex(
                name: "IX_court_reports_ref_response",
                table: "court_reports",
                column: "ref_response");

            migrationBuilder.CreateIndex(
                name: "IX_accounts_ref_avatar",
                table: "accounts",
                column: "ref_avatar");

            migrationBuilder.CreateIndex(
                name: "IX_account_otps_ref_account",
                table: "account_otps",
                column: "ref_account");

            migrationBuilder.CreateIndex(
                name: "IX_bans_ref_account",
                table: "bans",
                column: "ref_account");

            migrationBuilder.CreateIndex(
                name: "IX_bans_ref_court",
                table: "bans",
                column: "ref_court");

            migrationBuilder.CreateIndex(
                name: "IX_login_tokens_ref_account",
                table: "login_tokens",
                column: "ref_account");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_ref_account",
                table: "notifications",
                column: "ref_account");

            migrationBuilder.CreateIndex(
                name: "IX_promotions_ref_court",
                table: "promotions",
                column: "ref_court");

            migrationBuilder.AddForeignKey(
                name: "FK__account_o__ref_a__2D27B809",
                table: "account_otps",
                column: "ref_account",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__accounts__ref_av__2A4B4B5E",
                table: "accounts",
                column: "ref_avatar",
                principalTable: "account_avatars",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__accounts__role_i__29572725",
                table: "accounts",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__bookings__book_b__66603565",
                table: "bookings",
                column: "book_by",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__bookings__ref_or__656C112C",
                table: "bookings",
                column: "ref_order",
                principalTable: "orders",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__bookings__ref_sl__6477ECF3",
                table: "bookings",
                column: "ref_slot",
                principalTable: "slots",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__chat_mess__ref_c__6D0D32F4",
                table: "chat_messages",
                column: "ref_chatroom",
                principalTable: "chat_rooms",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__chat_mess__ref_o__6E01572D",
                table: "chat_messages",
                column: "ref_owner",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__chat_mess__ref_u__6EF57B66",
                table: "chat_messages",
                column: "ref_user",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__chat_room__ref_o__693CA210",
                table: "chat_rooms",
                column: "ref_owner",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__chat_room__ref_u__6A30C649",
                table: "chat_rooms",
                column: "ref_user",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__comments__commen__3E52440B",
                table: "comments",
                column: "comment_writer_id",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__comments__ref_co__3F466844",
                table: "comments",
                column: "ref_court",
                principalTable: "courts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__court_ima__court__3B75D760",
                table: "court_images",
                column: "court_id",
                principalTable: "courts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__court_rep__ref_c__440B1D61",
                table: "court_reports",
                column: "ref_court",
                principalTable: "courts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__court_rep__ref_r__45F365D3",
                table: "court_reports",
                column: "ref_response",
                principalTable: "court_report_responses",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__court_rep__repor__44FF419A",
                table: "court_reports",
                column: "reporter_id",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__courts__district__38996AB5",
                table: "courts",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__courts__owner_id__37A5467C",
                table: "courts",
                column: "owner_id",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__districts__provi__34C8D9D1",
                table: "districts",
                column: "province_id",
                principalTable: "provinces",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__orders__transact__60A75C0F",
                table: "orders",
                column: "transaction_id",
                principalTable: "transaction_history",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__orders__voucher___619B8048",
                table: "orders",
                column: "voucher_code",
                principalTable: "vouchers",
                principalColumn: "voucher_code");

            migrationBuilder.AddForeignKey(
                name: "FK__slots__ref_sub_c__5535A963",
                table: "slots",
                column: "ref_sub_court",
                principalTable: "sub_courts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__sub_court__court__52593CB8",
                table: "sub_courts",
                column: "court_type_id",
                principalTable: "court_types",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__sub_court__paren__5165187F",
                table: "sub_courts",
                column: "parent_court_id",
                principalTable: "courts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__transacti__ref_f__5CD6CB2B",
                table: "transaction_history",
                column: "ref_from",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__transacti__ref_t__5DCAEF64",
                table: "transaction_history",
                column: "ref_to",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__user_bala__ref_u__71D1E811",
                table: "user_balances",
                column: "ref_user",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__user_repo__ref_r__4CA06362",
                table: "user_reports",
                column: "ref_response",
                principalTable: "user_report_responses",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__user_repo__ref_u__4AB81AF0",
                table: "user_reports",
                column: "ref_user",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__user_repo__repor__4BAC3F29",
                table: "user_reports",
                column: "reporter_id",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__vouchers__create__59063A47",
                table: "vouchers",
                column: "create_by",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__vouchers__ref_co__59FA5E80",
                table: "vouchers",
                column: "ref_court",
                principalTable: "courts",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__account_o__ref_a__2D27B809",
                table: "account_otps");

            migrationBuilder.DropForeignKey(
                name: "FK__accounts__ref_av__2A4B4B5E",
                table: "accounts");

            migrationBuilder.DropForeignKey(
                name: "FK__accounts__role_i__29572725",
                table: "accounts");

            migrationBuilder.DropForeignKey(
                name: "FK__bookings__book_b__66603565",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK__bookings__ref_or__656C112C",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK__bookings__ref_sl__6477ECF3",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK__chat_mess__ref_c__6D0D32F4",
                table: "chat_messages");

            migrationBuilder.DropForeignKey(
                name: "FK__chat_mess__ref_o__6E01572D",
                table: "chat_messages");

            migrationBuilder.DropForeignKey(
                name: "FK__chat_mess__ref_u__6EF57B66",
                table: "chat_messages");

            migrationBuilder.DropForeignKey(
                name: "FK__chat_room__ref_o__693CA210",
                table: "chat_rooms");

            migrationBuilder.DropForeignKey(
                name: "FK__chat_room__ref_u__6A30C649",
                table: "chat_rooms");

            migrationBuilder.DropForeignKey(
                name: "FK__comments__commen__3E52440B",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK__comments__ref_co__3F466844",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK__court_ima__court__3B75D760",
                table: "court_images");

            migrationBuilder.DropForeignKey(
                name: "FK__court_rep__ref_c__440B1D61",
                table: "court_reports");

            migrationBuilder.DropForeignKey(
                name: "FK__court_rep__ref_r__45F365D3",
                table: "court_reports");

            migrationBuilder.DropForeignKey(
                name: "FK__court_rep__repor__44FF419A",
                table: "court_reports");

            migrationBuilder.DropForeignKey(
                name: "FK__courts__district__38996AB5",
                table: "courts");

            migrationBuilder.DropForeignKey(
                name: "FK__courts__owner_id__37A5467C",
                table: "courts");

            migrationBuilder.DropForeignKey(
                name: "FK__districts__provi__34C8D9D1",
                table: "districts");

            migrationBuilder.DropForeignKey(
                name: "FK__orders__transact__60A75C0F",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK__orders__voucher___619B8048",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK__slots__ref_sub_c__5535A963",
                table: "slots");

            migrationBuilder.DropForeignKey(
                name: "FK__sub_court__court__52593CB8",
                table: "sub_courts");

            migrationBuilder.DropForeignKey(
                name: "FK__sub_court__paren__5165187F",
                table: "sub_courts");

            migrationBuilder.DropForeignKey(
                name: "FK__transacti__ref_f__5CD6CB2B",
                table: "transaction_history");

            migrationBuilder.DropForeignKey(
                name: "FK__transacti__ref_t__5DCAEF64",
                table: "transaction_history");

            migrationBuilder.DropForeignKey(
                name: "FK__user_bala__ref_u__71D1E811",
                table: "user_balances");

            migrationBuilder.DropForeignKey(
                name: "FK__user_repo__ref_r__4CA06362",
                table: "user_reports");

            migrationBuilder.DropForeignKey(
                name: "FK__user_repo__ref_u__4AB81AF0",
                table: "user_reports");

            migrationBuilder.DropForeignKey(
                name: "FK__user_repo__repor__4BAC3F29",
                table: "user_reports");

            migrationBuilder.DropForeignKey(
                name: "FK__vouchers__create__59063A47",
                table: "vouchers");

            migrationBuilder.DropForeignKey(
                name: "FK__vouchers__ref_co__59FA5E80",
                table: "vouchers");

            migrationBuilder.DropTable(
                name: "account_avatars");

            migrationBuilder.DropTable(
                name: "bans");

            migrationBuilder.DropTable(
                name: "court_report_responses");

            migrationBuilder.DropTable(
                name: "login_tokens");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "promotions");

            migrationBuilder.DropTable(
                name: "user_report_responses");

            migrationBuilder.DropPrimaryKey(
                name: "PK__vouchers__3213E83F08758111",
                table: "vouchers");

            migrationBuilder.DropPrimaryKey(
                name: "PK__user_rep__3213E83F5A965BED",
                table: "user_reports");

            migrationBuilder.DropIndex(
                name: "IX_user_reports_ref_response",
                table: "user_reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK__user_bal__3213E83FFB632C9E",
                table: "user_balances");

            migrationBuilder.DropPrimaryKey(
                name: "PK__transact__3213E83FF04F1EA8",
                table: "transaction_history");

            migrationBuilder.DropPrimaryKey(
                name: "PK__sub_cour__3213E83FBEB5844F",
                table: "sub_courts");

            migrationBuilder.DropPrimaryKey(
                name: "PK__slots__3213E83FBB3BE737",
                table: "slots");

            migrationBuilder.DropPrimaryKey(
                name: "PK__roles__3213E83FCAA4D8CB",
                table: "roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK__province__3213E83F52E362D4",
                table: "provinces");

            migrationBuilder.DropPrimaryKey(
                name: "PK__orders__3213E83FCEC5BA3F",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_transaction_id",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_voucher_code",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK__district__3213E83F11904467",
                table: "districts");

            migrationBuilder.DropPrimaryKey(
                name: "PK__courts__3213E83F918EECEC",
                table: "courts");

            migrationBuilder.DropPrimaryKey(
                name: "PK__court_ty__3213E83FC3A78D59",
                table: "court_types");

            migrationBuilder.DropPrimaryKey(
                name: "PK__court_re__3213E83F78E36AD5",
                table: "court_reports");

            migrationBuilder.DropIndex(
                name: "IX_court_reports_ref_response",
                table: "court_reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK__court_im__3213E83FD178DA00",
                table: "court_images");

            migrationBuilder.DropPrimaryKey(
                name: "PK__comments__3213E83FAB9D61B6",
                table: "comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK__chat_roo__3213E83FBACFA368",
                table: "chat_rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK__chat_mes__3213E83F03BAEB0C",
                table: "chat_messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK__bookings__3213E83F199C458A",
                table: "bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK__accounts__3213E83FC82E5A90",
                table: "accounts");

            migrationBuilder.DropIndex(
                name: "IX_accounts_ref_avatar",
                table: "accounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK__account___3213E83F4C00CC5F",
                table: "account_otps");

            migrationBuilder.DropIndex(
                name: "IX_account_otps_ref_account",
                table: "account_otps");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "vouchers");

            migrationBuilder.DropColumn(
                name: "is_responsed",
                table: "user_reports");

            migrationBuilder.DropColumn(
                name: "ref_response",
                table: "user_reports");

            migrationBuilder.DropColumn(
                name: "reason",
                table: "transaction_history");

            migrationBuilder.DropColumn(
                name: "slot_duration",
                table: "sub_courts");

            migrationBuilder.DropColumn(
                name: "days_in_schedule",
                table: "slots");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "slots");

            migrationBuilder.DropColumn(
                name: "original_price",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "voucher_code",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "is_responded",
                table: "court_reports");

            migrationBuilder.DropColumn(
                name: "ref_response",
                table: "court_reports");

            migrationBuilder.DropColumn(
                name: "ref_image",
                table: "court_images");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "ref_avatar",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "ref_account",
                table: "account_otps");

            migrationBuilder.RenameIndex(
                name: "UQ__vouchers__21731069457448AB",
                table: "vouchers",
                newName: "UQ__vouchers__21731069488649AC");

            migrationBuilder.RenameIndex(
                name: "UQ__accounts__B43B145F4D261CD4",
                table: "accounts",
                newName: "UQ__accounts__B43B145F9C9D23A4");

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "user_reports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "transaction_id",
                table: "orders",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "court_reports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddColumn<byte[]>(
                name: "image_binary",
                table: "court_images",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "original_price",
                table: "bookings",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "voucher_code",
                table: "bookings",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK__vouchers__3213E83F3274A2E0",
                table: "vouchers",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__user_rep__3213E83FFB8FE9AF",
                table: "user_reports",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__user_bal__3213E83F8707A95E",
                table: "user_balances",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__transact__3213E83FA00BD87B",
                table: "transaction_history",
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
                name: "PK__court_re__3213E83F700D230C",
                table: "court_reports",
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
                name: "PK__chat_roo__3213E83FB89BEDAD",
                table: "chat_rooms",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__chat_mes__3213E83F96A628B9",
                table: "chat_messages",
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

            migrationBuilder.AddPrimaryKey(
                name: "PK__account___3213E83FD40492D3",
                table: "account_otps",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_voucher_code",
                table: "bookings",
                column: "voucher_code");

            migrationBuilder.CreateIndex(
                name: "IX_account_otps_phone",
                table: "account_otps",
                column: "phone");

            migrationBuilder.AddForeignKey(
                name: "FK__account_o__phone__2A4B4B5E",
                table: "account_otps",
                column: "phone",
                principalTable: "accounts",
                principalColumn: "phone");

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
                name: "FK__chat_mess__ref_c__5CD6CB2B",
                table: "chat_messages",
                column: "ref_chatroom",
                principalTable: "chat_rooms",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__chat_mess__ref_o__5DCAEF64",
                table: "chat_messages",
                column: "ref_owner",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__chat_mess__ref_u__5EBF139D",
                table: "chat_messages",
                column: "ref_user",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__chat_room__ref_o__59063A47",
                table: "chat_rooms",
                column: "ref_owner",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__chat_room__ref_u__59FA5E80",
                table: "chat_rooms",
                column: "ref_user",
                principalTable: "accounts",
                principalColumn: "id");

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
                name: "FK__court_rep__ref_c__3C69FB99",
                table: "court_reports",
                column: "ref_court",
                principalTable: "courts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__court_rep__repor__3D5E1FD2",
                table: "court_reports",
                column: "reporter_id",
                principalTable: "accounts",
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
                name: "FK__transacti__ref_f__6477ECF3",
                table: "transaction_history",
                column: "ref_from",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__transacti__ref_t__656C112C",
                table: "transaction_history",
                column: "ref_to",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__user_bala__ref_u__619B8048",
                table: "user_balances",
                column: "ref_user",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__user_repo__ref_u__403A8C7D",
                table: "user_reports",
                column: "ref_user",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__user_repo__repor__412EB0B6",
                table: "user_reports",
                column: "reporter_id",
                principalTable: "accounts",
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
    }
}
