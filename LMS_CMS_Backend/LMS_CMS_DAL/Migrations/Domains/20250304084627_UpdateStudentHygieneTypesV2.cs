using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class UpdateStudentHygieneTypesV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActionTaken",
                table: "StudentHygieneTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Attendance",
                table: "StudentHygieneTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "StudentHygieneTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SelectAll",
                table: "StudentHygieneTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionTaken",
                table: "StudentHygieneTypes");

            migrationBuilder.DropColumn(
                name: "Attendance",
                table: "StudentHygieneTypes");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "StudentHygieneTypes");

            migrationBuilder.DropColumn(
                name: "SelectAll",
                table: "StudentHygieneTypes");
        }
    }
}
