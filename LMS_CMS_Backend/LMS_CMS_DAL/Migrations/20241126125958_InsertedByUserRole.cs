using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class InsertedByUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InsertedByUserRole",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertedByUserRole",
                table: "Semester",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertedByUserRole",
                table: "Schools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertedByUserRole",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertedByUserRole",
                table: "Role_Detailes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertedByUserRole",
                table: "Parents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertedByUserRole",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertedByUserRole",
                table: "Domains",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertedByUserRole",
                table: "Domain_Page_Details",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertedByUserRole",
                table: "BusType",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertedByUserRole",
                table: "BusStudent",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertedByUserRole",
                table: "BusStatus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertedByUserRole",
                table: "BusRestrict",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertedByUserRole",
                table: "BusCompany",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertedByUserRole",
                table: "BusCategory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertedByUserRole",
                table: "Bus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertedByUserRole",
                table: "AcademicYear",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "Semester");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "Role_Detailes");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "Domains");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "Domain_Page_Details");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "BusType");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "BusStudent");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "BusStatus");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "BusRestrict");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "BusCompany");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "BusCategory");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "AcademicYear");
        }
    }
}
