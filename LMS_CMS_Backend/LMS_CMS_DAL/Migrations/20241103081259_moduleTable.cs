using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class moduleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Detailed_Permissions_Master_Permissions_Master_Permission_ID",
                table: "Detailed_Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailed_Permissions_Detailed_Permissions_Detailed_Permissions_ID",
                table: "Role_Detailed_Permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role_Detailed_Permissions",
                table: "Role_Detailed_Permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee_Roles",
                table: "Employee_Roles");

            migrationBuilder.DropIndex(
                name: "IX_Detailed_Permissions_Master_Permission_ID",
                table: "Detailed_Permissions");

            migrationBuilder.DropColumn(
                name: "Master_Permission_ID",
                table: "Detailed_Permissions");

            migrationBuilder.RenameColumn(
                name: "Detailed_Permissions_ID",
                table: "Role_Detailed_Permissions",
                newName: "Master_Detailed_Permissions_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Role_Detailed_Permissions_Detailed_Permissions_ID",
                table: "Role_Detailed_Permissions",
                newName: "IX_Role_Detailed_Permissions_Master_Detailed_Permissions_ID");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Role_Detailed_Permissions",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Employee_Roles",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role_Detailed_Permissions",
                table: "Role_Detailed_Permissions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee_Roles",
                table: "Employee_Roles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Master_Detailes_Permissions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Details_Id = table.Column<int>(type: "int", nullable: false),
                    Master_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Master_Detailes_Permissions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Master_Detailes_Permissions_Detailed_Permissions_Details_Id",
                        column: x => x.Details_Id,
                        principalTable: "Detailed_Permissions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Master_Detailes_Permissions_Master_Permissions_Master_Id",
                        column: x => x.Master_Id,
                        principalTable: "Master_Permissions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Modules_Master_permissions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Module_Id = table.Column<int>(type: "int", nullable: false),
                    Master_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules_Master_permissions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Modules_Master_permissions_Master_Permissions_Master_Id",
                        column: x => x.Master_Id,
                        principalTable: "Master_Permissions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Modules_Master_permissions_Modules_Module_Id",
                        column: x => x.Module_Id,
                        principalTable: "Modules",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Role_Detailed_Permissions_Role_ID",
                table: "Role_Detailed_Permissions",
                column: "Role_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Roles_Employee_Id",
                table: "Employee_Roles",
                column: "Employee_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Master_Detailes_Permissions_Details_Id",
                table: "Master_Detailes_Permissions",
                column: "Details_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Master_Detailes_Permissions_Master_Id",
                table: "Master_Detailes_Permissions",
                column: "Master_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_Name",
                table: "Modules",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modules_Master_permissions_Master_Id",
                table: "Modules_Master_permissions",
                column: "Master_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_Master_permissions_Module_Id",
                table: "Modules_Master_permissions",
                column: "Module_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailed_Permissions_Master_Detailes_Permissions_Master_Detailed_Permissions_ID",
                table: "Role_Detailed_Permissions",
                column: "Master_Detailed_Permissions_ID",
                principalTable: "Master_Detailes_Permissions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailed_Permissions_Master_Detailes_Permissions_Master_Detailed_Permissions_ID",
                table: "Role_Detailed_Permissions");

            migrationBuilder.DropTable(
                name: "Master_Detailes_Permissions");

            migrationBuilder.DropTable(
                name: "Modules_Master_permissions");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role_Detailed_Permissions",
                table: "Role_Detailed_Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Role_Detailed_Permissions_Role_ID",
                table: "Role_Detailed_Permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee_Roles",
                table: "Employee_Roles");

            migrationBuilder.DropIndex(
                name: "IX_Employee_Roles_Employee_Id",
                table: "Employee_Roles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Role_Detailed_Permissions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Employee_Roles");

            migrationBuilder.RenameColumn(
                name: "Master_Detailed_Permissions_ID",
                table: "Role_Detailed_Permissions",
                newName: "Detailed_Permissions_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Role_Detailed_Permissions_Master_Detailed_Permissions_ID",
                table: "Role_Detailed_Permissions",
                newName: "IX_Role_Detailed_Permissions_Detailed_Permissions_ID");

            migrationBuilder.AddColumn<int>(
                name: "Master_Permission_ID",
                table: "Detailed_Permissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role_Detailed_Permissions",
                table: "Role_Detailed_Permissions",
                columns: new[] { "Role_ID", "Detailed_Permissions_ID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee_Roles",
                table: "Employee_Roles",
                columns: new[] { "Employee_Id", "Role_Id" });

            migrationBuilder.CreateIndex(
                name: "IX_Detailed_Permissions_Master_Permission_ID",
                table: "Detailed_Permissions",
                column: "Master_Permission_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Detailed_Permissions_Master_Permissions_Master_Permission_ID",
                table: "Detailed_Permissions",
                column: "Master_Permission_ID",
                principalTable: "Master_Permissions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailed_Permissions_Detailed_Permissions_Detailed_Permissions_ID",
                table: "Role_Detailed_Permissions",
                column: "Detailed_Permissions_ID",
                principalTable: "Detailed_Permissions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
