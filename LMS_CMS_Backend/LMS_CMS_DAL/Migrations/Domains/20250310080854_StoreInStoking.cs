using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class StoreInStoking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StoreID",
                table: "Stocking",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Stocking_StoreID",
                table: "Stocking",
                column: "StoreID");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocking_Store_StoreID",
                table: "Stocking",
                column: "StoreID",
                principalTable: "Store",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocking_Store_StoreID",
                table: "Stocking");

            migrationBuilder.DropIndex(
                name: "IX_Stocking_StoreID",
                table: "Stocking");

            migrationBuilder.DropColumn(
                name: "StoreID",
                table: "Stocking");
        }
    }
}
