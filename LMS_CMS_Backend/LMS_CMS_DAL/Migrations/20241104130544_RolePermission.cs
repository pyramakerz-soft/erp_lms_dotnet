using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class RolePermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailed_Permissions_Master_Detailes_Permissions_Master_Detailed_Permissions_ID",
                table: "Role_Detailed_Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailed_Permissions_Roles_Role_ID",
                table: "Role_Detailed_Permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role_Detailed_Permissions",
                table: "Role_Detailed_Permissions");

            migrationBuilder.RenameTable(
                name: "Role_Detailed_Permissions",
                newName: "Role_Permissions");

            migrationBuilder.RenameIndex(
                name: "IX_Role_Detailed_Permissions_Role_ID",
                table: "Role_Permissions",
                newName: "IX_Role_Permissions_Role_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Role_Detailed_Permissions_Master_Detailed_Permissions_ID",
                table: "Role_Permissions",
                newName: "IX_Role_Permissions_Master_Detailed_Permissions_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role_Permissions",
                table: "Role_Permissions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Permissions_Master_Detailes_Permissions_Master_Detailed_Permissions_ID",
                table: "Role_Permissions",
                column: "Master_Detailed_Permissions_ID",
                principalTable: "Master_Detailes_Permissions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Permissions_Roles_Role_ID",
                table: "Role_Permissions",
                column: "Role_ID",
                principalTable: "Roles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role_Permissions_Master_Detailes_Permissions_Master_Detailed_Permissions_ID",
                table: "Role_Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Permissions_Roles_Role_ID",
                table: "Role_Permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role_Permissions",
                table: "Role_Permissions");

            migrationBuilder.RenameTable(
                name: "Role_Permissions",
                newName: "Role_Detailed_Permissions");

            migrationBuilder.RenameIndex(
                name: "IX_Role_Permissions_Role_ID",
                table: "Role_Detailed_Permissions",
                newName: "IX_Role_Detailed_Permissions_Role_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Role_Permissions_Master_Detailed_Permissions_ID",
                table: "Role_Detailed_Permissions",
                newName: "IX_Role_Detailed_Permissions_Master_Detailed_Permissions_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role_Detailed_Permissions",
                table: "Role_Detailed_Permissions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailed_Permissions_Master_Detailes_Permissions_Master_Detailed_Permissions_ID",
                table: "Role_Detailed_Permissions",
                column: "Master_Detailed_Permissions_ID",
                principalTable: "Master_Detailes_Permissions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailed_Permissions_Roles_Role_ID",
                table: "Role_Detailed_Permissions",
                column: "Role_ID",
                principalTable: "Roles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
