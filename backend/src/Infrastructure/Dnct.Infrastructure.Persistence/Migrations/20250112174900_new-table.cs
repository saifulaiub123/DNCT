using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dnct.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class newtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "run_time_parmtrs_mstr",
                schema: "codebotmstr",
                columns: table => new
                {
                    rtm_parmtrs_mstr_id = table.Column<int>(type: "integer", nullable: true),
                    parmtr_key = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    row_instr_ts = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    row_upd_ts = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "run_time_parmtrs_mstr",
                schema: "codebotmstr");
        }
    }
}
