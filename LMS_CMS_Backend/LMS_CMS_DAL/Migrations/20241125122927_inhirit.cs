using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class inhirit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                table: "EmployeeType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "EmployeeType",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "EmployeeType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "EmployeeType",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeType",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "EmployeeType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "EmployeeType",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EmployeeTypeID1",
                table: "Employees",
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
                name: "IX_EmployeeType_DeletedByUserId",
                table: "EmployeeType",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeType_InsertedByUserId",
                table: "EmployeeType",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeType_UpdatedByUserId",
                table: "EmployeeType",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeTypeID1",
                table: "Employees",
                column: "EmployeeTypeID1");

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
                name: "FK_Employees_EmployeeType_EmployeeTypeID1",
                table: "Employees",
                column: "EmployeeTypeID1",
                principalTable: "EmployeeType",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeType_Employees_DeletedByUserId",
                table: "EmployeeType",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeType_Employees_InsertedByUserId",
                table: "EmployeeType",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeType_Employees_UpdatedByUserId",
                table: "EmployeeType",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

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
                name: "FK_Employees_EmployeeType_EmployeeTypeID1",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeType_Employees_DeletedByUserId",
                table: "EmployeeType");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeType_Employees_InsertedByUserId",
                table: "EmployeeType");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeType_Employees_UpdatedByUserId",
                table: "EmployeeType");

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
                name: "IX_EmployeeType_DeletedByUserId",
                table: "EmployeeType");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeType_InsertedByUserId",
                table: "EmployeeType");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeType_UpdatedByUserId",
                table: "EmployeeType");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmployeeTypeID1",
                table: "Employees");

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
                table: "EmployeeType");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "EmployeeType");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "EmployeeType");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "EmployeeType");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "EmployeeType");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "EmployeeType");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "EmployeeType");

            migrationBuilder.DropColumn(
                name: "EmployeeTypeID1",
                table: "Employees");

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
        }
    }
}
