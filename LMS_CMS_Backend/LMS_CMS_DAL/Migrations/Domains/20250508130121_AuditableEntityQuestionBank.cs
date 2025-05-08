using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class AuditableEntityQuestionBank : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Video",
                table: "QuestionBank");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "SubBankQuestion",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByOctaId",
                table: "SubBankQuestion",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "SubBankQuestion",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "SubBankQuestion",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByOctaId",
                table: "SubBankQuestion",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "SubBankQuestion",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SubBankQuestion",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "SubBankQuestion",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByOctaId",
                table: "SubBankQuestion",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "SubBankQuestion",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "QuestionBankTags",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByOctaId",
                table: "QuestionBankTags",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "QuestionBankTags",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "QuestionBankTags",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByOctaId",
                table: "QuestionBankTags",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "QuestionBankTags",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "QuestionBankTags",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "QuestionBankTags",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByOctaId",
                table: "QuestionBankTags",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "QuestionBankTags",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "QuestionBankOption",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByOctaId",
                table: "QuestionBankOption",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "QuestionBankOption",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "QuestionBankOption",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByOctaId",
                table: "QuestionBankOption",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "QuestionBankOption",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "QuestionBankOption",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "QuestionBankOption",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByOctaId",
                table: "QuestionBankOption",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "QuestionBankOption",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "DragAndDropAnswer",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByOctaId",
                table: "DragAndDropAnswer",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "DragAndDropAnswer",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "DragAndDropAnswer",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByOctaId",
                table: "DragAndDropAnswer",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "DragAndDropAnswer",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "DragAndDropAnswer",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "DragAndDropAnswer",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByOctaId",
                table: "DragAndDropAnswer",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "DragAndDropAnswer",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubBankQuestion_DeletedByUserId",
                table: "SubBankQuestion",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubBankQuestion_InsertedByUserId",
                table: "SubBankQuestion",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubBankQuestion_UpdatedByUserId",
                table: "SubBankQuestion",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBankTags_DeletedByUserId",
                table: "QuestionBankTags",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBankTags_InsertedByUserId",
                table: "QuestionBankTags",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBankTags_UpdatedByUserId",
                table: "QuestionBankTags",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBankOption_DeletedByUserId",
                table: "QuestionBankOption",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBankOption_InsertedByUserId",
                table: "QuestionBankOption",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBankOption_UpdatedByUserId",
                table: "QuestionBankOption",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DragAndDropAnswer_DeletedByUserId",
                table: "DragAndDropAnswer",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DragAndDropAnswer_InsertedByUserId",
                table: "DragAndDropAnswer",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DragAndDropAnswer_UpdatedByUserId",
                table: "DragAndDropAnswer",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DragAndDropAnswer_Employee_DeletedByUserId",
                table: "DragAndDropAnswer",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_DragAndDropAnswer_Employee_InsertedByUserId",
                table: "DragAndDropAnswer",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_DragAndDropAnswer_Employee_UpdatedByUserId",
                table: "DragAndDropAnswer",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionBankOption_Employee_DeletedByUserId",
                table: "QuestionBankOption",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionBankOption_Employee_InsertedByUserId",
                table: "QuestionBankOption",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionBankOption_Employee_UpdatedByUserId",
                table: "QuestionBankOption",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionBankTags_Employee_DeletedByUserId",
                table: "QuestionBankTags",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionBankTags_Employee_InsertedByUserId",
                table: "QuestionBankTags",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionBankTags_Employee_UpdatedByUserId",
                table: "QuestionBankTags",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_SubBankQuestion_Employee_DeletedByUserId",
                table: "SubBankQuestion",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_SubBankQuestion_Employee_InsertedByUserId",
                table: "SubBankQuestion",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_SubBankQuestion_Employee_UpdatedByUserId",
                table: "SubBankQuestion",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DragAndDropAnswer_Employee_DeletedByUserId",
                table: "DragAndDropAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_DragAndDropAnswer_Employee_InsertedByUserId",
                table: "DragAndDropAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_DragAndDropAnswer_Employee_UpdatedByUserId",
                table: "DragAndDropAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionBankOption_Employee_DeletedByUserId",
                table: "QuestionBankOption");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionBankOption_Employee_InsertedByUserId",
                table: "QuestionBankOption");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionBankOption_Employee_UpdatedByUserId",
                table: "QuestionBankOption");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionBankTags_Employee_DeletedByUserId",
                table: "QuestionBankTags");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionBankTags_Employee_InsertedByUserId",
                table: "QuestionBankTags");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionBankTags_Employee_UpdatedByUserId",
                table: "QuestionBankTags");

            migrationBuilder.DropForeignKey(
                name: "FK_SubBankQuestion_Employee_DeletedByUserId",
                table: "SubBankQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_SubBankQuestion_Employee_InsertedByUserId",
                table: "SubBankQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_SubBankQuestion_Employee_UpdatedByUserId",
                table: "SubBankQuestion");

            migrationBuilder.DropIndex(
                name: "IX_SubBankQuestion_DeletedByUserId",
                table: "SubBankQuestion");

            migrationBuilder.DropIndex(
                name: "IX_SubBankQuestion_InsertedByUserId",
                table: "SubBankQuestion");

            migrationBuilder.DropIndex(
                name: "IX_SubBankQuestion_UpdatedByUserId",
                table: "SubBankQuestion");

            migrationBuilder.DropIndex(
                name: "IX_QuestionBankTags_DeletedByUserId",
                table: "QuestionBankTags");

            migrationBuilder.DropIndex(
                name: "IX_QuestionBankTags_InsertedByUserId",
                table: "QuestionBankTags");

            migrationBuilder.DropIndex(
                name: "IX_QuestionBankTags_UpdatedByUserId",
                table: "QuestionBankTags");

            migrationBuilder.DropIndex(
                name: "IX_QuestionBankOption_DeletedByUserId",
                table: "QuestionBankOption");

            migrationBuilder.DropIndex(
                name: "IX_QuestionBankOption_InsertedByUserId",
                table: "QuestionBankOption");

            migrationBuilder.DropIndex(
                name: "IX_QuestionBankOption_UpdatedByUserId",
                table: "QuestionBankOption");

            migrationBuilder.DropIndex(
                name: "IX_DragAndDropAnswer_DeletedByUserId",
                table: "DragAndDropAnswer");

            migrationBuilder.DropIndex(
                name: "IX_DragAndDropAnswer_InsertedByUserId",
                table: "DragAndDropAnswer");

            migrationBuilder.DropIndex(
                name: "IX_DragAndDropAnswer_UpdatedByUserId",
                table: "DragAndDropAnswer");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "SubBankQuestion");

            migrationBuilder.DropColumn(
                name: "DeletedByOctaId",
                table: "SubBankQuestion");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "SubBankQuestion");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "SubBankQuestion");

            migrationBuilder.DropColumn(
                name: "InsertedByOctaId",
                table: "SubBankQuestion");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "SubBankQuestion");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SubBankQuestion");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "SubBankQuestion");

            migrationBuilder.DropColumn(
                name: "UpdatedByOctaId",
                table: "SubBankQuestion");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "SubBankQuestion");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "QuestionBankTags");

            migrationBuilder.DropColumn(
                name: "DeletedByOctaId",
                table: "QuestionBankTags");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "QuestionBankTags");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "QuestionBankTags");

            migrationBuilder.DropColumn(
                name: "InsertedByOctaId",
                table: "QuestionBankTags");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "QuestionBankTags");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "QuestionBankTags");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "QuestionBankTags");

            migrationBuilder.DropColumn(
                name: "UpdatedByOctaId",
                table: "QuestionBankTags");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "QuestionBankTags");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "QuestionBankOption");

            migrationBuilder.DropColumn(
                name: "DeletedByOctaId",
                table: "QuestionBankOption");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "QuestionBankOption");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "QuestionBankOption");

            migrationBuilder.DropColumn(
                name: "InsertedByOctaId",
                table: "QuestionBankOption");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "QuestionBankOption");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "QuestionBankOption");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "QuestionBankOption");

            migrationBuilder.DropColumn(
                name: "UpdatedByOctaId",
                table: "QuestionBankOption");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "QuestionBankOption");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "DragAndDropAnswer");

            migrationBuilder.DropColumn(
                name: "DeletedByOctaId",
                table: "DragAndDropAnswer");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "DragAndDropAnswer");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "DragAndDropAnswer");

            migrationBuilder.DropColumn(
                name: "InsertedByOctaId",
                table: "DragAndDropAnswer");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "DragAndDropAnswer");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "DragAndDropAnswer");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "DragAndDropAnswer");

            migrationBuilder.DropColumn(
                name: "UpdatedByOctaId",
                table: "DragAndDropAnswer");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "DragAndDropAnswer");

            migrationBuilder.AddColumn<string>(
                name: "Video",
                table: "QuestionBank",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
