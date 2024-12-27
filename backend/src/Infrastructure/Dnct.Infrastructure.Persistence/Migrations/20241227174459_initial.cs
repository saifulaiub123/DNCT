using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dnct.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "codebotmstr");

            migrationBuilder.EnsureSchema(
                name: "usr");

            migrationBuilder.CreateTable(
                name: "contns_mstr",
                schema: "codebotmstr",
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
                    table.PrimaryKey("PK_contns_mstr", x => x.contn_id);
                });

            migrationBuilder.CreateTable(
                name: "datbs_srcs",
                schema: "codebotmstr",
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

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "usr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DisplayName = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_confgrtn",
                schema: "codebotmstr",
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
                    table.PrimaryKey("PK_tbl_confgrtn", x => new { x.tbl_confgrtn_id, x.confgrtn_eff_start_ts });
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "usr",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    GeneratedCode = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "usr_queries",
                schema: "codebotmstr",
                columns: table => new
                {
                    usr_qry_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    table_config_id = table.Column<int>(type: "integer", nullable: true),
                    usr_qry = table.Column<string>(type: "text", nullable: true),
                    base_query_ind = table.Column<int>(type: "integer", nullable: true),
                    qry_order_ind = table.Column<int>(type: "integer", nullable: true),
                    row_instr_ts = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usr_queries", x => x.usr_qry_id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "usr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedClaim = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "usr",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderName = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    ModifiedBy = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "usr",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "usr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "usr",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "usr",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    LoggedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "usr",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRefreshTokens",
                schema: "usr",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsValid = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    ModifiedBy = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "usr",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "usr",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    CreatedUserRoleDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "usr",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "usr",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "usr",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    GeneratedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "usr",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "usr",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "usr",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "usr",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "usr",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRefreshTokens_UserId",
                schema: "usr",
                table: "UserRefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "usr",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "usr",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "usr",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contns_mstr",
                schema: "codebotmstr");

            migrationBuilder.DropTable(
                name: "datbs_srcs",
                schema: "codebotmstr");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "usr");

            migrationBuilder.DropTable(
                name: "tbl_confgrtn",
                schema: "codebotmstr");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "usr");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "usr");

            migrationBuilder.DropTable(
                name: "UserRefreshTokens",
                schema: "usr");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "usr");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "usr");

            migrationBuilder.DropTable(
                name: "usr_queries",
                schema: "codebotmstr");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "usr");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "usr");
        }
    }
}
