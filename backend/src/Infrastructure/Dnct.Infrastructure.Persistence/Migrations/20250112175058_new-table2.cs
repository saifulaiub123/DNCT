using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dnct.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class newtable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "run_time_parmtrs",
                schema: "codebotmstr",
                columns: table => new
                {
                    table_config_id = table.Column<int>(type: "integer", nullable: true),
                    rtm_parmtrs_mstr_id = table.Column<int>(type: "integer", nullable: true),
                    parmtr_val = table.Column<string>(type: "text", nullable: true),
                    confgrtn_eff_start_ts = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    confgrtn_eff_end_ts = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "run_time_parmtrs",
                schema: "codebotmstr");
        }
    }
}
