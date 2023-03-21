using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookington.Core.Migrations
{
    /// <inheritdoc />
    public partial class BookingtonDBv17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__bans__ref_accoun__7A672E12",
                table: "bans");

            migrationBuilder.DropForeignKey(
                name: "FK__bans__ref_court__7B5B524B",
                table: "bans");

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
                name: "FK__notificat__ref_a__74AE54BC",
                table: "notifications");

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
                name: "FK__user_bala__ref_u__71D1E811",
                table: "user_balances");

            migrationBuilder.DropForeignKey(
                name: "FK__vouchers__create__59063A47",
                table: "vouchers");

            migrationBuilder.DropForeignKey(
                name: "FK__vouchers__ref_co__59FA5E80",
                table: "vouchers");

            migrationBuilder.DropTable(
                name: "promotions");

            migrationBuilder.DropTable(
                name: "transaction_history");

            migrationBuilder.DropPrimaryKey(
                name: "PK__vouchers__3213E83F08758111",
                table: "vouchers");

            migrationBuilder.DropPrimaryKey(
                name: "PK__user_rep__3213E83F5A965BED",
                table: "user_reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK__user_rep__3213E83F8503C715",
                table: "user_report_responses");

            migrationBuilder.DropPrimaryKey(
                name: "PK__user_bal__3213E83FFB632C9E",
                table: "user_balances");

            migrationBuilder.DropPrimaryKey(
                name: "PK__sub_cour__3213E83FBEB5844F",
                table: "sub_courts");

            migrationBuilder.DropPrimaryKey(
                name: "PK__slots__3213E83FBB3BE737",
                table: "slots");

            migrationBuilder.DropIndex(
                name: "IX_slots_ref_sub_court",
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

            migrationBuilder.DropPrimaryKey(
                name: "PK__notifica__3213E83FDDD6D746",
                table: "notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK__login_to__3213E83F7DD2294A",
                table: "login_tokens");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK__court_re__3213E83FEC7EEBB5",
                table: "court_report_responses");

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
                name: "PK__bans__3213E83FF8A75CE5",
                table: "bans");

            migrationBuilder.DropPrimaryKey(
                name: "PK__accounts__3213E83FC82E5A90",
                table: "accounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK__account___3213E83F4C00CC5F",
                table: "account_otps");

            migrationBuilder.DropPrimaryKey(
                name: "PK__account___3213E83F3425597B",
                table: "account_avatars");

            migrationBuilder.DropColumn(
                name: "slot_duration",
                table: "sub_courts");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "slots");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "slots");

            migrationBuilder.DropColumn(
                name: "price",
                table: "slots");

            migrationBuilder.DropColumn(
                name: "ref_sub_court",
                table: "slots");

            migrationBuilder.RenameIndex(
                name: "UQ__vouchers__21731069457448AB",
                table: "vouchers",
                newName: "UQ__vouchers__21731069342B7D6D");

            migrationBuilder.RenameColumn(
                name: "is_responsed",
                table: "user_reports",
                newName: "is_responded");

            migrationBuilder.RenameIndex(
                name: "UQ__accounts__B43B145F4D261CD4",
                table: "accounts",
                newName: "UQ__accounts__B43B145F397BEDD5");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "courts",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<bool>(
                name: "is_verified",
                table: "accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK__vouchers__3213E83FA7D82A14",
                table: "vouchers",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__user_rep__3213E83F64648F88",
                table: "user_reports",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__user_rep__3213E83F135B73FA",
                table: "user_report_responses",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__user_bal__3213E83FA2CE1C1E",
                table: "user_balances",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__sub_cour__3213E83FBF0987C0",
                table: "sub_courts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__slots__3213E83FC38719FD",
                table: "slots",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__roles__3213E83FC6C6E202",
                table: "roles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__province__3213E83F8E5F2367",
                table: "provinces",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__orders__3213E83FCD46385C",
                table: "orders",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__notifica__3213E83F69AFDDA1",
                table: "notifications",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__login_to__3213E83F2D789ED4",
                table: "login_tokens",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__district__3213E83FB8413502",
                table: "districts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__courts__3213E83FB258E19D",
                table: "courts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__court_ty__3213E83FA0EAE041",
                table: "court_types",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__court_re__3213E83F847D9840",
                table: "court_reports",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__court_re__3213E83F633ECED0",
                table: "court_report_responses",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__court_im__3213E83F052C61C1",
                table: "court_images",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__comments__3213E83FD28979F0",
                table: "comments",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__chat_roo__3213E83FFE942561",
                table: "chat_rooms",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__chat_mes__3213E83FDD47C9F5",
                table: "chat_messages",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__bookings__3213E83F64570587",
                table: "bookings",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__bans__3213E83F43F72A6F",
                table: "bans",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__accounts__3213E83F9A2958FA",
                table: "accounts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__account___3213E83F3202D36A",
                table: "account_otps",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__account___3213E83F9D100C5D",
                table: "account_avatars",
                column: "id");

            migrationBuilder.CreateTable(
                name: "ads",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    refcourt = table.Column<string>(name: "ref_court", type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    refimage = table.Column<string>(name: "ref_image", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    promotionorder = table.Column<int>(name: "promotion_order", type: "int", nullable: false),
                    starttime = table.Column<DateTime>(name: "start_time", type: "datetime", nullable: false),
                    endtime = table.Column<DateTime>(name: "end_time", type: "datetime", nullable: false),
                    adlink = table.Column<string>(name: "ad_link", type: "varchar(2048)", unicode: false, maxLength: 2048, nullable: false),
                    iscourtad = table.Column<bool>(name: "is_court_ad", type: "bit", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ads__3213E83F17EF7121", x => x.id);
                    table.ForeignKey(
                        name: "FK__ads__ref_court__7D439ABD",
                        column: x => x.refcourt,
                        principalTable: "courts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "competitions",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    hostby = table.Column<string>(name: "host_by", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    description = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: false),
                    numofteamsallowed = table.Column<int>(name: "num_of_teams_allowed", type: "int", nullable: false),
                    competitioncode = table.Column<string>(name: "competition_code", type: "varchar(6)", unicode: false, maxLength: 6, nullable: false),
                    startdate = table.Column<DateTime>(name: "start_date", type: "datetime", nullable: false),
                    enddate = table.Column<DateTime>(name: "end_date", type: "datetime", nullable: false),
                    registerdeadline = table.Column<DateTime>(name: "register_deadline", type: "datetime", nullable: false),
                    isstarted = table.Column<bool>(name: "is_started", type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__competit__3213E83FD439E557", x => x.id);
                    table.ForeignKey(
                        name: "FK__competiti__host___07C12930",
                        column: x => x.hostby,
                        principalTable: "accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "matches",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    hostby = table.Column<string>(name: "host_by", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refbooking = table.Column<string>(name: "ref_booking", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    numofplayersallowed = table.Column<int>(name: "num_of_players_allowed", type: "int", nullable: false),
                    numofrounds = table.Column<int>(name: "num_of_rounds", type: "int", nullable: false),
                    matchcode = table.Column<string>(name: "match_code", type: "varchar(6)", unicode: false, maxLength: 6, nullable: true),
                    ispaymentsplitted = table.Column<bool>(name: "is_payment_splitted", type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__matches__3213E83F49FD2E48", x => x.id);
                    table.ForeignKey(
                        name: "FK__matches__host_by__03F0984C",
                        column: x => x.hostby,
                        principalTable: "accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__matches__ref_boo__04E4BC85",
                        column: x => x.refbooking,
                        principalTable: "bookings",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "momo_transactions",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    amount = table.Column<double>(type: "float", nullable: false),
                    createat = table.Column<DateTime>(name: "create_at", type: "datetime", nullable: false),
                    issuccessful = table.Column<bool>(name: "is_successful", type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__momo_tra__3213E83FBE8BF800", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sub_court_slots",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refsubcourt = table.Column<string>(name: "ref_sub_court", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refslot = table.Column<string>(name: "ref_slot", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    isactive = table.Column<bool>(name: "is_active", type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__sub_cour__3213E83FD099B9FF", x => x.id);
                    table.ForeignKey(
                        name: "FK__sub_court__ref_s__571DF1D5",
                        column: x => x.refsubcourt,
                        principalTable: "sub_courts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__sub_court__ref_s__5812160E",
                        column: x => x.refslot,
                        principalTable: "slots",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "competition_matches",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refcompetition = table.Column<string>(name: "ref_competition", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refmatch = table.Column<string>(name: "ref_match", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    matchposition = table.Column<int>(name: "match_position", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__competit__3213E83F5D1048D8", x => x.id);
                    table.ForeignKey(
                        name: "FK__competiti__ref_c__0A9D95DB",
                        column: x => x.refcompetition,
                        principalTable: "competitions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__competiti__ref_m__0B91BA14",
                        column: x => x.refmatch,
                        principalTable: "matches",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "teams",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refmatch = table.Column<string>(name: "ref_match", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refcompetition = table.Column<string>(name: "ref_competition", type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    teamcode = table.Column<string>(name: "team_code", type: "varchar(6)", unicode: false, maxLength: 6, nullable: true),
                    iscompetitionteam = table.Column<bool>(name: "is_competition_team", type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__teams__3213E83F88A07E73", x => x.id);
                    table.ForeignKey(
                        name: "FK__teams__ref_compe__0F624AF8",
                        column: x => x.refcompetition,
                        principalTable: "competitions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__teams__ref_match__0E6E26BF",
                        column: x => x.refmatch,
                        principalTable: "matches",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "transactions",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    reffrom = table.Column<string>(name: "ref_from", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refto = table.Column<string>(name: "ref_to", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refmomotransaction = table.Column<string>(name: "ref_momo_transaction", type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    amount = table.Column<double>(type: "float", nullable: false),
                    reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    createat = table.Column<DateTime>(name: "create_at", type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__transact__3213E83FA9389AD9", x => x.id);
                    table.ForeignKey(
                        name: "FK__transacti__ref_f__619B8048",
                        column: x => x.reffrom,
                        principalTable: "accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__transacti__ref_m__6383C8BA",
                        column: x => x.refmomotransaction,
                        principalTable: "momo_transactions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__transacti__ref_t__628FA481",
                        column: x => x.refto,
                        principalTable: "accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "match_teams",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refmatch = table.Column<string>(name: "ref_match", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refteam = table.Column<string>(name: "ref_team", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__match_te__3213E83FC99F7C9B", x => x.id);
                    table.ForeignKey(
                        name: "FK__match_tea__ref_m__123EB7A3",
                        column: x => x.refmatch,
                        principalTable: "matches",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__match_tea__ref_t__1332DBDC",
                        column: x => x.refteam,
                        principalTable: "teams",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "rounds",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refmatch = table.Column<string>(name: "ref_match", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refteam = table.Column<string>(name: "ref_team", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    roundnum = table.Column<int>(name: "round_num", type: "int", nullable: false),
                    point = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__rounds__3213E83FA84F30B7", x => x.id);
                    table.ForeignKey(
                        name: "FK__rounds__ref_matc__160F4887",
                        column: x => x.refmatch,
                        principalTable: "matches",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__rounds__ref_team__17036CC0",
                        column: x => x.refteam,
                        principalTable: "teams",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "team_players",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refteam = table.Column<string>(name: "ref_team", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refaccount = table.Column<string>(name: "ref_account", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__team_pla__3213E83F558D630D", x => x.id);
                    table.ForeignKey(
                        name: "FK__team_play__ref_a__1AD3FDA4",
                        column: x => x.refaccount,
                        principalTable: "accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__team_play__ref_t__19DFD96B",
                        column: x => x.refteam,
                        principalTable: "teams",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ads_ref_court",
                table: "ads",
                column: "ref_court");

            migrationBuilder.CreateIndex(
                name: "IX_competition_matches_ref_competition",
                table: "competition_matches",
                column: "ref_competition");

            migrationBuilder.CreateIndex(
                name: "IX_competition_matches_ref_match",
                table: "competition_matches",
                column: "ref_match");

            migrationBuilder.CreateIndex(
                name: "IX_competitions_host_by",
                table: "competitions",
                column: "host_by");

            migrationBuilder.CreateIndex(
                name: "IX_match_teams_ref_match",
                table: "match_teams",
                column: "ref_match");

            migrationBuilder.CreateIndex(
                name: "IX_match_teams_ref_team",
                table: "match_teams",
                column: "ref_team");

            migrationBuilder.CreateIndex(
                name: "IX_matches_host_by",
                table: "matches",
                column: "host_by");

            migrationBuilder.CreateIndex(
                name: "IX_matches_ref_booking",
                table: "matches",
                column: "ref_booking");

            migrationBuilder.CreateIndex(
                name: "IX_rounds_ref_match",
                table: "rounds",
                column: "ref_match");

            migrationBuilder.CreateIndex(
                name: "IX_rounds_ref_team",
                table: "rounds",
                column: "ref_team");

            migrationBuilder.CreateIndex(
                name: "IX_sub_court_slots_ref_slot",
                table: "sub_court_slots",
                column: "ref_slot");

            migrationBuilder.CreateIndex(
                name: "IX_sub_court_slots_ref_sub_court",
                table: "sub_court_slots",
                column: "ref_sub_court");

            migrationBuilder.CreateIndex(
                name: "IX_team_players_ref_account",
                table: "team_players",
                column: "ref_account");

            migrationBuilder.CreateIndex(
                name: "IX_team_players_ref_team",
                table: "team_players",
                column: "ref_team");

            migrationBuilder.CreateIndex(
                name: "IX_teams_ref_competition",
                table: "teams",
                column: "ref_competition");

            migrationBuilder.CreateIndex(
                name: "IX_teams_ref_match",
                table: "teams",
                column: "ref_match");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_ref_from",
                table: "transactions",
                column: "ref_from");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_ref_momo_transaction",
                table: "transactions",
                column: "ref_momo_transaction");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_ref_to",
                table: "transactions",
                column: "ref_to");

            migrationBuilder.AddForeignKey(
                name: "FK__bans__ref_accoun__00200768",
                table: "bans",
                column: "ref_account",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__bans__ref_court__01142BA1",
                table: "bans",
                column: "ref_court",
                principalTable: "courts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__bookings__book_b__6C190EBB",
                table: "bookings",
                column: "book_by",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__bookings__ref_or__6B24EA82",
                table: "bookings",
                column: "ref_order",
                principalTable: "orders",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__bookings__ref_sl__6A30C649",
                table: "bookings",
                column: "ref_slot",
                principalTable: "slots",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__chat_mess__ref_c__72C60C4A",
                table: "chat_messages",
                column: "ref_chatroom",
                principalTable: "chat_rooms",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__chat_mess__ref_o__73BA3083",
                table: "chat_messages",
                column: "ref_owner",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__chat_mess__ref_u__74AE54BC",
                table: "chat_messages",
                column: "ref_user",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__chat_room__ref_o__6EF57B66",
                table: "chat_rooms",
                column: "ref_owner",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__chat_room__ref_u__6FE99F9F",
                table: "chat_rooms",
                column: "ref_user",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__notificat__ref_a__7A672E12",
                table: "notifications",
                column: "ref_account",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__orders__transact__66603565",
                table: "orders",
                column: "transaction_id",
                principalTable: "transactions",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__orders__voucher___6754599E",
                table: "orders",
                column: "voucher_code",
                principalTable: "vouchers",
                principalColumn: "voucher_code");

            migrationBuilder.AddForeignKey(
                name: "FK__user_bala__ref_u__778AC167",
                table: "user_balances",
                column: "ref_user",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__vouchers__create__5BE2A6F2",
                table: "vouchers",
                column: "create_by",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__vouchers__ref_co__5CD6CB2B",
                table: "vouchers",
                column: "ref_court",
                principalTable: "courts",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__bans__ref_accoun__00200768",
                table: "bans");

            migrationBuilder.DropForeignKey(
                name: "FK__bans__ref_court__01142BA1",
                table: "bans");

            migrationBuilder.DropForeignKey(
                name: "FK__bookings__book_b__6C190EBB",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK__bookings__ref_or__6B24EA82",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK__bookings__ref_sl__6A30C649",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK__chat_mess__ref_c__72C60C4A",
                table: "chat_messages");

            migrationBuilder.DropForeignKey(
                name: "FK__chat_mess__ref_o__73BA3083",
                table: "chat_messages");

            migrationBuilder.DropForeignKey(
                name: "FK__chat_mess__ref_u__74AE54BC",
                table: "chat_messages");

            migrationBuilder.DropForeignKey(
                name: "FK__chat_room__ref_o__6EF57B66",
                table: "chat_rooms");

            migrationBuilder.DropForeignKey(
                name: "FK__chat_room__ref_u__6FE99F9F",
                table: "chat_rooms");

            migrationBuilder.DropForeignKey(
                name: "FK__notificat__ref_a__7A672E12",
                table: "notifications");

            migrationBuilder.DropForeignKey(
                name: "FK__orders__transact__66603565",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK__orders__voucher___6754599E",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK__user_bala__ref_u__778AC167",
                table: "user_balances");

            migrationBuilder.DropForeignKey(
                name: "FK__vouchers__create__5BE2A6F2",
                table: "vouchers");

            migrationBuilder.DropForeignKey(
                name: "FK__vouchers__ref_co__5CD6CB2B",
                table: "vouchers");

            migrationBuilder.DropTable(
                name: "ads");

            migrationBuilder.DropTable(
                name: "competition_matches");

            migrationBuilder.DropTable(
                name: "match_teams");

            migrationBuilder.DropTable(
                name: "rounds");

            migrationBuilder.DropTable(
                name: "sub_court_slots");

            migrationBuilder.DropTable(
                name: "team_players");

            migrationBuilder.DropTable(
                name: "transactions");

            migrationBuilder.DropTable(
                name: "teams");

            migrationBuilder.DropTable(
                name: "momo_transactions");

            migrationBuilder.DropTable(
                name: "competitions");

            migrationBuilder.DropTable(
                name: "matches");

            migrationBuilder.DropPrimaryKey(
                name: "PK__vouchers__3213E83FA7D82A14",
                table: "vouchers");

            migrationBuilder.DropPrimaryKey(
                name: "PK__user_rep__3213E83F64648F88",
                table: "user_reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK__user_rep__3213E83F135B73FA",
                table: "user_report_responses");

            migrationBuilder.DropPrimaryKey(
                name: "PK__user_bal__3213E83FA2CE1C1E",
                table: "user_balances");

            migrationBuilder.DropPrimaryKey(
                name: "PK__sub_cour__3213E83FBF0987C0",
                table: "sub_courts");

            migrationBuilder.DropPrimaryKey(
                name: "PK__slots__3213E83FC38719FD",
                table: "slots");

            migrationBuilder.DropPrimaryKey(
                name: "PK__roles__3213E83FC6C6E202",
                table: "roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK__province__3213E83F8E5F2367",
                table: "provinces");

            migrationBuilder.DropPrimaryKey(
                name: "PK__orders__3213E83FCD46385C",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK__notifica__3213E83F69AFDDA1",
                table: "notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK__login_to__3213E83F2D789ED4",
                table: "login_tokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK__district__3213E83FB8413502",
                table: "districts");

            migrationBuilder.DropPrimaryKey(
                name: "PK__courts__3213E83FB258E19D",
                table: "courts");

            migrationBuilder.DropPrimaryKey(
                name: "PK__court_ty__3213E83FA0EAE041",
                table: "court_types");

            migrationBuilder.DropPrimaryKey(
                name: "PK__court_re__3213E83F847D9840",
                table: "court_reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK__court_re__3213E83F633ECED0",
                table: "court_report_responses");

            migrationBuilder.DropPrimaryKey(
                name: "PK__court_im__3213E83F052C61C1",
                table: "court_images");

            migrationBuilder.DropPrimaryKey(
                name: "PK__comments__3213E83FD28979F0",
                table: "comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK__chat_roo__3213E83FFE942561",
                table: "chat_rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK__chat_mes__3213E83FDD47C9F5",
                table: "chat_messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK__bookings__3213E83F64570587",
                table: "bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK__bans__3213E83F43F72A6F",
                table: "bans");

            migrationBuilder.DropPrimaryKey(
                name: "PK__accounts__3213E83F9A2958FA",
                table: "accounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK__account___3213E83F3202D36A",
                table: "account_otps");

            migrationBuilder.DropPrimaryKey(
                name: "PK__account___3213E83F9D100C5D",
                table: "account_avatars");

            migrationBuilder.DropColumn(
                name: "is_verified",
                table: "accounts");

            migrationBuilder.RenameIndex(
                name: "UQ__vouchers__21731069342B7D6D",
                table: "vouchers",
                newName: "UQ__vouchers__21731069457448AB");

            migrationBuilder.RenameColumn(
                name: "is_responded",
                table: "user_reports",
                newName: "is_responsed");

            migrationBuilder.RenameIndex(
                name: "UQ__accounts__B43B145F397BEDD5",
                table: "accounts",
                newName: "UQ__accounts__B43B145F4D261CD4");

            migrationBuilder.AddColumn<int>(
                name: "slot_duration",
                table: "sub_courts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "slots",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "slots",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "price",
                table: "slots",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ref_sub_court",
                table: "slots",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "courts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AddPrimaryKey(
                name: "PK__vouchers__3213E83F08758111",
                table: "vouchers",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__user_rep__3213E83F5A965BED",
                table: "user_reports",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__user_rep__3213E83F8503C715",
                table: "user_report_responses",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__user_bal__3213E83FFB632C9E",
                table: "user_balances",
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
                name: "PK__notifica__3213E83FDDD6D746",
                table: "notifications",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__login_to__3213E83F7DD2294A",
                table: "login_tokens",
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
                name: "PK__court_re__3213E83FEC7EEBB5",
                table: "court_report_responses",
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
                name: "PK__bans__3213E83FF8A75CE5",
                table: "bans",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__accounts__3213E83FC82E5A90",
                table: "accounts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__account___3213E83F4C00CC5F",
                table: "account_otps",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__account___3213E83F3425597B",
                table: "account_avatars",
                column: "id");

            migrationBuilder.CreateTable(
                name: "promotions",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refcourt = table.Column<string>(name: "ref_court", type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    endtime = table.Column<DateTime>(name: "end_time", type: "datetime", nullable: false),
                    iscourtpromotion = table.Column<bool>(name: "is_court_promotion", type: "bit", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "bit", nullable: false),
                    link = table.Column<string>(type: "varchar(2048)", unicode: false, maxLength: 2048, nullable: false),
                    promotionorder = table.Column<int>(name: "promotion_order", type: "int", nullable: false),
                    refimage = table.Column<string>(name: "ref_image", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    starttime = table.Column<DateTime>(name: "start_time", type: "datetime", nullable: false),
                    title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
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
                name: "transaction_history",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    reffrom = table.Column<string>(name: "ref_from", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    refto = table.Column<string>(name: "ref_to", type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    amount = table.Column<double>(type: "float", nullable: false),
                    createat = table.Column<DateTime>(name: "create_at", type: "datetime", nullable: false),
                    reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__transact__3213E83FF04F1EA8", x => x.id);
                    table.ForeignKey(
                        name: "FK__transacti__ref_f__5CD6CB2B",
                        column: x => x.reffrom,
                        principalTable: "accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__transacti__ref_t__5DCAEF64",
                        column: x => x.refto,
                        principalTable: "accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_slots_ref_sub_court",
                table: "slots",
                column: "ref_sub_court");

            migrationBuilder.CreateIndex(
                name: "IX_promotions_ref_court",
                table: "promotions",
                column: "ref_court");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_history_ref_from",
                table: "transaction_history",
                column: "ref_from");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_history_ref_to",
                table: "transaction_history",
                column: "ref_to");

            migrationBuilder.AddForeignKey(
                name: "FK__bans__ref_accoun__7A672E12",
                table: "bans",
                column: "ref_account",
                principalTable: "accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__bans__ref_court__7B5B524B",
                table: "bans",
                column: "ref_court",
                principalTable: "courts",
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
                name: "FK__notificat__ref_a__74AE54BC",
                table: "notifications",
                column: "ref_account",
                principalTable: "accounts",
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
                name: "FK__user_bala__ref_u__71D1E811",
                table: "user_balances",
                column: "ref_user",
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
    }
}
