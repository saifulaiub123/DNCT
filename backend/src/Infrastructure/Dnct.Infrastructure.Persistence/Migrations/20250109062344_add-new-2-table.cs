using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dnct.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addnew2table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "load_strategy",
                schema: "codebotmstr",
                columns: table => new
                {
                    load_stratgy_id = table.Column<int>(type: "integer", nullable: true),
                    load_stratgy_name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    load_stratgy_description = table.Column<string>(type: "character varying(900)", maxLength: 900, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "tbl_load_strategy",
                columns: table => new
                {
                    table_config_id = table.Column<int>(type: "integer", nullable: true),
                    load_stratgy_id = table.Column<int>(type: "integer", nullable: true),
                    confgrtn_eff_start_ts = table.Column<DateTime>(type: "timestamp", nullable: true),
                    confgrtn_eff_end_ts = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "load_strategy",
                schema: "codebotmstr");

            migrationBuilder.DropTable(
                name: "tbl_load_strategy");
        }
    }
}
