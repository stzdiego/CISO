using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CISO.AuditService.Backend.Migrations
{
    /// <inheritdoc />
    public partial class CamposAdicionales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Requeriments",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "RegulationSections",
                newName: "ContentMD");

            migrationBuilder.AddColumn<string>(
                name: "ContentHTML",
                table: "Requeriments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContentMD",
                table: "Requeriments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Requeriments",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContentHTML",
                table: "RegulationSections",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentHTML",
                table: "Requeriments");

            migrationBuilder.DropColumn(
                name: "ContentMD",
                table: "Requeriments");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Requeriments");

            migrationBuilder.DropColumn(
                name: "ContentHTML",
                table: "RegulationSections");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Requeriments",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "ContentMD",
                table: "RegulationSections",
                newName: "Content");
        }
    }
}
