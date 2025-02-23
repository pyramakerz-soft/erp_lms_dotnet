using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class InventoryMasterFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryMaster_InventoryFlags_FlagId",
                table: "InventoryMaster");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryMaster_InventoryFlags_FlagId",
                table: "InventoryMaster",
                column: "FlagId",
                principalTable: "InventoryFlags",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryMaster_InventoryFlags_FlagId",
                table: "InventoryMaster");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryMaster_InventoryFlags_FlagId",
                table: "InventoryMaster",
                column: "FlagId",
                principalTable: "InventoryFlags",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
