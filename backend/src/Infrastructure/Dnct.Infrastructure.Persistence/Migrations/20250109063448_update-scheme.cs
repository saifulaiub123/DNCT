using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dnct.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatescheme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "tbl_load_strategy",
                newName: "tbl_load_strategy",
                newSchema: "codebotmstr");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "tbl_load_strategy",
                schema: "codebotmstr",
                newName: "tbl_load_strategy");
        }
    }
}
