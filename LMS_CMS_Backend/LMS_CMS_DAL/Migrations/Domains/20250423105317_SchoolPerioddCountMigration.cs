using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class SchoolPerioddCountMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaximumPeriodCountRemedials",
                table: "School",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaximumPeriodCountTimeTable",
                table: "School",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaximumPeriodCountRemedials",
                table: "School");

            migrationBuilder.DropColumn(
                name: "MaximumPeriodCountTimeTable",
                table: "School");
        }
    }
}
