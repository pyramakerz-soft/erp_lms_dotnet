using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domain
{
    /// <inheritdoc />
    public partial class RemoveOnDeleteRestrictFromRoleDetailsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailes_Page_Page_ID",
                table: "Role_Detailes");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailes_Role_Role_ID",
                table: "Role_Detailes");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailes_Page_Page_ID",
                table: "Role_Detailes",
                column: "Page_ID",
                principalTable: "Page",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailes_Role_Role_ID",
                table: "Role_Detailes",
                column: "Role_ID",
                principalTable: "Role",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailes_Page_Page_ID",
                table: "Role_Detailes");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailes_Role_Role_ID",
                table: "Role_Detailes");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailes_Page_Page_ID",
                table: "Role_Detailes",
                column: "Page_ID",
                principalTable: "Page",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailes_Role_Role_ID",
                table: "Role_Detailes",
                column: "Role_ID",
                principalTable: "Role",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
