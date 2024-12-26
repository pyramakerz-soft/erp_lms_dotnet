using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class AuditableForViolationModuleMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Violation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByOctaId",
                table: "Violation",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "Violation",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "Violation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByOctaId",
                table: "Violation",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "Violation",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Violation",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Violation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByOctaId",
                table: "Violation",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "Violation",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "EmployeeTypeViolation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByOctaId",
                table: "EmployeeTypeViolation",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "EmployeeTypeViolation",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "EmployeeTypeViolation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByOctaId",
                table: "EmployeeTypeViolation",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "EmployeeTypeViolation",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeTypeViolation",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "EmployeeTypeViolation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByOctaId",
                table: "EmployeeTypeViolation",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "EmployeeTypeViolation",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Violation_DeletedByUserId",
                table: "Violation",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Violation_InsertedByUserId",
                table: "Violation",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Violation_UpdatedByUserId",
                table: "Violation",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTypeViolation_DeletedByUserId",
                table: "EmployeeTypeViolation",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTypeViolation_InsertedByUserId",
                table: "EmployeeTypeViolation",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTypeViolation_UpdatedByUserId",
                table: "EmployeeTypeViolation",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTypeViolation_Employee_DeletedByUserId",
                table: "EmployeeTypeViolation",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTypeViolation_Employee_InsertedByUserId",
                table: "EmployeeTypeViolation",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTypeViolation_Employee_UpdatedByUserId",
                table: "EmployeeTypeViolation",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Violation_Employee_DeletedByUserId",
                table: "Violation",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Violation_Employee_InsertedByUserId",
                table: "Violation",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Violation_Employee_UpdatedByUserId",
                table: "Violation",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTypeViolation_Employee_DeletedByUserId",
                table: "EmployeeTypeViolation");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTypeViolation_Employee_InsertedByUserId",
                table: "EmployeeTypeViolation");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTypeViolation_Employee_UpdatedByUserId",
                table: "EmployeeTypeViolation");

            migrationBuilder.DropForeignKey(
                name: "FK_Violation_Employee_DeletedByUserId",
                table: "Violation");

            migrationBuilder.DropForeignKey(
                name: "FK_Violation_Employee_InsertedByUserId",
                table: "Violation");

            migrationBuilder.DropForeignKey(
                name: "FK_Violation_Employee_UpdatedByUserId",
                table: "Violation");

            migrationBuilder.DropIndex(
                name: "IX_Violation_DeletedByUserId",
                table: "Violation");

            migrationBuilder.DropIndex(
                name: "IX_Violation_InsertedByUserId",
                table: "Violation");

            migrationBuilder.DropIndex(
                name: "IX_Violation_UpdatedByUserId",
                table: "Violation");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeTypeViolation_DeletedByUserId",
                table: "EmployeeTypeViolation");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeTypeViolation_InsertedByUserId",
                table: "EmployeeTypeViolation");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeTypeViolation_UpdatedByUserId",
                table: "EmployeeTypeViolation");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Violation");

            migrationBuilder.DropColumn(
                name: "DeletedByOctaId",
                table: "Violation");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Violation");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "Violation");

            migrationBuilder.DropColumn(
                name: "InsertedByOctaId",
                table: "Violation");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "Violation");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Violation");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Violation");

            migrationBuilder.DropColumn(
                name: "UpdatedByOctaId",
                table: "Violation");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Violation");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "EmployeeTypeViolation");

            migrationBuilder.DropColumn(
                name: "DeletedByOctaId",
                table: "EmployeeTypeViolation");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "EmployeeTypeViolation");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "EmployeeTypeViolation");

            migrationBuilder.DropColumn(
                name: "InsertedByOctaId",
                table: "EmployeeTypeViolation");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "EmployeeTypeViolation");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "EmployeeTypeViolation");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "EmployeeTypeViolation");

            migrationBuilder.DropColumn(
                name: "UpdatedByOctaId",
                table: "EmployeeTypeViolation");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "EmployeeTypeViolation");
        }
    }
}
