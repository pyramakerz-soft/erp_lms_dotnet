using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class StockingDetailsAuditableEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "StockingDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByOctaId",
                table: "StockingDetails",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "StockingDetails",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "StockingDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByOctaId",
                table: "StockingDetails",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "StockingDetails",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "StockingDetails",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "StockingDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByOctaId",
                table: "StockingDetails",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "StockingDetails",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockingDetails_DeletedByUserId",
                table: "StockingDetails",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StockingDetails_InsertedByUserId",
                table: "StockingDetails",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StockingDetails_UpdatedByUserId",
                table: "StockingDetails",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockingDetails_Employee_DeletedByUserId",
                table: "StockingDetails",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_StockingDetails_Employee_InsertedByUserId",
                table: "StockingDetails",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_StockingDetails_Employee_UpdatedByUserId",
                table: "StockingDetails",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockingDetails_Employee_DeletedByUserId",
                table: "StockingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StockingDetails_Employee_InsertedByUserId",
                table: "StockingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StockingDetails_Employee_UpdatedByUserId",
                table: "StockingDetails");

            migrationBuilder.DropIndex(
                name: "IX_StockingDetails_DeletedByUserId",
                table: "StockingDetails");

            migrationBuilder.DropIndex(
                name: "IX_StockingDetails_InsertedByUserId",
                table: "StockingDetails");

            migrationBuilder.DropIndex(
                name: "IX_StockingDetails_UpdatedByUserId",
                table: "StockingDetails");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "StockingDetails");

            migrationBuilder.DropColumn(
                name: "DeletedByOctaId",
                table: "StockingDetails");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "StockingDetails");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "StockingDetails");

            migrationBuilder.DropColumn(
                name: "InsertedByOctaId",
                table: "StockingDetails");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "StockingDetails");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "StockingDetails");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "StockingDetails");

            migrationBuilder.DropColumn(
                name: "UpdatedByOctaId",
                table: "StockingDetails");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "StockingDetails");
        }
    }
}
