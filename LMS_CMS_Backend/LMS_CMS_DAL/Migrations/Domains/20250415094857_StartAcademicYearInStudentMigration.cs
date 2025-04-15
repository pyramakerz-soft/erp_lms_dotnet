using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class StartAcademicYearInStudentMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StartAcademicYearID",
                table: "Student",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_StartAcademicYearID",
                table: "Student",
                column: "StartAcademicYearID");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_AcademicYear_StartAcademicYearID",
                table: "Student",
                column: "StartAcademicYearID",
                principalTable: "AcademicYear",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_AcademicYear_StartAcademicYearID",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_StartAcademicYearID",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "StartAcademicYearID",
                table: "Student");
        }
    }
}
