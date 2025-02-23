using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class InventoryMaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryDetails_InventoryMaster_SalesID",
                table: "InventoryDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesItemAttachment_InventoryDetails_SalesItemID",
                table: "SalesItemAttachment");

            migrationBuilder.RenameColumn(
                name: "SalesItemID",
                table: "SalesItemAttachment",
                newName: "InventoryDetailsID");

            migrationBuilder.RenameIndex(
                name: "IX_SalesItemAttachment_SalesItemID",
                table: "SalesItemAttachment",
                newName: "IX_SalesItemAttachment_InventoryDetailsID");

            migrationBuilder.RenameColumn(
                name: "SalesID",
                table: "InventoryDetails",
                newName: "InventoryMasterId");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryDetails_SalesID",
                table: "InventoryDetails",
                newName: "IX_InventoryDetails_InventoryMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryDetails_InventoryMaster_InventoryMasterId",
                table: "InventoryDetails",
                column: "InventoryMasterId",
                principalTable: "InventoryMaster",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesItemAttachment_InventoryDetails_InventoryDetailsID",
                table: "SalesItemAttachment",
                column: "InventoryDetailsID",
                principalTable: "InventoryDetails",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryDetails_InventoryMaster_InventoryMasterId",
                table: "InventoryDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesItemAttachment_InventoryDetails_InventoryDetailsID",
                table: "SalesItemAttachment");

            migrationBuilder.RenameColumn(
                name: "InventoryDetailsID",
                table: "SalesItemAttachment",
                newName: "SalesItemID");

            migrationBuilder.RenameIndex(
                name: "IX_SalesItemAttachment_InventoryDetailsID",
                table: "SalesItemAttachment",
                newName: "IX_SalesItemAttachment_SalesItemID");

            migrationBuilder.RenameColumn(
                name: "InventoryMasterId",
                table: "InventoryDetails",
                newName: "SalesID");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryDetails_InventoryMasterId",
                table: "InventoryDetails",
                newName: "IX_InventoryDetails_SalesID");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryDetails_InventoryMaster_SalesID",
                table: "InventoryDetails",
                column: "SalesID",
                principalTable: "InventoryMaster",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesItemAttachment_InventoryDetails_SalesItemID",
                table: "SalesItemAttachment",
                column: "SalesItemID",
                principalTable: "InventoryDetails",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
