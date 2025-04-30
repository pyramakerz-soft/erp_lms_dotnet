using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.LMS_CMS_
{
    /// <inheritdoc />
    public partial class updateInventoryMaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BuildingNumber",
                table: "School",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "School",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CitySubdivision",
                table: "School",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalZone",
                table: "School",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetName",
                table: "School",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SchoolId",
                table: "InventoryMaster",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMaster_SchoolId",
                table: "InventoryMaster",
                column: "SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryMaster_School_SchoolId",
                table: "InventoryMaster",
                column: "SchoolId",
                principalTable: "School",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryMaster_School_SchoolId",
                table: "InventoryMaster");

            migrationBuilder.DropIndex(
                name: "IX_InventoryMaster_SchoolId",
                table: "InventoryMaster");

            migrationBuilder.DropColumn(
                name: "BuildingNumber",
                table: "School");

            migrationBuilder.DropColumn(
                name: "City",
                table: "School");

            migrationBuilder.DropColumn(
                name: "CitySubdivision",
                table: "School");

            migrationBuilder.DropColumn(
                name: "PostalZone",
                table: "School");

            migrationBuilder.DropColumn(
                name: "StreetName",
                table: "School");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "InventoryMaster");
        }
    }
}
