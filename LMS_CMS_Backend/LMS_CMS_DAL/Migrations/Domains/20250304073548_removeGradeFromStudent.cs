using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class removeGradeFromStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Grade_GradeId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_GradeId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "Student");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "GradeId",
                table: "Student",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Student_GradeId",
                table: "Student",
                column: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Grade_GradeId",
                table: "Student",
                column: "GradeId",
                principalTable: "Grade",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
