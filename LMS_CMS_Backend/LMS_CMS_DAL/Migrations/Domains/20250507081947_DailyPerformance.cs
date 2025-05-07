using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class DailyPerformance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentPerformance_Subject_SubjectID",
                table: "StudentPerformance");

            migrationBuilder.AlterColumn<long>(
                name: "SubjectID",
                table: "StudentPerformance",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "DailyPerformanceID",
                table: "StudentPerformance",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "DailyPerformance",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubjectID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyPerformance", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DailyPerformance_Subject_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subject",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentPerformance_DailyPerformanceID",
                table: "StudentPerformance",
                column: "DailyPerformanceID");

            migrationBuilder.CreateIndex(
                name: "IX_DailyPerformance_SubjectID",
                table: "DailyPerformance",
                column: "SubjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPerformance_DailyPerformance_DailyPerformanceID",
                table: "StudentPerformance",
                column: "DailyPerformanceID",
                principalTable: "DailyPerformance",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPerformance_Subject_SubjectID",
                table: "StudentPerformance",
                column: "SubjectID",
                principalTable: "Subject",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentPerformance_DailyPerformance_DailyPerformanceID",
                table: "StudentPerformance");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPerformance_Subject_SubjectID",
                table: "StudentPerformance");

            migrationBuilder.DropTable(
                name: "DailyPerformance");

            migrationBuilder.DropIndex(
                name: "IX_StudentPerformance_DailyPerformanceID",
                table: "StudentPerformance");

            migrationBuilder.DropColumn(
                name: "DailyPerformanceID",
                table: "StudentPerformance");

            migrationBuilder.AlterColumn<long>(
                name: "SubjectID",
                table: "StudentPerformance",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPerformance_Subject_SubjectID",
                table: "StudentPerformance",
                column: "SubjectID",
                principalTable: "Subject",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
