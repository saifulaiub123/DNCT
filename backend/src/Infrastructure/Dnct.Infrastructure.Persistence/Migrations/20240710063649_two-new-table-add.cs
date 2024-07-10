using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dnct.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class twonewtableadd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contns_mstrs",
                schema: "public",
                columns: table => new
                {
                    contn_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_user_name = table.Column<string>(type: "text", nullable: true),
                    contn_name = table.Column<string>(type: "text", nullable: true),
                    host_ip = table.Column<string>(type: "text", nullable: true),
                    user_name = table.Column<string>(type: "text", nullable: true),
                    paswd = table.Column<string>(type: "text", nullable: true),
                    platform_type = table.Column<string>(type: "text", nullable: true),
                    logmech = table.Column<string>(type: "text", nullable: true),
                    td_parameter = table.Column<string>(type: "text", nullable: true),
                    default_db = table.Column<string>(type: "text", nullable: true),
                    accnt_str = table.Column<string>(type: "text", nullable: true),
                    orcl_tns_alias = table.Column<string>(type: "text", nullable: true),
                    private_key = table.Column<string>(type: "text", nullable: true),
                    public_key = table.Column<string>(type: "text", nullable: true),
                    encr_key = table.Column<string>(type: "text", nullable: true),
                    enabled_ind = table.Column<int>(type: "integer", nullable: true),
                    contn_port = table.Column<string>(type: "text", nullable: true),
                    attr1 = table.Column<string>(type: "text", nullable: true),
                    attr2 = table.Column<string>(type: "text", nullable: true),
                    attr3 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contns_mstrs", x => x.contn_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_confgrtns",
                schema: "public",
                columns: table => new
                {
                    tbl_confgrtn_id = table.Column<int>(type: "integer", nullable: false),
                    confgrtn_eff_start_ts = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    instnc_name = table.Column<string>(type: "text", nullable: true),
                    instnc_use_type = table.Column<string>(type: "text", nullable: true),
                    datbs_src_id = table.Column<int>(type: "integer", nullable: true),
                    queryband = table.Column<string>(type: "text", nullable: true),
                    adtnl_wher_condtns = table.Column<string>(type: "text", nullable: true),
                    sql_to_use_for_sel = table.Column<string>(type: "text", nullable: true),
                    dml_action_type = table.Column<string>(type: "text", nullable: true),
                    trunct_tbl_befr_load = table.Column<char>(type: "character(1)", nullable: true),
                    setl_setup_name = table.Column<string>(type: "text", nullable: true),
                    dedup_by_colmns = table.Column<string>(type: "text", nullable: true),
                    objct_als = table.Column<string>(type: "text", nullable: true),
                    type2_colmns = table.Column<string>(type: "text", nullable: true),
                    delt_row_idntfctn = table.Column<string>(type: "text", nullable: true),
                    estmtd_tbl_siz = table.Column<int>(type: "integer", nullable: true),
                    objct_natr = table.Column<string>(type: "text", nullable: true),
                    partition_clause = table.Column<string>(type: "text", nullable: true),
                    partition_colmns = table.Column<string>(type: "text", nullable: true),
                    target_objc_con_name = table.Column<string>(type: "text", nullable: true),
                    dedup_logic = table.Column<string>(type: "text", nullable: true),
                    pk_colmns = table.Column<string>(type: "text", nullable: true),
                    trunct_tbl_aftr_load = table.Column<char>(type: "character(1)", nullable: true),
                    years_of_history = table.Column<int>(type: "integer", nullable: true),
                    src_patrn_id = table.Column<int>(type: "integer", nullable: true),
                    confgrtn_eff_end_ts = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_confgrtns", x => new { x.tbl_confgrtn_id, x.confgrtn_eff_start_ts });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contns_mstrs",
                schema: "public");

            migrationBuilder.DropTable(
                name: "tbl_confgrtns",
                schema: "public");
        }
    }
}
