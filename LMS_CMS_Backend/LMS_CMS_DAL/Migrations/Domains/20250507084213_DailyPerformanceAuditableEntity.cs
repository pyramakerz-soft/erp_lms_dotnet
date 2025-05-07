using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class DailyPerformanceAuditableEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "DailyPerformance",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByOctaId",
                table: "DailyPerformance",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "DailyPerformance",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "DailyPerformance",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByOctaId",
                table: "DailyPerformance",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "DailyPerformance",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "DailyPerformance",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "DailyPerformance",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByOctaId",
                table: "DailyPerformance",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "DailyPerformance",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DailyPerformance_DeletedByUserId",
                table: "DailyPerformance",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyPerformance_InsertedByUserId",
                table: "DailyPerformance",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyPerformance_UpdatedByUserId",
                table: "DailyPerformance",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyPerformance_Employee_DeletedByUserId",
                table: "DailyPerformance",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyPerformance_Employee_InsertedByUserId",
                table: "DailyPerformance",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyPerformance_Employee_UpdatedByUserId",
                table: "DailyPerformance",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyPerformance_Employee_DeletedByUserId",
                table: "DailyPerformance");

            migrationBuilder.DropForeignKey(
                name: "FK_DailyPerformance_Employee_InsertedByUserId",
                table: "DailyPerformance");

            migrationBuilder.DropForeignKey(
                name: "FK_DailyPerformance_Employee_UpdatedByUserId",
                table: "DailyPerformance");

            migrationBuilder.DropIndex(
                name: "IX_DailyPerformance_DeletedByUserId",
                table: "DailyPerformance");

            migrationBuilder.DropIndex(
                name: "IX_DailyPerformance_InsertedByUserId",
                table: "DailyPerformance");

            migrationBuilder.DropIndex(
                name: "IX_DailyPerformance_UpdatedByUserId",
                table: "DailyPerformance");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "DailyPerformance");

            migrationBuilder.DropColumn(
                name: "DeletedByOctaId",
                table: "DailyPerformance");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "DailyPerformance");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "DailyPerformance");

            migrationBuilder.DropColumn(
                name: "InsertedByOctaId",
                table: "DailyPerformance");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "DailyPerformance");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "DailyPerformance");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "DailyPerformance");

            migrationBuilder.DropColumn(
                name: "UpdatedByOctaId",
                table: "DailyPerformance");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "DailyPerformance");
        }
    }
}
