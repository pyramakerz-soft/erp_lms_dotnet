using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class SupplierAndStoreInInventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "StudentID",
                table: "InventoryMaster",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "StoreToTransformId",
                table: "InventoryMaster",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SupplierId",
                table: "InventoryMaster",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMaster_StoreToTransformId",
                table: "InventoryMaster",
                column: "StoreToTransformId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMaster_SupplierId",
                table: "InventoryMaster",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryMaster_Store_StoreToTransformId",
                table: "InventoryMaster",
                column: "StoreToTransformId",
                principalTable: "Store",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryMaster_Suppliers_SupplierId",
                table: "InventoryMaster",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryMaster_Store_StoreToTransformId",
                table: "InventoryMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryMaster_Suppliers_SupplierId",
                table: "InventoryMaster");

            migrationBuilder.DropIndex(
                name: "IX_InventoryMaster_StoreToTransformId",
                table: "InventoryMaster");

            migrationBuilder.DropIndex(
                name: "IX_InventoryMaster_SupplierId",
                table: "InventoryMaster");

            migrationBuilder.DropColumn(
                name: "StoreToTransformId",
                table: "InventoryMaster");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "InventoryMaster");

            migrationBuilder.AlterColumn<long>(
                name: "StudentID",
                table: "InventoryMaster",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
