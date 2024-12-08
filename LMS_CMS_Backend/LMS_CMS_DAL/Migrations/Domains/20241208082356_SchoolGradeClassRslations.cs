using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class SchoolGradeClassRslations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SchoolID",
                table: "Grade",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "GradeID",
                table: "Class",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Grade_SchoolID",
                table: "Grade",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_Class_GradeID",
                table: "Class",
                column: "GradeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Class_Grade_GradeID",
                table: "Class",
                column: "GradeID",
                principalTable: "Grade",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Schools_SchoolID",
                table: "Grade",
                column: "SchoolID",
                principalTable: "Schools",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Class_Grade_GradeID",
                table: "Class");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Schools_SchoolID",
                table: "Grade");

            migrationBuilder.DropIndex(
                name: "IX_Grade_SchoolID",
                table: "Grade");

            migrationBuilder.DropIndex(
                name: "IX_Class_GradeID",
                table: "Class");

            migrationBuilder.DropColumn(
                name: "SchoolID",
                table: "Grade");

            migrationBuilder.DropColumn(
                name: "GradeID",
                table: "Class");
        }
    }
}
