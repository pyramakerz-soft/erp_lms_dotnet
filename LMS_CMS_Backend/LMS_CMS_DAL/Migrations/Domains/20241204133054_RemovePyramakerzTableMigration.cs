using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class RemovePyramakerzTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYear_Pyramakerz_DeletedByPyramakerzId",
                table: "AcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYear_Pyramakerz_InsertedByPyramakerzId",
                table: "AcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYear_Pyramakerz_UpdatedByPyramakerzId",
                table: "AcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Domains_DomainID",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Pyramakerz_DeletedByPyramakerzId",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Pyramakerz_InsertedByPyramakerzId",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Pyramakerz_UpdatedByPyramakerzId",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCategory_Domains_DomainId",
                table: "BusCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCategory_Pyramakerz_DeletedByPyramakerzId",
                table: "BusCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCategory_Pyramakerz_InsertedByPyramakerzId",
                table: "BusCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCategory_Pyramakerz_UpdatedByPyramakerzId",
                table: "BusCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Domains_DomainId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Pyramakerz_DeletedByPyramakerzId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Pyramakerz_InsertedByPyramakerzId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Pyramakerz_UpdatedByPyramakerzId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BusRestrict_Domains_DomainId",
                table: "BusRestrict");

            migrationBuilder.DropForeignKey(
                name: "FK_BusRestrict_Pyramakerz_DeletedByPyramakerzId",
                table: "BusRestrict");

            migrationBuilder.DropForeignKey(
                name: "FK_BusRestrict_Pyramakerz_InsertedByPyramakerzId",
                table: "BusRestrict");

            migrationBuilder.DropForeignKey(
                name: "FK_BusRestrict_Pyramakerz_UpdatedByPyramakerzId",
                table: "BusRestrict");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStatus_Domains_DomainId",
                table: "BusStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStatus_Pyramakerz_DeletedByPyramakerzId",
                table: "BusStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStatus_Pyramakerz_InsertedByPyramakerzId",
                table: "BusStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStatus_Pyramakerz_UpdatedByPyramakerzId",
                table: "BusStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStudent_Pyramakerz_DeletedByPyramakerzId",
                table: "BusStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStudent_Pyramakerz_InsertedByPyramakerzId",
                table: "BusStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStudent_Pyramakerz_UpdatedByPyramakerzId",
                table: "BusStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusType_Domains_DomainId",
                table: "BusType");

            migrationBuilder.DropForeignKey(
                name: "FK_BusType_Pyramakerz_DeletedByPyramakerzId",
                table: "BusType");

            migrationBuilder.DropForeignKey(
                name: "FK_BusType_Pyramakerz_InsertedByPyramakerzId",
                table: "BusType");

            migrationBuilder.DropForeignKey(
                name: "FK_BusType_Pyramakerz_UpdatedByPyramakerzId",
                table: "BusType");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Domains_Domain_ID",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Pyramakerz_DeletedByPyramakerzId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Pyramakerz_InsertedByPyramakerzId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Pyramakerz_UpdatedByPyramakerzId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Pyramakerz_DeletedByPyramakerzId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Pyramakerz_InsertedByPyramakerzId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Pyramakerz_UpdatedByPyramakerzId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailes_Pyramakerz_DeletedByPyramakerzId",
                table: "Role_Detailes");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailes_Pyramakerz_InsertedByPyramakerzId",
                table: "Role_Detailes");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailes_Pyramakerz_UpdatedByPyramakerzId",
                table: "Role_Detailes");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Domains_Domain_ID",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Pyramakerz_DeletedByPyramakerzId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Pyramakerz_InsertedByPyramakerzId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Pyramakerz_UpdatedByPyramakerzId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Domains_Domain_id",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Pyramakerz_DeletedByPyramakerzId",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Pyramakerz_InsertedByPyramakerzId",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Pyramakerz_UpdatedByPyramakerzId",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Semester_Pyramakerz_DeletedByPyramakerzId",
                table: "Semester");

            migrationBuilder.DropForeignKey(
                name: "FK_Semester_Pyramakerz_InsertedByPyramakerzId",
                table: "Semester");

            migrationBuilder.DropForeignKey(
                name: "FK_Semester_Pyramakerz_UpdatedByPyramakerzId",
                table: "Semester");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Pyramakerz_DeletedByPyramakerzId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Pyramakerz_InsertedByPyramakerzId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Pyramakerz_UpdatedByPyramakerzId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Domain_Page_Details");

            migrationBuilder.DropTable(
                name: "Domains");

            migrationBuilder.DropTable(
                name: "Pyramakerz");

            migrationBuilder.DropIndex(
                name: "IX_Schools_Domain_id",
                table: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_Roles_Domain_ID",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Employees_Domain_ID",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_BusType_DomainId",
                table: "BusType");

            migrationBuilder.DropIndex(
                name: "IX_BusStatus_DomainId",
                table: "BusStatus");

            migrationBuilder.DropIndex(
                name: "IX_BusRestrict_DomainId",
                table: "BusRestrict");

            migrationBuilder.DropIndex(
                name: "IX_BusCompany_DomainId",
                table: "BusCompany");

            migrationBuilder.DropIndex(
                name: "IX_BusCategory_DomainId",
                table: "BusCategory");

            migrationBuilder.DropIndex(
                name: "IX_Bus_DomainID",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "Domain_id",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "Domain_ID",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Domain_ID",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DomainId",
                table: "BusType");

            migrationBuilder.DropColumn(
                name: "DomainId",
                table: "BusStatus");

            migrationBuilder.DropColumn(
                name: "DomainId",
                table: "BusRestrict");

            migrationBuilder.DropColumn(
                name: "DomainId",
                table: "BusCompany");

            migrationBuilder.DropColumn(
                name: "DomainId",
                table: "BusCategory");

            migrationBuilder.DropColumn(
                name: "DomainID",
                table: "Bus");

            migrationBuilder.RenameColumn(
                name: "UpdatedByPyramakerzId",
                table: "Students",
                newName: "UpdatedByOctaId");

            migrationBuilder.RenameColumn(
                name: "InsertedByPyramakerzId",
                table: "Students",
                newName: "InsertedByOctaId");

            migrationBuilder.RenameColumn(
                name: "DeletedByPyramakerzId",
                table: "Students",
                newName: "DeletedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_UpdatedByPyramakerzId",
                table: "Students",
                newName: "IX_Students_UpdatedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_InsertedByPyramakerzId",
                table: "Students",
                newName: "IX_Students_InsertedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_DeletedByPyramakerzId",
                table: "Students",
                newName: "IX_Students_DeletedByOctaId");

            migrationBuilder.RenameColumn(
                name: "UpdatedByPyramakerzId",
                table: "Semester",
                newName: "UpdatedByOctaId");

            migrationBuilder.RenameColumn(
                name: "InsertedByPyramakerzId",
                table: "Semester",
                newName: "InsertedByOctaId");

            migrationBuilder.RenameColumn(
                name: "DeletedByPyramakerzId",
                table: "Semester",
                newName: "DeletedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_Semester_UpdatedByPyramakerzId",
                table: "Semester",
                newName: "IX_Semester_UpdatedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_Semester_InsertedByPyramakerzId",
                table: "Semester",
                newName: "IX_Semester_InsertedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_Semester_DeletedByPyramakerzId",
                table: "Semester",
                newName: "IX_Semester_DeletedByOctaId");

            migrationBuilder.RenameColumn(
                name: "UpdatedByPyramakerzId",
                table: "Schools",
                newName: "UpdatedByOctaId");

            migrationBuilder.RenameColumn(
                name: "InsertedByPyramakerzId",
                table: "Schools",
                newName: "InsertedByOctaId");

            migrationBuilder.RenameColumn(
                name: "DeletedByPyramakerzId",
                table: "Schools",
                newName: "DeletedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_Schools_UpdatedByPyramakerzId",
                table: "Schools",
                newName: "IX_Schools_UpdatedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_Schools_InsertedByPyramakerzId",
                table: "Schools",
                newName: "IX_Schools_InsertedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_Schools_DeletedByPyramakerzId",
                table: "Schools",
                newName: "IX_Schools_DeletedByOctaId");

            migrationBuilder.RenameColumn(
                name: "UpdatedByPyramakerzId",
                table: "Roles",
                newName: "UpdatedByOctaId");

            migrationBuilder.RenameColumn(
                name: "InsertedByPyramakerzId",
                table: "Roles",
                newName: "InsertedByOctaId");

            migrationBuilder.RenameColumn(
                name: "DeletedByPyramakerzId",
                table: "Roles",
                newName: "DeletedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_UpdatedByPyramakerzId",
                table: "Roles",
                newName: "IX_Roles_UpdatedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_InsertedByPyramakerzId",
                table: "Roles",
                newName: "IX_Roles_InsertedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_DeletedByPyramakerzId",
                table: "Roles",
                newName: "IX_Roles_DeletedByOctaId");

            migrationBuilder.RenameColumn(
                name: "UpdatedByPyramakerzId",
                table: "Role_Detailes",
                newName: "UpdatedByOctaId");

            migrationBuilder.RenameColumn(
                name: "InsertedByPyramakerzId",
                table: "Role_Detailes",
                newName: "InsertedByOctaId");

            migrationBuilder.RenameColumn(
                name: "DeletedByPyramakerzId",
                table: "Role_Detailes",
                newName: "DeletedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_Role_Detailes_UpdatedByPyramakerzId",
                table: "Role_Detailes",
                newName: "IX_Role_Detailes_UpdatedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_Role_Detailes_InsertedByPyramakerzId",
                table: "Role_Detailes",
                newName: "IX_Role_Detailes_InsertedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_Role_Detailes_DeletedByPyramakerzId",
                table: "Role_Detailes",
                newName: "IX_Role_Detailes_DeletedByOctaId");

            migrationBuilder.RenameColumn(
                name: "UpdatedByPyramakerzId",
                table: "Parents",
                newName: "UpdatedByOctaId");

            migrationBuilder.RenameColumn(
                name: "InsertedByPyramakerzId",
                table: "Parents",
                newName: "InsertedByOctaId");

            migrationBuilder.RenameColumn(
                name: "DeletedByPyramakerzId",
                table: "Parents",
                newName: "DeletedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_Parents_UpdatedByPyramakerzId",
                table: "Parents",
                newName: "IX_Parents_UpdatedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_Parents_InsertedByPyramakerzId",
                table: "Parents",
                newName: "IX_Parents_InsertedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_Parents_DeletedByPyramakerzId",
                table: "Parents",
                newName: "IX_Parents_DeletedByOctaId");

            migrationBuilder.RenameColumn(
                name: "UpdatedByPyramakerzId",
                table: "Employees",
                newName: "UpdatedByOctaId");

            migrationBuilder.RenameColumn(
                name: "InsertedByPyramakerzId",
                table: "Employees",
                newName: "InsertedByOctaId");

            migrationBuilder.RenameColumn(
                name: "DeletedByPyramakerzId",
                table: "Employees",
                newName: "DeletedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_UpdatedByPyramakerzId",
                table: "Employees",
                newName: "IX_Employees_UpdatedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_InsertedByPyramakerzId",
                table: "Employees",
                newName: "IX_Employees_InsertedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_DeletedByPyramakerzId",
                table: "Employees",
                newName: "IX_Employees_DeletedByOctaId");

            migrationBuilder.RenameColumn(
                name: "UpdatedByPyramakerzId",
                table: "BusType",
                newName: "UpdatedByOctaId");

            migrationBuilder.RenameColumn(
                name: "InsertedByPyramakerzId",
                table: "BusType",
                newName: "InsertedByOctaId");

            migrationBuilder.RenameColumn(
                name: "DeletedByPyramakerzId",
                table: "BusType",
                newName: "DeletedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_BusType_UpdatedByPyramakerzId",
                table: "BusType",
                newName: "IX_BusType_UpdatedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_BusType_InsertedByPyramakerzId",
                table: "BusType",
                newName: "IX_BusType_InsertedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_BusType_DeletedByPyramakerzId",
                table: "BusType",
                newName: "IX_BusType_DeletedByOctaId");

            migrationBuilder.RenameColumn(
                name: "UpdatedByPyramakerzId",
                table: "BusStudent",
                newName: "UpdatedByOctaId");

            migrationBuilder.RenameColumn(
                name: "InsertedByPyramakerzId",
                table: "BusStudent",
                newName: "InsertedByOctaId");

            migrationBuilder.RenameColumn(
                name: "DeletedByPyramakerzId",
                table: "BusStudent",
                newName: "DeletedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_BusStudent_UpdatedByPyramakerzId",
                table: "BusStudent",
                newName: "IX_BusStudent_UpdatedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_BusStudent_InsertedByPyramakerzId",
                table: "BusStudent",
                newName: "IX_BusStudent_InsertedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_BusStudent_DeletedByPyramakerzId",
                table: "BusStudent",
                newName: "IX_BusStudent_DeletedByOctaId");

            migrationBuilder.RenameColumn(
                name: "UpdatedByPyramakerzId",
                table: "BusStatus",
                newName: "UpdatedByOctaId");

            migrationBuilder.RenameColumn(
                name: "InsertedByPyramakerzId",
                table: "BusStatus",
                newName: "InsertedByOctaId");

            migrationBuilder.RenameColumn(
                name: "DeletedByPyramakerzId",
                table: "BusStatus",
                newName: "DeletedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_BusStatus_UpdatedByPyramakerzId",
                table: "BusStatus",
                newName: "IX_BusStatus_UpdatedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_BusStatus_InsertedByPyramakerzId",
                table: "BusStatus",
                newName: "IX_BusStatus_InsertedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_BusStatus_DeletedByPyramakerzId",
                table: "BusStatus",
                newName: "IX_BusStatus_DeletedByOctaId");

            migrationBuilder.RenameColumn(
                name: "UpdatedByPyramakerzId",
                table: "BusRestrict",
                newName: "UpdatedByOctaId");

            migrationBuilder.RenameColumn(
                name: "InsertedByPyramakerzId",
                table: "BusRestrict",
                newName: "InsertedByOctaId");

            migrationBuilder.RenameColumn(
                name: "DeletedByPyramakerzId",
                table: "BusRestrict",
                newName: "DeletedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_BusRestrict_UpdatedByPyramakerzId",
                table: "BusRestrict",
                newName: "IX_BusRestrict_UpdatedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_BusRestrict_InsertedByPyramakerzId",
                table: "BusRestrict",
                newName: "IX_BusRestrict_InsertedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_BusRestrict_DeletedByPyramakerzId",
                table: "BusRestrict",
                newName: "IX_BusRestrict_DeletedByOctaId");

            migrationBuilder.RenameColumn(
                name: "UpdatedByPyramakerzId",
                table: "BusCompany",
                newName: "UpdatedByOctaId");

            migrationBuilder.RenameColumn(
                name: "InsertedByPyramakerzId",
                table: "BusCompany",
                newName: "InsertedByOctaId");

            migrationBuilder.RenameColumn(
                name: "DeletedByPyramakerzId",
                table: "BusCompany",
                newName: "DeletedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_BusCompany_UpdatedByPyramakerzId",
                table: "BusCompany",
                newName: "IX_BusCompany_UpdatedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_BusCompany_InsertedByPyramakerzId",
                table: "BusCompany",
                newName: "IX_BusCompany_InsertedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_BusCompany_DeletedByPyramakerzId",
                table: "BusCompany",
                newName: "IX_BusCompany_DeletedByOctaId");

            migrationBuilder.RenameColumn(
                name: "UpdatedByPyramakerzId",
                table: "BusCategory",
                newName: "UpdatedByOctaId");

            migrationBuilder.RenameColumn(
                name: "InsertedByPyramakerzId",
                table: "BusCategory",
                newName: "InsertedByOctaId");

            migrationBuilder.RenameColumn(
                name: "DeletedByPyramakerzId",
                table: "BusCategory",
                newName: "DeletedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_BusCategory_UpdatedByPyramakerzId",
                table: "BusCategory",
                newName: "IX_BusCategory_UpdatedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_BusCategory_InsertedByPyramakerzId",
                table: "BusCategory",
                newName: "IX_BusCategory_InsertedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_BusCategory_DeletedByPyramakerzId",
                table: "BusCategory",
                newName: "IX_BusCategory_DeletedByOctaId");

            migrationBuilder.RenameColumn(
                name: "UpdatedByPyramakerzId",
                table: "Bus",
                newName: "UpdatedByOctaId");

            migrationBuilder.RenameColumn(
                name: "InsertedByPyramakerzId",
                table: "Bus",
                newName: "InsertedByOctaId");

            migrationBuilder.RenameColumn(
                name: "DeletedByPyramakerzId",
                table: "Bus",
                newName: "DeletedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_Bus_UpdatedByPyramakerzId",
                table: "Bus",
                newName: "IX_Bus_UpdatedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_Bus_InsertedByPyramakerzId",
                table: "Bus",
                newName: "IX_Bus_InsertedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_Bus_DeletedByPyramakerzId",
                table: "Bus",
                newName: "IX_Bus_DeletedByOctaId");

            migrationBuilder.RenameColumn(
                name: "UpdatedByPyramakerzId",
                table: "AcademicYear",
                newName: "UpdatedByOctaId");

            migrationBuilder.RenameColumn(
                name: "InsertedByPyramakerzId",
                table: "AcademicYear",
                newName: "InsertedByOctaId");

            migrationBuilder.RenameColumn(
                name: "DeletedByPyramakerzId",
                table: "AcademicYear",
                newName: "DeletedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_AcademicYear_UpdatedByPyramakerzId",
                table: "AcademicYear",
                newName: "IX_AcademicYear_UpdatedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_AcademicYear_InsertedByPyramakerzId",
                table: "AcademicYear",
                newName: "IX_AcademicYear_InsertedByOctaId");

            migrationBuilder.RenameIndex(
                name: "IX_AcademicYear_DeletedByPyramakerzId",
                table: "AcademicYear",
                newName: "IX_AcademicYear_DeletedByOctaId");

            migrationBuilder.CreateTable(
                name: "Octa",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Octa", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYear_Octa_DeletedByOctaId",
                table: "AcademicYear",
                column: "DeletedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYear_Octa_InsertedByOctaId",
                table: "AcademicYear",
                column: "InsertedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYear_Octa_UpdatedByOctaId",
                table: "AcademicYear",
                column: "UpdatedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Octa_DeletedByOctaId",
                table: "Bus",
                column: "DeletedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Octa_InsertedByOctaId",
                table: "Bus",
                column: "InsertedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Octa_UpdatedByOctaId",
                table: "Bus",
                column: "UpdatedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCategory_Octa_DeletedByOctaId",
                table: "BusCategory",
                column: "DeletedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCategory_Octa_InsertedByOctaId",
                table: "BusCategory",
                column: "InsertedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCategory_Octa_UpdatedByOctaId",
                table: "BusCategory",
                column: "UpdatedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Octa_DeletedByOctaId",
                table: "BusCompany",
                column: "DeletedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Octa_InsertedByOctaId",
                table: "BusCompany",
                column: "InsertedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Octa_UpdatedByOctaId",
                table: "BusCompany",
                column: "UpdatedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusRestrict_Octa_DeletedByOctaId",
                table: "BusRestrict",
                column: "DeletedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusRestrict_Octa_InsertedByOctaId",
                table: "BusRestrict",
                column: "InsertedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusRestrict_Octa_UpdatedByOctaId",
                table: "BusRestrict",
                column: "UpdatedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStatus_Octa_DeletedByOctaId",
                table: "BusStatus",
                column: "DeletedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStatus_Octa_InsertedByOctaId",
                table: "BusStatus",
                column: "InsertedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStatus_Octa_UpdatedByOctaId",
                table: "BusStatus",
                column: "UpdatedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStudent_Octa_DeletedByOctaId",
                table: "BusStudent",
                column: "DeletedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStudent_Octa_InsertedByOctaId",
                table: "BusStudent",
                column: "InsertedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStudent_Octa_UpdatedByOctaId",
                table: "BusStudent",
                column: "UpdatedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusType_Octa_DeletedByOctaId",
                table: "BusType",
                column: "DeletedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusType_Octa_InsertedByOctaId",
                table: "BusType",
                column: "InsertedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusType_Octa_UpdatedByOctaId",
                table: "BusType",
                column: "UpdatedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Octa_DeletedByOctaId",
                table: "Employees",
                column: "DeletedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Octa_InsertedByOctaId",
                table: "Employees",
                column: "InsertedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Octa_UpdatedByOctaId",
                table: "Employees",
                column: "UpdatedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Octa_DeletedByOctaId",
                table: "Parents",
                column: "DeletedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Octa_InsertedByOctaId",
                table: "Parents",
                column: "InsertedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Octa_UpdatedByOctaId",
                table: "Parents",
                column: "UpdatedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailes_Octa_DeletedByOctaId",
                table: "Role_Detailes",
                column: "DeletedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailes_Octa_InsertedByOctaId",
                table: "Role_Detailes",
                column: "InsertedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailes_Octa_UpdatedByOctaId",
                table: "Role_Detailes",
                column: "UpdatedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Octa_DeletedByOctaId",
                table: "Roles",
                column: "DeletedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Octa_InsertedByOctaId",
                table: "Roles",
                column: "InsertedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Octa_UpdatedByOctaId",
                table: "Roles",
                column: "UpdatedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Octa_DeletedByOctaId",
                table: "Schools",
                column: "DeletedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Octa_InsertedByOctaId",
                table: "Schools",
                column: "InsertedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Octa_UpdatedByOctaId",
                table: "Schools",
                column: "UpdatedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Semester_Octa_DeletedByOctaId",
                table: "Semester",
                column: "DeletedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Semester_Octa_InsertedByOctaId",
                table: "Semester",
                column: "InsertedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Semester_Octa_UpdatedByOctaId",
                table: "Semester",
                column: "UpdatedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Octa_DeletedByOctaId",
                table: "Students",
                column: "DeletedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Octa_InsertedByOctaId",
                table: "Students",
                column: "InsertedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Octa_UpdatedByOctaId",
                table: "Students",
                column: "UpdatedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYear_Octa_DeletedByOctaId",
                table: "AcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYear_Octa_InsertedByOctaId",
                table: "AcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYear_Octa_UpdatedByOctaId",
                table: "AcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Octa_DeletedByOctaId",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Octa_InsertedByOctaId",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Octa_UpdatedByOctaId",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCategory_Octa_DeletedByOctaId",
                table: "BusCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCategory_Octa_InsertedByOctaId",
                table: "BusCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCategory_Octa_UpdatedByOctaId",
                table: "BusCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Octa_DeletedByOctaId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Octa_InsertedByOctaId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Octa_UpdatedByOctaId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BusRestrict_Octa_DeletedByOctaId",
                table: "BusRestrict");

            migrationBuilder.DropForeignKey(
                name: "FK_BusRestrict_Octa_InsertedByOctaId",
                table: "BusRestrict");

            migrationBuilder.DropForeignKey(
                name: "FK_BusRestrict_Octa_UpdatedByOctaId",
                table: "BusRestrict");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStatus_Octa_DeletedByOctaId",
                table: "BusStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStatus_Octa_InsertedByOctaId",
                table: "BusStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStatus_Octa_UpdatedByOctaId",
                table: "BusStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStudent_Octa_DeletedByOctaId",
                table: "BusStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStudent_Octa_InsertedByOctaId",
                table: "BusStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStudent_Octa_UpdatedByOctaId",
                table: "BusStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusType_Octa_DeletedByOctaId",
                table: "BusType");

            migrationBuilder.DropForeignKey(
                name: "FK_BusType_Octa_InsertedByOctaId",
                table: "BusType");

            migrationBuilder.DropForeignKey(
                name: "FK_BusType_Octa_UpdatedByOctaId",
                table: "BusType");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Octa_DeletedByOctaId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Octa_InsertedByOctaId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Octa_UpdatedByOctaId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Octa_DeletedByOctaId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Octa_InsertedByOctaId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Octa_UpdatedByOctaId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailes_Octa_DeletedByOctaId",
                table: "Role_Detailes");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailes_Octa_InsertedByOctaId",
                table: "Role_Detailes");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailes_Octa_UpdatedByOctaId",
                table: "Role_Detailes");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Octa_DeletedByOctaId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Octa_InsertedByOctaId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Octa_UpdatedByOctaId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Octa_DeletedByOctaId",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Octa_InsertedByOctaId",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Octa_UpdatedByOctaId",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Semester_Octa_DeletedByOctaId",
                table: "Semester");

            migrationBuilder.DropForeignKey(
                name: "FK_Semester_Octa_InsertedByOctaId",
                table: "Semester");

            migrationBuilder.DropForeignKey(
                name: "FK_Semester_Octa_UpdatedByOctaId",
                table: "Semester");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Octa_DeletedByOctaId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Octa_InsertedByOctaId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Octa_UpdatedByOctaId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Octa");

            migrationBuilder.RenameColumn(
                name: "UpdatedByOctaId",
                table: "Students",
                newName: "UpdatedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "InsertedByOctaId",
                table: "Students",
                newName: "InsertedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "DeletedByOctaId",
                table: "Students",
                newName: "DeletedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_UpdatedByOctaId",
                table: "Students",
                newName: "IX_Students_UpdatedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_InsertedByOctaId",
                table: "Students",
                newName: "IX_Students_InsertedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_DeletedByOctaId",
                table: "Students",
                newName: "IX_Students_DeletedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "UpdatedByOctaId",
                table: "Semester",
                newName: "UpdatedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "InsertedByOctaId",
                table: "Semester",
                newName: "InsertedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "DeletedByOctaId",
                table: "Semester",
                newName: "DeletedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_Semester_UpdatedByOctaId",
                table: "Semester",
                newName: "IX_Semester_UpdatedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_Semester_InsertedByOctaId",
                table: "Semester",
                newName: "IX_Semester_InsertedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_Semester_DeletedByOctaId",
                table: "Semester",
                newName: "IX_Semester_DeletedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "UpdatedByOctaId",
                table: "Schools",
                newName: "UpdatedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "InsertedByOctaId",
                table: "Schools",
                newName: "InsertedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "DeletedByOctaId",
                table: "Schools",
                newName: "DeletedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_Schools_UpdatedByOctaId",
                table: "Schools",
                newName: "IX_Schools_UpdatedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_Schools_InsertedByOctaId",
                table: "Schools",
                newName: "IX_Schools_InsertedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_Schools_DeletedByOctaId",
                table: "Schools",
                newName: "IX_Schools_DeletedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "UpdatedByOctaId",
                table: "Roles",
                newName: "UpdatedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "InsertedByOctaId",
                table: "Roles",
                newName: "InsertedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "DeletedByOctaId",
                table: "Roles",
                newName: "DeletedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_UpdatedByOctaId",
                table: "Roles",
                newName: "IX_Roles_UpdatedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_InsertedByOctaId",
                table: "Roles",
                newName: "IX_Roles_InsertedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_DeletedByOctaId",
                table: "Roles",
                newName: "IX_Roles_DeletedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "UpdatedByOctaId",
                table: "Role_Detailes",
                newName: "UpdatedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "InsertedByOctaId",
                table: "Role_Detailes",
                newName: "InsertedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "DeletedByOctaId",
                table: "Role_Detailes",
                newName: "DeletedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_Role_Detailes_UpdatedByOctaId",
                table: "Role_Detailes",
                newName: "IX_Role_Detailes_UpdatedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_Role_Detailes_InsertedByOctaId",
                table: "Role_Detailes",
                newName: "IX_Role_Detailes_InsertedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_Role_Detailes_DeletedByOctaId",
                table: "Role_Detailes",
                newName: "IX_Role_Detailes_DeletedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "UpdatedByOctaId",
                table: "Parents",
                newName: "UpdatedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "InsertedByOctaId",
                table: "Parents",
                newName: "InsertedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "DeletedByOctaId",
                table: "Parents",
                newName: "DeletedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_Parents_UpdatedByOctaId",
                table: "Parents",
                newName: "IX_Parents_UpdatedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_Parents_InsertedByOctaId",
                table: "Parents",
                newName: "IX_Parents_InsertedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_Parents_DeletedByOctaId",
                table: "Parents",
                newName: "IX_Parents_DeletedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "UpdatedByOctaId",
                table: "Employees",
                newName: "UpdatedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "InsertedByOctaId",
                table: "Employees",
                newName: "InsertedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "DeletedByOctaId",
                table: "Employees",
                newName: "DeletedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_UpdatedByOctaId",
                table: "Employees",
                newName: "IX_Employees_UpdatedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_InsertedByOctaId",
                table: "Employees",
                newName: "IX_Employees_InsertedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_DeletedByOctaId",
                table: "Employees",
                newName: "IX_Employees_DeletedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "UpdatedByOctaId",
                table: "BusType",
                newName: "UpdatedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "InsertedByOctaId",
                table: "BusType",
                newName: "InsertedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "DeletedByOctaId",
                table: "BusType",
                newName: "DeletedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_BusType_UpdatedByOctaId",
                table: "BusType",
                newName: "IX_BusType_UpdatedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_BusType_InsertedByOctaId",
                table: "BusType",
                newName: "IX_BusType_InsertedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_BusType_DeletedByOctaId",
                table: "BusType",
                newName: "IX_BusType_DeletedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "UpdatedByOctaId",
                table: "BusStudent",
                newName: "UpdatedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "InsertedByOctaId",
                table: "BusStudent",
                newName: "InsertedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "DeletedByOctaId",
                table: "BusStudent",
                newName: "DeletedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_BusStudent_UpdatedByOctaId",
                table: "BusStudent",
                newName: "IX_BusStudent_UpdatedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_BusStudent_InsertedByOctaId",
                table: "BusStudent",
                newName: "IX_BusStudent_InsertedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_BusStudent_DeletedByOctaId",
                table: "BusStudent",
                newName: "IX_BusStudent_DeletedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "UpdatedByOctaId",
                table: "BusStatus",
                newName: "UpdatedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "InsertedByOctaId",
                table: "BusStatus",
                newName: "InsertedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "DeletedByOctaId",
                table: "BusStatus",
                newName: "DeletedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_BusStatus_UpdatedByOctaId",
                table: "BusStatus",
                newName: "IX_BusStatus_UpdatedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_BusStatus_InsertedByOctaId",
                table: "BusStatus",
                newName: "IX_BusStatus_InsertedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_BusStatus_DeletedByOctaId",
                table: "BusStatus",
                newName: "IX_BusStatus_DeletedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "UpdatedByOctaId",
                table: "BusRestrict",
                newName: "UpdatedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "InsertedByOctaId",
                table: "BusRestrict",
                newName: "InsertedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "DeletedByOctaId",
                table: "BusRestrict",
                newName: "DeletedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_BusRestrict_UpdatedByOctaId",
                table: "BusRestrict",
                newName: "IX_BusRestrict_UpdatedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_BusRestrict_InsertedByOctaId",
                table: "BusRestrict",
                newName: "IX_BusRestrict_InsertedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_BusRestrict_DeletedByOctaId",
                table: "BusRestrict",
                newName: "IX_BusRestrict_DeletedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "UpdatedByOctaId",
                table: "BusCompany",
                newName: "UpdatedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "InsertedByOctaId",
                table: "BusCompany",
                newName: "InsertedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "DeletedByOctaId",
                table: "BusCompany",
                newName: "DeletedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_BusCompany_UpdatedByOctaId",
                table: "BusCompany",
                newName: "IX_BusCompany_UpdatedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_BusCompany_InsertedByOctaId",
                table: "BusCompany",
                newName: "IX_BusCompany_InsertedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_BusCompany_DeletedByOctaId",
                table: "BusCompany",
                newName: "IX_BusCompany_DeletedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "UpdatedByOctaId",
                table: "BusCategory",
                newName: "UpdatedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "InsertedByOctaId",
                table: "BusCategory",
                newName: "InsertedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "DeletedByOctaId",
                table: "BusCategory",
                newName: "DeletedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_BusCategory_UpdatedByOctaId",
                table: "BusCategory",
                newName: "IX_BusCategory_UpdatedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_BusCategory_InsertedByOctaId",
                table: "BusCategory",
                newName: "IX_BusCategory_InsertedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_BusCategory_DeletedByOctaId",
                table: "BusCategory",
                newName: "IX_BusCategory_DeletedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "UpdatedByOctaId",
                table: "Bus",
                newName: "UpdatedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "InsertedByOctaId",
                table: "Bus",
                newName: "InsertedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "DeletedByOctaId",
                table: "Bus",
                newName: "DeletedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_Bus_UpdatedByOctaId",
                table: "Bus",
                newName: "IX_Bus_UpdatedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_Bus_InsertedByOctaId",
                table: "Bus",
                newName: "IX_Bus_InsertedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_Bus_DeletedByOctaId",
                table: "Bus",
                newName: "IX_Bus_DeletedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "UpdatedByOctaId",
                table: "AcademicYear",
                newName: "UpdatedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "InsertedByOctaId",
                table: "AcademicYear",
                newName: "InsertedByPyramakerzId");

            migrationBuilder.RenameColumn(
                name: "DeletedByOctaId",
                table: "AcademicYear",
                newName: "DeletedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_AcademicYear_UpdatedByOctaId",
                table: "AcademicYear",
                newName: "IX_AcademicYear_UpdatedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_AcademicYear_InsertedByOctaId",
                table: "AcademicYear",
                newName: "IX_AcademicYear_InsertedByPyramakerzId");

            migrationBuilder.RenameIndex(
                name: "IX_AcademicYear_DeletedByOctaId",
                table: "AcademicYear",
                newName: "IX_AcademicYear_DeletedByPyramakerzId");

            migrationBuilder.AddColumn<long>(
                name: "Domain_id",
                table: "Schools",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Domain_ID",
                table: "Roles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Domain_ID",
                table: "Employees",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DomainId",
                table: "BusType",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DomainId",
                table: "BusStatus",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DomainId",
                table: "BusRestrict",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DomainId",
                table: "BusCompany",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DomainId",
                table: "BusCategory",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DomainID",
                table: "Bus",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Pyramakerz",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    User_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pyramakerz", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Domains",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeletedByPyramakerzId = table.Column<long>(type: "bigint", nullable: true),
                    DeletedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    InsertedByPyramakerzId = table.Column<long>(type: "bigint", nullable: true),
                    InsertedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByPyramakerzId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domains", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Domains_Employees_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Domains_Employees_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Domains_Employees_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Domains_Pyramakerz_DeletedByPyramakerzId",
                        column: x => x.DeletedByPyramakerzId,
                        principalTable: "Pyramakerz",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Domains_Pyramakerz_InsertedByPyramakerzId",
                        column: x => x.InsertedByPyramakerzId,
                        principalTable: "Pyramakerz",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Domains_Pyramakerz_UpdatedByPyramakerzId",
                        column: x => x.UpdatedByPyramakerzId,
                        principalTable: "Pyramakerz",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Domain_Page_Details",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeletedByPyramakerzId = table.Column<long>(type: "bigint", nullable: true),
                    DeletedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    Domain_ID = table.Column<long>(type: "bigint", nullable: false),
                    InsertedByPyramakerzId = table.Column<long>(type: "bigint", nullable: true),
                    InsertedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    Page_ID = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedByPyramakerzId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domain_Page_Details", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Domain_Page_Details_Domains_Domain_ID",
                        column: x => x.Domain_ID,
                        principalTable: "Domains",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Domain_Page_Details_Employees_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Domain_Page_Details_Employees_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Domain_Page_Details_Employees_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Domain_Page_Details_Pages_Page_ID",
                        column: x => x.Page_ID,
                        principalTable: "Pages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Domain_Page_Details_Pyramakerz_DeletedByPyramakerzId",
                        column: x => x.DeletedByPyramakerzId,
                        principalTable: "Pyramakerz",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Domain_Page_Details_Pyramakerz_InsertedByPyramakerzId",
                        column: x => x.InsertedByPyramakerzId,
                        principalTable: "Pyramakerz",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Domain_Page_Details_Pyramakerz_UpdatedByPyramakerzId",
                        column: x => x.UpdatedByPyramakerzId,
                        principalTable: "Pyramakerz",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schools_Domain_id",
                table: "Schools",
                column: "Domain_id");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Domain_ID",
                table: "Roles",
                column: "Domain_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Domain_ID",
                table: "Employees",
                column: "Domain_ID");

            migrationBuilder.CreateIndex(
                name: "IX_BusType_DomainId",
                table: "BusType",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStatus_DomainId",
                table: "BusStatus",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_BusRestrict_DomainId",
                table: "BusRestrict",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_BusCompany_DomainId",
                table: "BusCompany",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_BusCategory_DomainId",
                table: "BusCategory",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_DomainID",
                table: "Bus",
                column: "DomainID");

            migrationBuilder.CreateIndex(
                name: "IX_Domain_Page_Details_DeletedByPyramakerzId",
                table: "Domain_Page_Details",
                column: "DeletedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Domain_Page_Details_DeletedByUserId",
                table: "Domain_Page_Details",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Domain_Page_Details_Domain_ID",
                table: "Domain_Page_Details",
                column: "Domain_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Domain_Page_Details_InsertedByPyramakerzId",
                table: "Domain_Page_Details",
                column: "InsertedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Domain_Page_Details_InsertedByUserId",
                table: "Domain_Page_Details",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Domain_Page_Details_Page_ID",
                table: "Domain_Page_Details",
                column: "Page_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Domain_Page_Details_UpdatedByPyramakerzId",
                table: "Domain_Page_Details",
                column: "UpdatedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Domain_Page_Details_UpdatedByUserId",
                table: "Domain_Page_Details",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Domains_DeletedByPyramakerzId",
                table: "Domains",
                column: "DeletedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Domains_DeletedByUserId",
                table: "Domains",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Domains_InsertedByPyramakerzId",
                table: "Domains",
                column: "InsertedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Domains_InsertedByUserId",
                table: "Domains",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Domains_Name",
                table: "Domains",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Domains_UpdatedByPyramakerzId",
                table: "Domains",
                column: "UpdatedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Domains_UpdatedByUserId",
                table: "Domains",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pyramakerz_Email",
                table: "Pyramakerz",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pyramakerz_User_Name",
                table: "Pyramakerz",
                column: "User_Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYear_Pyramakerz_DeletedByPyramakerzId",
                table: "AcademicYear",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYear_Pyramakerz_InsertedByPyramakerzId",
                table: "AcademicYear",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYear_Pyramakerz_UpdatedByPyramakerzId",
                table: "AcademicYear",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Domains_DomainID",
                table: "Bus",
                column: "DomainID",
                principalTable: "Domains",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Pyramakerz_DeletedByPyramakerzId",
                table: "Bus",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Pyramakerz_InsertedByPyramakerzId",
                table: "Bus",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Pyramakerz_UpdatedByPyramakerzId",
                table: "Bus",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCategory_Domains_DomainId",
                table: "BusCategory",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusCategory_Pyramakerz_DeletedByPyramakerzId",
                table: "BusCategory",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCategory_Pyramakerz_InsertedByPyramakerzId",
                table: "BusCategory",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCategory_Pyramakerz_UpdatedByPyramakerzId",
                table: "BusCategory",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Domains_DomainId",
                table: "BusCompany",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Pyramakerz_DeletedByPyramakerzId",
                table: "BusCompany",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Pyramakerz_InsertedByPyramakerzId",
                table: "BusCompany",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Pyramakerz_UpdatedByPyramakerzId",
                table: "BusCompany",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusRestrict_Domains_DomainId",
                table: "BusRestrict",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusRestrict_Pyramakerz_DeletedByPyramakerzId",
                table: "BusRestrict",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusRestrict_Pyramakerz_InsertedByPyramakerzId",
                table: "BusRestrict",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusRestrict_Pyramakerz_UpdatedByPyramakerzId",
                table: "BusRestrict",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStatus_Domains_DomainId",
                table: "BusStatus",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusStatus_Pyramakerz_DeletedByPyramakerzId",
                table: "BusStatus",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStatus_Pyramakerz_InsertedByPyramakerzId",
                table: "BusStatus",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStatus_Pyramakerz_UpdatedByPyramakerzId",
                table: "BusStatus",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStudent_Pyramakerz_DeletedByPyramakerzId",
                table: "BusStudent",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStudent_Pyramakerz_InsertedByPyramakerzId",
                table: "BusStudent",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStudent_Pyramakerz_UpdatedByPyramakerzId",
                table: "BusStudent",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusType_Domains_DomainId",
                table: "BusType",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusType_Pyramakerz_DeletedByPyramakerzId",
                table: "BusType",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusType_Pyramakerz_InsertedByPyramakerzId",
                table: "BusType",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusType_Pyramakerz_UpdatedByPyramakerzId",
                table: "BusType",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Domains_Domain_ID",
                table: "Employees",
                column: "Domain_ID",
                principalTable: "Domains",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Pyramakerz_DeletedByPyramakerzId",
                table: "Employees",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Pyramakerz_InsertedByPyramakerzId",
                table: "Employees",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Pyramakerz_UpdatedByPyramakerzId",
                table: "Employees",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Pyramakerz_DeletedByPyramakerzId",
                table: "Parents",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Pyramakerz_InsertedByPyramakerzId",
                table: "Parents",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Pyramakerz_UpdatedByPyramakerzId",
                table: "Parents",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailes_Pyramakerz_DeletedByPyramakerzId",
                table: "Role_Detailes",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailes_Pyramakerz_InsertedByPyramakerzId",
                table: "Role_Detailes",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailes_Pyramakerz_UpdatedByPyramakerzId",
                table: "Role_Detailes",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Domains_Domain_ID",
                table: "Roles",
                column: "Domain_ID",
                principalTable: "Domains",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Pyramakerz_DeletedByPyramakerzId",
                table: "Roles",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Pyramakerz_InsertedByPyramakerzId",
                table: "Roles",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Pyramakerz_UpdatedByPyramakerzId",
                table: "Roles",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Domains_Domain_id",
                table: "Schools",
                column: "Domain_id",
                principalTable: "Domains",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Pyramakerz_DeletedByPyramakerzId",
                table: "Schools",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Pyramakerz_InsertedByPyramakerzId",
                table: "Schools",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Pyramakerz_UpdatedByPyramakerzId",
                table: "Schools",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Semester_Pyramakerz_DeletedByPyramakerzId",
                table: "Semester",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Semester_Pyramakerz_InsertedByPyramakerzId",
                table: "Semester",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Semester_Pyramakerz_UpdatedByPyramakerzId",
                table: "Semester",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Pyramakerz_DeletedByPyramakerzId",
                table: "Students",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Pyramakerz_InsertedByPyramakerzId",
                table: "Students",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Pyramakerz_UpdatedByPyramakerzId",
                table: "Students",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");
        }
    }
}
