using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class addAuditableEntityToEmpAttach : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "EmployeeAttachment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByOctaId",
                table: "EmployeeAttachment",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "EmployeeAttachment",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "EmployeeAttachment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByOctaId",
                table: "EmployeeAttachment",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "EmployeeAttachment",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeAttachment",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "EmployeeAttachment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByOctaId",
                table: "EmployeeAttachment",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "EmployeeAttachment",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttachment_DeletedByUserId",
                table: "EmployeeAttachment",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttachment_InsertedByUserId",
                table: "EmployeeAttachment",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttachment_UpdatedByUserId",
                table: "EmployeeAttachment",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAttachment_Employee_DeletedByUserId",
                table: "EmployeeAttachment",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAttachment_Employee_InsertedByUserId",
                table: "EmployeeAttachment",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAttachment_Employee_UpdatedByUserId",
                table: "EmployeeAttachment",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAttachment_Employee_DeletedByUserId",
                table: "EmployeeAttachment");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAttachment_Employee_InsertedByUserId",
                table: "EmployeeAttachment");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAttachment_Employee_UpdatedByUserId",
                table: "EmployeeAttachment");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeAttachment_DeletedByUserId",
                table: "EmployeeAttachment");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeAttachment_InsertedByUserId",
                table: "EmployeeAttachment");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeAttachment_UpdatedByUserId",
                table: "EmployeeAttachment");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "EmployeeAttachment");

            migrationBuilder.DropColumn(
                name: "DeletedByOctaId",
                table: "EmployeeAttachment");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "EmployeeAttachment");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "EmployeeAttachment");

            migrationBuilder.DropColumn(
                name: "InsertedByOctaId",
                table: "EmployeeAttachment");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "EmployeeAttachment");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "EmployeeAttachment");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "EmployeeAttachment");

            migrationBuilder.DropColumn(
                name: "UpdatedByOctaId",
                table: "EmployeeAttachment");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "EmployeeAttachment");
        }
    }
}
