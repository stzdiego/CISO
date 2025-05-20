using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CISO.AuditService.Backend.Migrations
{
    /// <inheritdoc />
    public partial class ModificationRegulationSectionNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContentMD",
                table: "RegulationSections",
                newName: "ContentMd");

            migrationBuilder.RenameColumn(
                name: "ContentHTML",
                table: "RegulationSections",
                newName: "ContentHtml");

            migrationBuilder.AlterColumn<string>(
                name: "ContentMd",
                table: "RegulationSections",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ContentHtml",
                table: "RegulationSections",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContentMd",
                table: "RegulationSections",
                newName: "ContentMD");

            migrationBuilder.RenameColumn(
                name: "ContentHtml",
                table: "RegulationSections",
                newName: "ContentHTML");

            migrationBuilder.AlterColumn<string>(
                name: "ContentMD",
                table: "RegulationSections",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContentHTML",
                table: "RegulationSections",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
