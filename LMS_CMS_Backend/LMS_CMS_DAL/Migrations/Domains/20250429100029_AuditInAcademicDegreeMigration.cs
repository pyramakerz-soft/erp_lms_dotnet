using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class AuditInAcademicDegreeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "AcademicDegrees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByOctaId",
                table: "AcademicDegrees",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "AcademicDegrees",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "AcademicDegrees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByOctaId",
                table: "AcademicDegrees",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "AcademicDegrees",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AcademicDegrees",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "AcademicDegrees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByOctaId",
                table: "AcademicDegrees",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "AcademicDegrees",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AcademicDegrees_DeletedByUserId",
                table: "AcademicDegrees",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicDegrees_InsertedByUserId",
                table: "AcademicDegrees",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicDegrees_UpdatedByUserId",
                table: "AcademicDegrees",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicDegrees_Employee_DeletedByUserId",
                table: "AcademicDegrees",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicDegrees_Employee_InsertedByUserId",
                table: "AcademicDegrees",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicDegrees_Employee_UpdatedByUserId",
                table: "AcademicDegrees",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicDegrees_Employee_DeletedByUserId",
                table: "AcademicDegrees");

            migrationBuilder.DropForeignKey(
                name: "FK_AcademicDegrees_Employee_InsertedByUserId",
                table: "AcademicDegrees");

            migrationBuilder.DropForeignKey(
                name: "FK_AcademicDegrees_Employee_UpdatedByUserId",
                table: "AcademicDegrees");

            migrationBuilder.DropIndex(
                name: "IX_AcademicDegrees_DeletedByUserId",
                table: "AcademicDegrees");

            migrationBuilder.DropIndex(
                name: "IX_AcademicDegrees_InsertedByUserId",
                table: "AcademicDegrees");

            migrationBuilder.DropIndex(
                name: "IX_AcademicDegrees_UpdatedByUserId",
                table: "AcademicDegrees");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "AcademicDegrees");

            migrationBuilder.DropColumn(
                name: "DeletedByOctaId",
                table: "AcademicDegrees");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "AcademicDegrees");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "AcademicDegrees");

            migrationBuilder.DropColumn(
                name: "InsertedByOctaId",
                table: "AcademicDegrees");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "AcademicDegrees");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AcademicDegrees");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "AcademicDegrees");

            migrationBuilder.DropColumn(
                name: "UpdatedByOctaId",
                table: "AcademicDegrees");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "AcademicDegrees");
        }
    }
}
