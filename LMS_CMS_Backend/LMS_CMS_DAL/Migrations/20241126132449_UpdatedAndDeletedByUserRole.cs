using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedAndDeletedByUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeletedByUserRole",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserRole",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedByUserRole",
                table: "Semester",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserRole",
                table: "Semester",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedByUserRole",
                table: "Schools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserRole",
                table: "Schools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedByUserRole",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserRole",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedByUserRole",
                table: "Role_Detailes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserRole",
                table: "Role_Detailes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedByUserRole",
                table: "Parents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserRole",
                table: "Parents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedByUserRole",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserRole",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedByUserRole",
                table: "Domains",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserRole",
                table: "Domains",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedByUserRole",
                table: "Domain_Page_Details",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserRole",
                table: "Domain_Page_Details",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedByUserRole",
                table: "BusType",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserRole",
                table: "BusType",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedByUserRole",
                table: "BusStudent",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserRole",
                table: "BusStudent",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedByUserRole",
                table: "BusStatus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserRole",
                table: "BusStatus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedByUserRole",
                table: "BusRestrict",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserRole",
                table: "BusRestrict",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedByUserRole",
                table: "BusCompany",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserRole",
                table: "BusCompany",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedByUserRole",
                table: "BusCategory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserRole",
                table: "BusCategory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedByUserRole",
                table: "Bus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserRole",
                table: "Bus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedByUserRole",
                table: "AcademicYear",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserRole",
                table: "AcademicYear",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "Semester");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "Semester");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "Role_Detailes");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "Role_Detailes");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "Domains");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "Domains");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "Domain_Page_Details");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "Domain_Page_Details");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "BusType");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "BusType");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "BusStudent");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "BusStudent");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "BusStatus");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "BusStatus");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "BusRestrict");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "BusRestrict");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "BusCompany");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "BusCompany");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "BusCategory");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "BusCategory");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "AcademicYear");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "AcademicYear");
        }
    }
}
