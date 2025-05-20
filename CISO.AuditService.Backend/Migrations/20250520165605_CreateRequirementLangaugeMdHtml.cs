using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CISO.AuditService.Backend.Migrations
{
    /// <inheritdoc />
    public partial class CreateRequirementLangaugeMdHtml : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Html",
                table: "RequirementLanguages",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Markdown",
                table: "RequirementLanguages",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Html",
                table: "RequirementLanguages");

            migrationBuilder.DropColumn(
                name: "Markdown",
                table: "RequirementLanguages");
        }
    }
}
