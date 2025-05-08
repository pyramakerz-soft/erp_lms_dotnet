using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class QuestionBankType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionBank_QuestionType_QuestionTypeID",
                table: "QuestionBank");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "QuestionBank",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByOctaId",
                table: "QuestionBank",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "QuestionBank",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "QuestionBank",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByOctaId",
                table: "QuestionBank",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "QuestionBank",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "QuestionBank",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "QuestionTypeID1",
                table: "QuestionBank",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "QuestionBank",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByOctaId",
                table: "QuestionBank",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "QuestionBank",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QuestionBankType",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionBankType", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBank_DeletedByUserId",
                table: "QuestionBank",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBank_InsertedByUserId",
                table: "QuestionBank",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBank_QuestionTypeID1",
                table: "QuestionBank",
                column: "QuestionTypeID1");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBank_UpdatedByUserId",
                table: "QuestionBank",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionBank_Employee_DeletedByUserId",
                table: "QuestionBank",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionBank_Employee_InsertedByUserId",
                table: "QuestionBank",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionBank_Employee_UpdatedByUserId",
                table: "QuestionBank",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionBank_QuestionBankType_QuestionTypeID",
                table: "QuestionBank",
                column: "QuestionTypeID",
                principalTable: "QuestionBankType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionBank_QuestionType_QuestionTypeID1",
                table: "QuestionBank",
                column: "QuestionTypeID1",
                principalTable: "QuestionType",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionBank_Employee_DeletedByUserId",
                table: "QuestionBank");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionBank_Employee_InsertedByUserId",
                table: "QuestionBank");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionBank_Employee_UpdatedByUserId",
                table: "QuestionBank");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionBank_QuestionBankType_QuestionTypeID",
                table: "QuestionBank");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionBank_QuestionType_QuestionTypeID1",
                table: "QuestionBank");

            migrationBuilder.DropTable(
                name: "QuestionBankType");

            migrationBuilder.DropIndex(
                name: "IX_QuestionBank_DeletedByUserId",
                table: "QuestionBank");

            migrationBuilder.DropIndex(
                name: "IX_QuestionBank_InsertedByUserId",
                table: "QuestionBank");

            migrationBuilder.DropIndex(
                name: "IX_QuestionBank_QuestionTypeID1",
                table: "QuestionBank");

            migrationBuilder.DropIndex(
                name: "IX_QuestionBank_UpdatedByUserId",
                table: "QuestionBank");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "QuestionBank");

            migrationBuilder.DropColumn(
                name: "DeletedByOctaId",
                table: "QuestionBank");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "QuestionBank");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "QuestionBank");

            migrationBuilder.DropColumn(
                name: "InsertedByOctaId",
                table: "QuestionBank");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "QuestionBank");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "QuestionBank");

            migrationBuilder.DropColumn(
                name: "QuestionTypeID1",
                table: "QuestionBank");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "QuestionBank");

            migrationBuilder.DropColumn(
                name: "UpdatedByOctaId",
                table: "QuestionBank");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "QuestionBank");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionBank_QuestionType_QuestionTypeID",
                table: "QuestionBank",
                column: "QuestionTypeID",
                principalTable: "QuestionType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
