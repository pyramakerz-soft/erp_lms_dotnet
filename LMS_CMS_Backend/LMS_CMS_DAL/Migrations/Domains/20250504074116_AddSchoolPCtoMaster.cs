using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.LMS_CMS_
{
    /// <inheritdoc />
    public partial class AddSchoolPCtoMaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SchoolPCId",
                table: "InventoryMaster",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMaster_SchoolPCId",
                table: "InventoryMaster",
                column: "SchoolPCId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryMaster_SchoolPCs_SchoolPCId",
                table: "InventoryMaster",
                column: "SchoolPCId",
                principalTable: "SchoolPCs",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryMaster_SchoolPCs_SchoolPCId",
                table: "InventoryMaster");

            migrationBuilder.DropIndex(
                name: "IX_InventoryMaster_SchoolPCId",
                table: "InventoryMaster");

            migrationBuilder.DropColumn(
                name: "SchoolPCId",
                table: "InventoryMaster");
        }
    }
}
