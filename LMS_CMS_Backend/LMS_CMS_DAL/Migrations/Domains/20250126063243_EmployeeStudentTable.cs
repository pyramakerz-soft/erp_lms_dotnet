using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class EmployeeStudentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Employee_EmployeeID",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_EmployeeID",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "Student");

            migrationBuilder.CreateTable(
                name: "EmployeeStudent",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<long>(type: "bigint", nullable: false),
                    EmployeeID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeStudent", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EmployeeStudent_Employee_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeStudent_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeStudent_EmployeeID",
                table: "EmployeeStudent",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeStudent_StudentID",
                table: "EmployeeStudent",
                column: "StudentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeStudent");

            migrationBuilder.AddColumn<long>(
                name: "EmployeeID",
                table: "Student",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_EmployeeID",
                table: "Student",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Employee_EmployeeID",
                table: "Student",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
