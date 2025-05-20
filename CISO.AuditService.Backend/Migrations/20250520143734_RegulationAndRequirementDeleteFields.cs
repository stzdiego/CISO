using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CISO.AuditService.Backend.Migrations
{
    /// <inheritdoc />
    public partial class RegulationAndRequirementDeleteFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentHtml",
                table: "Requirements");

            migrationBuilder.DropColumn(
                name: "ContentMd",
                table: "Requirements");

            migrationBuilder.DropColumn(
                name: "ContentHtml",
                table: "RegulationSections");

            migrationBuilder.DropColumn(
                name: "ContentMd",
                table: "RegulationSections");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentHtml",
                table: "Requirements",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentMd",
                table: "Requirements",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentHtml",
                table: "RegulationSections",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentMd",
                table: "RegulationSections",
                type: "text",
                nullable: true);
        }
    }
}
