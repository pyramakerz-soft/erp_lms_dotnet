using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class StudentAccountIDMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AccountNumberID",
                table: "Student",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_AccountNumberID",
                table: "Student",
                column: "AccountNumberID");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_AccountingTreeCharts_AccountNumberID",
                table: "Student",
                column: "AccountNumberID",
                principalTable: "AccountingTreeCharts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_AccountingTreeCharts_AccountNumberID",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_AccountNumberID",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "AccountNumberID",
                table: "Student");
        }
    }
}
