using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dnct.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addnewtabledatbssrcs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.AlterColumn<DateTime>(
                name: "GeneratedTime",
                schema: "usr",
                table: "UserTokens",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedUserRoleDate",
                schema: "usr",
                table: "UserRoles",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                schema: "usr",
                table: "UserRefreshTokens",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                schema: "usr",
                table: "UserRefreshTokens",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "usr",
                table: "UserRefreshTokens",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LoggedOn",
                schema: "usr",
                table: "UserLogins",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "usr",
                table: "Roles",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedClaim",
                schema: "usr",
                table: "RoleClaims",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Orders",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                table: "Orders",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateTable(
                name: "datbs_srcs",
                schema: "public",
                columns: table => new
                {
                    datbs_src_id = table.Column<int>(type: "integer", nullable: false),
                    confgrtn_eff_end_ts = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    usrname = table.Column<string>(type: "text", nullable: true),
                    repstry_name = table.Column<string>(type: "text", nullable: true),
                    conctn_name = table.Column<string>(type: "text", nullable: true),
                    tbl_dbs_name = table.Column<string>(type: "text", nullable: true),
                    tbl_name = table.Column<string>(type: "text", nullable: true),
                    tabl_kind = table.Column<string>(type: "text", nullable: true),
                    sql_to_use = table.Column<string>(type: "text", nullable: true),
                    queryband = table.Column<string>(type: "text", nullable: true),
                    adtnl_wher_condtns = table.Column<string>(type: "text", nullable: true),
                    trunct_tbl_befr_load = table.Column<char>(type: "character(1)", nullable: true),
                    setl_setup_name = table.Column<string>(type: "text", nullable: true),
                    objct_als = table.Column<string>(type: "text", nullable: true),
                    dedup_by_colmns = table.Column<string>(type: "text", nullable: true),
                    delt_row_idntfctn = table.Column<string>(type: "text", nullable: true),
                    estmtd_tbl_siz = table.Column<int>(type: "integer", nullable: true),
                    type2_colmns = table.Column<string>(type: "text", nullable: true),
                    objct_natr = table.Column<string>(type: "text", nullable: true),
                    odbc_typ = table.Column<string>(type: "text", nullable: true),
                    target_objc_con_name = table.Column<string>(type: "text", nullable: true),
                    partition_clause = table.Column<string>(type: "text", nullable: true),
                    partition_colmns = table.Column<string>(type: "text", nullable: true),
                    sorc_targt_file_id = table.Column<int>(type: "integer", nullable: true),
                    dedup_logic = table.Column<string>(type: "text", nullable: true),
                    pk_colmns = table.Column<string>(type: "text", nullable: true),
                    trunct_tbl_aftr_load = table.Column<char>(type: "character(1)", nullable: true),
                    years_of_history = table.Column<int>(type: "integer", nullable: true),
                    confgrtn_eff_start_ts = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_datbs_srcs", x => new { x.datbs_src_id, x.confgrtn_eff_end_ts });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "datbs_srcs",
                schema: "public");

            migrationBuilder.AlterColumn<DateTime>(
                name: "GeneratedTime",
                schema: "usr",
                table: "UserTokens",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedUserRoleDate",
                schema: "usr",
                table: "UserRoles",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                schema: "usr",
                table: "UserRefreshTokens",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                schema: "usr",
                table: "UserRefreshTokens",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "usr",
                table: "UserRefreshTokens",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LoggedOn",
                schema: "usr",
                table: "UserLogins",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "usr",
                table: "Roles",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedClaim",
                schema: "usr",
                table: "RoleClaims",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
