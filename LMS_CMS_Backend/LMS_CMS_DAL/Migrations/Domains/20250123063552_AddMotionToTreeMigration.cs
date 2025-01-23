using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class AddMotionToTreeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MotionTypeID",
                table: "AccountingTreeCharts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_AccountingTreeCharts_MotionTypeID",
                table: "AccountingTreeCharts",
                column: "MotionTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingTreeCharts_MotionTypes_MotionTypeID",
                table: "AccountingTreeCharts",
                column: "MotionTypeID",
                principalTable: "MotionTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountingTreeCharts_MotionTypes_MotionTypeID",
                table: "AccountingTreeCharts");

            migrationBuilder.DropIndex(
                name: "IX_AccountingTreeCharts_MotionTypeID",
                table: "AccountingTreeCharts");

            migrationBuilder.DropColumn(
                name: "MotionTypeID",
                table: "AccountingTreeCharts");
        }
    }
}
