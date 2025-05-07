using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class DailyPerformanceStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentPerformance_Student_StudentID",
                table: "StudentPerformance");

            migrationBuilder.AlterColumn<long>(
                name: "StudentID",
                table: "StudentPerformance",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "StudentID",
                table: "DailyPerformance",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_DailyPerformance_StudentID",
                table: "DailyPerformance",
                column: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyPerformance_Student_StudentID",
                table: "DailyPerformance",
                column: "StudentID",
                principalTable: "Student",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPerformance_Student_StudentID",
                table: "StudentPerformance",
                column: "StudentID",
                principalTable: "Student",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyPerformance_Student_StudentID",
                table: "DailyPerformance");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPerformance_Student_StudentID",
                table: "StudentPerformance");

            migrationBuilder.DropIndex(
                name: "IX_DailyPerformance_StudentID",
                table: "DailyPerformance");

            migrationBuilder.DropColumn(
                name: "StudentID",
                table: "DailyPerformance");

            migrationBuilder.AlterColumn<long>(
                name: "StudentID",
                table: "StudentPerformance",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPerformance_Student_StudentID",
                table: "StudentPerformance",
                column: "StudentID",
                principalTable: "Student",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
