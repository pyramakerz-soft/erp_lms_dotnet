using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class AddAuditableToSchoolTypeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "SchoolType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "SchoolType",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "SchoolType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "SchoolType",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SchoolType",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "SchoolType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "SchoolType",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SchoolType_DeletedByUserId",
                table: "SchoolType",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolType_InsertedByUserId",
                table: "SchoolType",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolType_UpdatedByUserId",
                table: "SchoolType",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolType_Octa_DeletedByUserId",
                table: "SchoolType",
                column: "DeletedByUserId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolType_Octa_InsertedByUserId",
                table: "SchoolType",
                column: "InsertedByUserId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolType_Octa_UpdatedByUserId",
                table: "SchoolType",
                column: "UpdatedByUserId",
                principalTable: "Octa",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolType_Octa_DeletedByUserId",
                table: "SchoolType");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolType_Octa_InsertedByUserId",
                table: "SchoolType");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolType_Octa_UpdatedByUserId",
                table: "SchoolType");

            migrationBuilder.DropIndex(
                name: "IX_SchoolType_DeletedByUserId",
                table: "SchoolType");

            migrationBuilder.DropIndex(
                name: "IX_SchoolType_InsertedByUserId",
                table: "SchoolType");

            migrationBuilder.DropIndex(
                name: "IX_SchoolType_UpdatedByUserId",
                table: "SchoolType");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "SchoolType");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "SchoolType");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "SchoolType");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "SchoolType");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SchoolType");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "SchoolType");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "SchoolType");
        }
    }
}
