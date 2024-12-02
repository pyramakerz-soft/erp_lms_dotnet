using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class EmployeeRelationInAuditable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "Semester");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "Semester");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "Semester");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "Role_Detailes");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "Role_Detailes");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "Role_Detailes");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "Domains");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "Domains");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "Domains");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "Domain_Page_Details");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "Domain_Page_Details");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "Domain_Page_Details");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "BusType");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "BusType");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "BusType");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "BusStudent");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "BusStudent");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "BusStudent");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "BusStatus");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "BusStatus");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "BusStatus");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "BusRestrict");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "BusRestrict");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "BusRestrict");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "BusCompany");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "BusCompany");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "BusCompany");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "BusCategory");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "BusCategory");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "BusCategory");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "DeletedByUserRole",
                table: "AcademicYear");

            migrationBuilder.DropColumn(
                name: "InsertedByUserRole",
                table: "AcademicYear");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserRole",
                table: "AcademicYear");

            migrationBuilder.AddColumn<long>(
                name: "DeletedByPyramakerzId",
                table: "Students",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByPyramakerzId",
                table: "Students",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByPyramakerzId",
                table: "Students",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByPyramakerzId",
                table: "Semester",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByPyramakerzId",
                table: "Semester",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByPyramakerzId",
                table: "Semester",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByPyramakerzId",
                table: "Schools",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByPyramakerzId",
                table: "Schools",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByPyramakerzId",
                table: "Schools",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByPyramakerzId",
                table: "Roles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByPyramakerzId",
                table: "Roles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByPyramakerzId",
                table: "Roles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByPyramakerzId",
                table: "Role_Detailes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByPyramakerzId",
                table: "Role_Detailes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByPyramakerzId",
                table: "Role_Detailes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByPyramakerzId",
                table: "Parents",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByPyramakerzId",
                table: "Parents",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByPyramakerzId",
                table: "Parents",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByPyramakerzId",
                table: "Employees",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByPyramakerzId",
                table: "Employees",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByPyramakerzId",
                table: "Employees",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByPyramakerzId",
                table: "Domains",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByPyramakerzId",
                table: "Domains",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByPyramakerzId",
                table: "Domains",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByPyramakerzId",
                table: "Domain_Page_Details",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByPyramakerzId",
                table: "Domain_Page_Details",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByPyramakerzId",
                table: "Domain_Page_Details",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByPyramakerzId",
                table: "BusType",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByPyramakerzId",
                table: "BusType",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByPyramakerzId",
                table: "BusType",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByPyramakerzId",
                table: "BusStudent",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByPyramakerzId",
                table: "BusStudent",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByPyramakerzId",
                table: "BusStudent",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByPyramakerzId",
                table: "BusStatus",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByPyramakerzId",
                table: "BusStatus",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByPyramakerzId",
                table: "BusStatus",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByPyramakerzId",
                table: "BusRestrict",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByPyramakerzId",
                table: "BusRestrict",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByPyramakerzId",
                table: "BusRestrict",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByPyramakerzId",
                table: "BusCompany",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByPyramakerzId",
                table: "BusCompany",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByPyramakerzId",
                table: "BusCompany",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByPyramakerzId",
                table: "BusCategory",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByPyramakerzId",
                table: "BusCategory",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByPyramakerzId",
                table: "BusCategory",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByPyramakerzId",
                table: "Bus",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByPyramakerzId",
                table: "Bus",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByPyramakerzId",
                table: "Bus",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByPyramakerzId",
                table: "AcademicYear",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByPyramakerzId",
                table: "AcademicYear",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByPyramakerzId",
                table: "AcademicYear",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedByPyramakerzId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "InsertedByPyramakerzId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UpdatedByPyramakerzId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DeletedByPyramakerzId",
                table: "Semester");

            migrationBuilder.DropColumn(
                name: "InsertedByPyramakerzId",
                table: "Semester");

            migrationBuilder.DropColumn(
                name: "UpdatedByPyramakerzId",
                table: "Semester");

            migrationBuilder.DropColumn(
                name: "DeletedByPyramakerzId",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "InsertedByPyramakerzId",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "UpdatedByPyramakerzId",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "DeletedByPyramakerzId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "InsertedByPyramakerzId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UpdatedByPyramakerzId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DeletedByPyramakerzId",
                table: "Role_Detailes");

            migrationBuilder.DropColumn(
                name: "InsertedByPyramakerzId",
                table: "Role_Detailes");

            migrationBuilder.DropColumn(
                name: "UpdatedByPyramakerzId",
                table: "Role_Detailes");

            migrationBuilder.DropColumn(
                name: "DeletedByPyramakerzId",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "InsertedByPyramakerzId",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "UpdatedByPyramakerzId",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "DeletedByPyramakerzId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "InsertedByPyramakerzId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UpdatedByPyramakerzId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DeletedByPyramakerzId",
                table: "Domains");

            migrationBuilder.DropColumn(
                name: "InsertedByPyramakerzId",
                table: "Domains");

            migrationBuilder.DropColumn(
                name: "UpdatedByPyramakerzId",
                table: "Domains");

            migrationBuilder.DropColumn(
                name: "DeletedByPyramakerzId",
                table: "Domain_Page_Details");

            migrationBuilder.DropColumn(
                name: "InsertedByPyramakerzId",
                table: "Domain_Page_Details");

            migrationBuilder.DropColumn(
                name: "UpdatedByPyramakerzId",
                table: "Domain_Page_Details");

            migrationBuilder.DropColumn(
                name: "DeletedByPyramakerzId",
                table: "BusType");

            migrationBuilder.DropColumn(
                name: "InsertedByPyramakerzId",
                table: "BusType");

            migrationBuilder.DropColumn(
                name: "UpdatedByPyramakerzId",
                table: "BusType");

            migrationBuilder.DropColumn(
                name: "DeletedByPyramakerzId",
                table: "BusStudent");

            migrationBuilder.DropColumn(
                name: "InsertedByPyramakerzId",
                table: "BusStudent");

            migrationBuilder.DropColumn(
                name: "UpdatedByPyramakerzId",
                table: "BusStudent");

            migrationBuilder.DropColumn(
                name: "DeletedByPyramakerzId",
                table: "BusStatus");

            migrationBuilder.DropColumn(
                name: "InsertedByPyramakerzId",
                table: "BusStatus");

            migrationBuilder.DropColumn(
                name: "UpdatedByPyramakerzId",
                table: "BusStatus");

            migrationBuilder.DropColumn(
                name: "DeletedByPyramakerzId",
                table: "BusRestrict");

            migrationBuilder.DropColumn(
                name: "InsertedByPyramakerzId",
                table: "BusRestrict");

            migrationBuilder.DropColumn(
                name: "UpdatedByPyramakerzId",
                table: "BusRestrict");

            migrationBuilder.DropColumn(
                name: "DeletedByPyramakerzId",
                table: "BusCompany");

            migrationBuilder.DropColumn(
                name: "InsertedByPyramakerzId",
                table: "BusCompany");

            migrationBuilder.DropColumn(
                name: "UpdatedByPyramakerzId",
                table: "BusCompany");

            migrationBuilder.DropColumn(
                name: "DeletedByPyramakerzId",
                table: "BusCategory");

            migrationBuilder.DropColumn(
                name: "InsertedByPyramakerzId",
                table: "BusCategory");

            migrationBuilder.DropColumn(
                name: "UpdatedByPyramakerzId",
                table: "BusCategory");

            migrationBuilder.DropColumn(
                name: "DeletedByPyramakerzId",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "InsertedByPyramakerzId",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "UpdatedByPyramakerzId",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "DeletedByPyramakerzId",
                table: "AcademicYear");

            migrationBuilder.DropColumn(
                name: "InsertedByPyramakerzId",
                table: "AcademicYear");

            migrationBuilder.DropColumn(
                name: "UpdatedByPyramakerzId",
                table: "AcademicYear");

            migrationBuilder.AddColumn<string>(
                name: "DeletedByUserRole",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertedByUserRole",
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
                name: "InsertedByUserRole",
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
                name: "InsertedByUserRole",
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
                name: "InsertedByUserRole",
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
                name: "InsertedByUserRole",
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
                name: "InsertedByUserRole",
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
                name: "InsertedByUserRole",
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
                name: "InsertedByUserRole",
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
                name: "InsertedByUserRole",
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
                name: "InsertedByUserRole",
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
                name: "InsertedByUserRole",
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
                name: "InsertedByUserRole",
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
                name: "InsertedByUserRole",
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
                name: "InsertedByUserRole",
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
                name: "InsertedByUserRole",
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
                name: "InsertedByUserRole",
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
                name: "InsertedByUserRole",
                table: "AcademicYear",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserRole",
                table: "AcademicYear",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
