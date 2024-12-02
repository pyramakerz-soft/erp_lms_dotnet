using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class DeletedByInsertedByUpdatedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Employees_DeletedByUserId",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCategory_Employees_DeletedByUserId",
                table: "BusCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Domains_DomainId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Employees_DeletedByUserId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Employees_InsertedByUserId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Employees_UpdatedByUserId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BusRestrict_Employees_DeletedByUserId",
                table: "BusRestrict");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStatus_Employees_DeletedByUserId",
                table: "BusStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusType_Employees_DeletedByUserId",
                table: "BusType");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeType_EmployeeTypeID",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Pages_Employees_DeletedByUserId",
                table: "Pages");

            migrationBuilder.DropForeignKey(
                name: "FK_Pages_Employees_InsertedByUserId",
                table: "Pages");

            migrationBuilder.DropForeignKey(
                name: "FK_Pages_Employees_UpdatedByUserId",
                table: "Pages");

            migrationBuilder.DropForeignKey(
                name: "FK_Pyramakerz_Employees_DeletedByUserId",
                table: "Pyramakerz");

            migrationBuilder.DropForeignKey(
                name: "FK_Pyramakerz_Employees_InsertedByUserId",
                table: "Pyramakerz");

            migrationBuilder.DropForeignKey(
                name: "FK_Pyramakerz_Employees_UpdatedByUserId",
                table: "Pyramakerz");

            migrationBuilder.DropIndex(
                name: "IX_Pyramakerz_DeletedByUserId",
                table: "Pyramakerz");

            migrationBuilder.DropIndex(
                name: "IX_Pyramakerz_InsertedByUserId",
                table: "Pyramakerz");

            migrationBuilder.DropIndex(
                name: "IX_Pyramakerz_UpdatedByUserId",
                table: "Pyramakerz");

            migrationBuilder.DropIndex(
                name: "IX_Pages_DeletedByUserId",
                table: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Pages_InsertedByUserId",
                table: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Pages_UpdatedByUserId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Pyramakerz");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Pyramakerz");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "Pyramakerz");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "Pyramakerz");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Pyramakerz");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Pyramakerz");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Pyramakerz");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Pages");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Semester",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "Semester",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "Semester",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "Semester",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Semester",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Semester",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "Semester",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "AcademicYear",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "AcademicYear",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "AcademicYear",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "AcademicYear",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AcademicYear",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "AcademicYear",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "AcademicYear",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Semester_DeletedByUserId",
                table: "Semester",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Semester_InsertedByUserId",
                table: "Semester",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Semester_UpdatedByUserId",
                table: "Semester",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicYear_DeletedByUserId",
                table: "AcademicYear",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicYear_InsertedByUserId",
                table: "AcademicYear",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicYear_UpdatedByUserId",
                table: "AcademicYear",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYear_Employees_DeletedByUserId",
                table: "AcademicYear",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYear_Employees_InsertedByUserId",
                table: "AcademicYear",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYear_Employees_UpdatedByUserId",
                table: "AcademicYear",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Employees_DeletedByUserId",
                table: "Bus",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_BusCategory_Employees_DeletedByUserId",
                table: "BusCategory",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Domains_DomainId",
                table: "BusCompany",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Employees_DeletedByUserId",
                table: "BusCompany",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Employees_InsertedByUserId",
                table: "BusCompany",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Employees_UpdatedByUserId",
                table: "BusCompany",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusRestrict_Employees_DeletedByUserId",
                table: "BusRestrict",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStatus_Employees_DeletedByUserId",
                table: "BusStatus",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusType_Employees_DeletedByUserId",
                table: "BusType",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeType_EmployeeTypeID",
                table: "Employees",
                column: "EmployeeTypeID",
                principalTable: "EmployeeType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Semester_Employees_DeletedByUserId",
                table: "Semester",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Semester_Employees_InsertedByUserId",
                table: "Semester",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Semester_Employees_UpdatedByUserId",
                table: "Semester",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYear_Employees_DeletedByUserId",
                table: "AcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYear_Employees_InsertedByUserId",
                table: "AcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYear_Employees_UpdatedByUserId",
                table: "AcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Employees_DeletedByUserId",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCategory_Employees_DeletedByUserId",
                table: "BusCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Domains_DomainId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Employees_DeletedByUserId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Employees_InsertedByUserId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Employees_UpdatedByUserId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BusRestrict_Employees_DeletedByUserId",
                table: "BusRestrict");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStatus_Employees_DeletedByUserId",
                table: "BusStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusType_Employees_DeletedByUserId",
                table: "BusType");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeType_EmployeeTypeID",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Semester_Employees_DeletedByUserId",
                table: "Semester");

            migrationBuilder.DropForeignKey(
                name: "FK_Semester_Employees_InsertedByUserId",
                table: "Semester");

            migrationBuilder.DropForeignKey(
                name: "FK_Semester_Employees_UpdatedByUserId",
                table: "Semester");

            migrationBuilder.DropIndex(
                name: "IX_Semester_DeletedByUserId",
                table: "Semester");

            migrationBuilder.DropIndex(
                name: "IX_Semester_InsertedByUserId",
                table: "Semester");

            migrationBuilder.DropIndex(
                name: "IX_Semester_UpdatedByUserId",
                table: "Semester");

            migrationBuilder.DropIndex(
                name: "IX_AcademicYear_DeletedByUserId",
                table: "AcademicYear");

            migrationBuilder.DropIndex(
                name: "IX_AcademicYear_InsertedByUserId",
                table: "AcademicYear");

            migrationBuilder.DropIndex(
                name: "IX_AcademicYear_UpdatedByUserId",
                table: "AcademicYear");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Semester");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Semester");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "Semester");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "Semester");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Semester");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Semester");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Semester");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "AcademicYear");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "AcademicYear");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "AcademicYear");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "AcademicYear");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AcademicYear");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "AcademicYear");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "AcademicYear");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Pyramakerz",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "Pyramakerz",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "Pyramakerz",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "Pyramakerz",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Pyramakerz",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Pyramakerz",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "Pyramakerz",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Pages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "Pages",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "Pages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "Pages",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Pages",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Pages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "Pages",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pyramakerz_DeletedByUserId",
                table: "Pyramakerz",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pyramakerz_InsertedByUserId",
                table: "Pyramakerz",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pyramakerz_UpdatedByUserId",
                table: "Pyramakerz",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_DeletedByUserId",
                table: "Pages",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_InsertedByUserId",
                table: "Pages",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_UpdatedByUserId",
                table: "Pages",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Employees_DeletedByUserId",
                table: "Bus",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusCategory_Employees_DeletedByUserId",
                table: "BusCategory",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Domains_DomainId",
                table: "BusCompany",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Employees_DeletedByUserId",
                table: "BusCompany",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Employees_InsertedByUserId",
                table: "BusCompany",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Employees_UpdatedByUserId",
                table: "BusCompany",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusRestrict_Employees_DeletedByUserId",
                table: "BusRestrict",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusStatus_Employees_DeletedByUserId",
                table: "BusStatus",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusType_Employees_DeletedByUserId",
                table: "BusType",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeType_EmployeeTypeID",
                table: "Employees",
                column: "EmployeeTypeID",
                principalTable: "EmployeeType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Employees_DeletedByUserId",
                table: "Pages",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Employees_InsertedByUserId",
                table: "Pages",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Employees_UpdatedByUserId",
                table: "Pages",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pyramakerz_Employees_DeletedByUserId",
                table: "Pyramakerz",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pyramakerz_Employees_InsertedByUserId",
                table: "Pyramakerz",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pyramakerz_Employees_UpdatedByUserId",
                table: "Pyramakerz",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");
        }
    }
}
