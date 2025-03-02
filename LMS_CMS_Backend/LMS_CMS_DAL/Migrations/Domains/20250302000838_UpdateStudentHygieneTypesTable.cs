using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class UpdateStudentHygieneTypesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentHygieneTypes_HygieneForms_HygieneFormId",
                table: "StudentHygieneTypes");

            migrationBuilder.DropColumn(
                name: "ActionTaken",
                table: "StudentHygieneTypes");

            migrationBuilder.DropColumn(
                name: "Attendance",
                table: "StudentHygieneTypes");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "StudentHygieneTypes");

            migrationBuilder.DropColumn(
                name: "SelectAll",
                table: "StudentHygieneTypes");

            migrationBuilder.AlterColumn<long>(
                name: "HygieneFormId",
                table: "StudentHygieneTypes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "StudentHygieneTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByOctaId",
                table: "StudentHygieneTypes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "StudentHygieneTypes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "StudentHygieneTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByOctaId",
                table: "StudentHygieneTypes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "StudentHygieneTypes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "StudentHygieneTypes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "StudentHygieneTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByOctaId",
                table: "StudentHygieneTypes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "StudentHygieneTypes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentHygieneTypes_DeletedByUserId",
                table: "StudentHygieneTypes",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentHygieneTypes_InsertedByUserId",
                table: "StudentHygieneTypes",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentHygieneTypes_UpdatedByUserId",
                table: "StudentHygieneTypes",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentHygieneTypes_Employee_DeletedByUserId",
                table: "StudentHygieneTypes",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentHygieneTypes_Employee_InsertedByUserId",
                table: "StudentHygieneTypes",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentHygieneTypes_Employee_UpdatedByUserId",
                table: "StudentHygieneTypes",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentHygieneTypes_HygieneForms_HygieneFormId",
                table: "StudentHygieneTypes",
                column: "HygieneFormId",
                principalTable: "HygieneForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentHygieneTypes_Employee_DeletedByUserId",
                table: "StudentHygieneTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentHygieneTypes_Employee_InsertedByUserId",
                table: "StudentHygieneTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentHygieneTypes_Employee_UpdatedByUserId",
                table: "StudentHygieneTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentHygieneTypes_HygieneForms_HygieneFormId",
                table: "StudentHygieneTypes");

            migrationBuilder.DropIndex(
                name: "IX_StudentHygieneTypes_DeletedByUserId",
                table: "StudentHygieneTypes");

            migrationBuilder.DropIndex(
                name: "IX_StudentHygieneTypes_InsertedByUserId",
                table: "StudentHygieneTypes");

            migrationBuilder.DropIndex(
                name: "IX_StudentHygieneTypes_UpdatedByUserId",
                table: "StudentHygieneTypes");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "StudentHygieneTypes");

            migrationBuilder.DropColumn(
                name: "DeletedByOctaId",
                table: "StudentHygieneTypes");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "StudentHygieneTypes");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "StudentHygieneTypes");

            migrationBuilder.DropColumn(
                name: "InsertedByOctaId",
                table: "StudentHygieneTypes");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "StudentHygieneTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "StudentHygieneTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "StudentHygieneTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedByOctaId",
                table: "StudentHygieneTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "StudentHygieneTypes");

            migrationBuilder.AlterColumn<long>(
                name: "HygieneFormId",
                table: "StudentHygieneTypes",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "ActionTaken",
                table: "StudentHygieneTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Attendance",
                table: "StudentHygieneTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "StudentHygieneTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SelectAll",
                table: "StudentHygieneTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentHygieneTypes_HygieneForms_HygieneFormId",
                table: "StudentHygieneTypes",
                column: "HygieneFormId",
                principalTable: "HygieneForms",
                principalColumn: "Id");
        }
    }
}
