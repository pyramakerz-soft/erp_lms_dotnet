using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class HeaderReportSchoolMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReportHeaderOneAr",
                table: "School",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportHeaderOneEn",
                table: "School",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportHeaderTwoAr",
                table: "School",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportHeaderTwoEn",
                table: "School",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportImage",
                table: "School",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReportHeaderOneAr",
                table: "School");

            migrationBuilder.DropColumn(
                name: "ReportHeaderOneEn",
                table: "School");

            migrationBuilder.DropColumn(
                name: "ReportHeaderTwoAr",
                table: "School");

            migrationBuilder.DropColumn(
                name: "ReportHeaderTwoEn",
                table: "School");

            migrationBuilder.DropColumn(
                name: "ReportImage",
                table: "School");
        }
    }
}
