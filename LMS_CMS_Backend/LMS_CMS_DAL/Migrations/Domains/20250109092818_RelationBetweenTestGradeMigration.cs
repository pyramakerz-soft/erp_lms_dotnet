using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class RelationBetweenTestGradeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "GradeID",
                table: "Test",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Test_GradeID",
                table: "Test",
                column: "GradeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Test_Grade_GradeID",
                table: "Test",
                column: "GradeID",
                principalTable: "Grade",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Test_Grade_GradeID",
                table: "Test");

            migrationBuilder.DropIndex(
                name: "IX_Test_GradeID",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "GradeID",
                table: "Test");
        }
    }
}
