using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CISO.AuditService.Backend.Migrations
{
    /// <inheritdoc />
    public partial class RequerimentFieldsNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContentMD",
                table: "Requirements",
                newName: "ContentMd");

            migrationBuilder.RenameColumn(
                name: "ContentHTML",
                table: "Requirements",
                newName: "ContentHtml");

            migrationBuilder.AlterColumn<string>(
                name: "ContentMd",
                table: "Requirements",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ContentHtml",
                table: "Requirements",
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
                table: "Requirements",
                newName: "ContentMD");

            migrationBuilder.RenameColumn(
                name: "ContentHtml",
                table: "Requirements",
                newName: "ContentHTML");

            migrationBuilder.AlterColumn<string>(
                name: "ContentMD",
                table: "Requirements",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContentHTML",
                table: "Requirements",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
