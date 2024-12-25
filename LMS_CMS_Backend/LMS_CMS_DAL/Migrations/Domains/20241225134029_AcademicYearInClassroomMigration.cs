using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class AcademicYearInClassroomMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AcademicYearID",
                table: "Classroom",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Classroom_AcademicYearID",
                table: "Classroom",
                column: "AcademicYearID");

            migrationBuilder.AddForeignKey(
                name: "FK_Classroom_AcademicYear_AcademicYearID",
                table: "Classroom",
                column: "AcademicYearID",
                principalTable: "AcademicYear",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classroom_AcademicYear_AcademicYearID",
                table: "Classroom");

            migrationBuilder.DropIndex(
                name: "IX_Classroom_AcademicYearID",
                table: "Classroom");

            migrationBuilder.DropColumn(
                name: "AcademicYearID",
                table: "Classroom");
        }
    }
}
