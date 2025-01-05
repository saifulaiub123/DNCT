using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dnct.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addTableColConfigurationtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_col_confgrtn",
                schema: "codebotmstr",
                columns: table => new
                {
                    tbl_col_confgrtn_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tbl_confgrtn_id = table.Column<int>(type: "integer", nullable: false),
                    colmn_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    data_type = table.Column<string>(type: "text", nullable: true),
                    colmn_trnsfrmtn_step1 = table.Column<string>(type: "text", nullable: true),
                    genrt_id_ind = table.Column<char>(type: "bpchar(1)", nullable: true),
                    id_genrtn_stratgy_id = table.Column<short>(type: "smallint", nullable: true),
                    type2_start_ind = table.Column<short>(type: "smallint", nullable: true),
                    type2_end_ind = table.Column<short>(type: "smallint", nullable: true),
                    curr_row_ind = table.Column<short>(type: "smallint", nullable: true),
                    pattern1 = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    pattern2 = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    pattern3 = table.Column<char>(type: "bpchar(1)", nullable: true),
                    lad_ind = table.Column<short>(type: "smallint", nullable: true),
                    join_dups_ind = table.Column<short>(type: "smallint", nullable: true),
                    confgrtn_eff_start_ts = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    confgrtn_eff_end_ts = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_col_confgrtn", x => x.tbl_col_confgrtn_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_col_confgrtn",
                schema: "codebotmstr");
        }
    }
}
