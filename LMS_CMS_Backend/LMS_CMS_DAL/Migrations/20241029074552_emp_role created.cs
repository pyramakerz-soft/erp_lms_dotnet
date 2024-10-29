using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class emp_rolecreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Students",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Parents",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Employee_Roles",
                columns: table => new
                {
                    Employee_Id = table.Column<int>(type: "int", nullable: false),
                    Role_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee_Roles", x => new { x.Employee_Id, x.Role_Id });
                    table.ForeignKey(
                        name: "FK_Employee_Roles_Employees_Employee_Id",
                        column: x => x.Employee_Id,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employee_Roles_Roles_Role_Id",
                        column: x => x.Role_Id,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_Email",
                table: "Students",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parents_Email",
                table: "Parents",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Roles_Role_Id",
                table: "Employee_Roles",
                column: "Role_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee_Roles");

            migrationBuilder.DropIndex(
                name: "IX_Students_Email",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Parents_Email",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_Employees_Email",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Parents",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
