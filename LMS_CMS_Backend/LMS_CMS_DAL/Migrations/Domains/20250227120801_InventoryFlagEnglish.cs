using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class InventoryFlagEnglish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "InventoryFlags",
                newName: "en_Title");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "InventoryFlags",
                newName: "enName");

            migrationBuilder.AddColumn<string>(
                name: "arName",
                table: "InventoryFlags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ar_Title",
                table: "InventoryFlags",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "arName",
                table: "InventoryFlags");

            migrationBuilder.DropColumn(
                name: "ar_Title",
                table: "InventoryFlags");

            migrationBuilder.RenameColumn(
                name: "en_Title",
                table: "InventoryFlags",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "enName",
                table: "InventoryFlags",
                newName: "Name");
        }
    }
}
