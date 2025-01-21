using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class RemoveSemester : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAcademicYear_Semester_SemesterID",
                table: "StudentAcademicYear");

            migrationBuilder.DropIndex(
                name: "IX_StudentAcademicYear_SemesterID",
                table: "StudentAcademicYear");

            migrationBuilder.DropColumn(
                name: "SemesterID",
                table: "StudentAcademicYear");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SemesterID",
                table: "StudentAcademicYear",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentAcademicYear_SemesterID",
                table: "StudentAcademicYear",
                column: "SemesterID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAcademicYear_Semester_SemesterID",
                table: "StudentAcademicYear",
                column: "SemesterID",
                principalTable: "Semester",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
