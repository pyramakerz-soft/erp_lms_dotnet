using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class AddAuditableEntityMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Employee_DeletedByUserId",
                table: "Bus");

            migrationBuilder.DropTable(
                name: "EmployeeTypeViolation");

            migrationBuilder.DropTable(
                name: "Violations");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "SubjectCategory",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByOctaId",
                table: "SubjectCategory",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "SubjectCategory",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "SubjectCategory",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByOctaId",
                table: "SubjectCategory",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "SubjectCategory",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SubjectCategory",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "SubjectCategory",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByOctaId",
                table: "SubjectCategory",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "SubjectCategory",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Subject",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByOctaId",
                table: "Subject",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "Subject",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "Subject",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByOctaId",
                table: "Subject",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "Subject",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Subject",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Subject",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByOctaId",
                table: "Subject",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "Subject",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Section",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByOctaId",
                table: "Section",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "Section",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "Section",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByOctaId",
                table: "Section",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "Section",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Section",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Section",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByOctaId",
                table: "Section",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "Section",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "SchoolType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByOctaId",
                table: "SchoolType",
                type: "bigint",
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
                name: "InsertedByOctaId",
                table: "SchoolType",
                type: "bigint",
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
                name: "UpdatedByOctaId",
                table: "SchoolType",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "SchoolType",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Floor",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByOctaId",
                table: "Floor",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "Floor",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "Floor",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByOctaId",
                table: "Floor",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "Floor",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Floor",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Floor",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByOctaId",
                table: "Floor",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "Floor",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Classroom",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByOctaId",
                table: "Classroom",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "Classroom",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "Classroom",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByOctaId",
                table: "Classroom",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "Classroom",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Classroom",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Classroom",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByOctaId",
                table: "Classroom",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "Classroom",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Building",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByOctaId",
                table: "Building",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "Building",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "Building",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByOctaId",
                table: "Building",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "Building",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Building",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Building",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByOctaId",
                table: "Building",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "Building",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubjectCategory_DeletedByUserId",
                table: "SubjectCategory",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectCategory_InsertedByUserId",
                table: "SubjectCategory",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectCategory_UpdatedByUserId",
                table: "SubjectCategory",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_DeletedByUserId",
                table: "Subject",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_InsertedByUserId",
                table: "Subject",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_UpdatedByUserId",
                table: "Subject",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Section_DeletedByUserId",
                table: "Section",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Section_InsertedByUserId",
                table: "Section",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Section_UpdatedByUserId",
                table: "Section",
                column: "UpdatedByUserId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Floor_DeletedByUserId",
                table: "Floor",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Floor_InsertedByUserId",
                table: "Floor",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Floor_UpdatedByUserId",
                table: "Floor",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Classroom_DeletedByUserId",
                table: "Classroom",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Classroom_InsertedByUserId",
                table: "Classroom",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Classroom_UpdatedByUserId",
                table: "Classroom",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Building_DeletedByUserId",
                table: "Building",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Building_InsertedByUserId",
                table: "Building",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Building_UpdatedByUserId",
                table: "Building",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Building_Employee_DeletedByUserId",
                table: "Building",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Building_Employee_InsertedByUserId",
                table: "Building",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Building_Employee_UpdatedByUserId",
                table: "Building",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Employee_DeletedByUserId",
                table: "Bus",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Classroom_Employee_DeletedByUserId",
                table: "Classroom",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Classroom_Employee_InsertedByUserId",
                table: "Classroom",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Classroom_Employee_UpdatedByUserId",
                table: "Classroom",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Floor_Employee_DeletedByUserId",
                table: "Floor",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Floor_Employee_InsertedByUserId",
                table: "Floor",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Floor_Employee_UpdatedByUserId",
                table: "Floor",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolType_Employee_DeletedByUserId",
                table: "SchoolType",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolType_Employee_InsertedByUserId",
                table: "SchoolType",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolType_Employee_UpdatedByUserId",
                table: "SchoolType",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Section_Employee_DeletedByUserId",
                table: "Section",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Section_Employee_InsertedByUserId",
                table: "Section",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Section_Employee_UpdatedByUserId",
                table: "Section",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Employee_DeletedByUserId",
                table: "Subject",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Employee_InsertedByUserId",
                table: "Subject",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Employee_UpdatedByUserId",
                table: "Subject",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectCategory_Employee_DeletedByUserId",
                table: "SubjectCategory",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectCategory_Employee_InsertedByUserId",
                table: "SubjectCategory",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectCategory_Employee_UpdatedByUserId",
                table: "SubjectCategory",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Building_Employee_DeletedByUserId",
                table: "Building");

            migrationBuilder.DropForeignKey(
                name: "FK_Building_Employee_InsertedByUserId",
                table: "Building");

            migrationBuilder.DropForeignKey(
                name: "FK_Building_Employee_UpdatedByUserId",
                table: "Building");

            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Employee_DeletedByUserId",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_Classroom_Employee_DeletedByUserId",
                table: "Classroom");

            migrationBuilder.DropForeignKey(
                name: "FK_Classroom_Employee_InsertedByUserId",
                table: "Classroom");

            migrationBuilder.DropForeignKey(
                name: "FK_Classroom_Employee_UpdatedByUserId",
                table: "Classroom");

            migrationBuilder.DropForeignKey(
                name: "FK_Floor_Employee_DeletedByUserId",
                table: "Floor");

            migrationBuilder.DropForeignKey(
                name: "FK_Floor_Employee_InsertedByUserId",
                table: "Floor");

            migrationBuilder.DropForeignKey(
                name: "FK_Floor_Employee_UpdatedByUserId",
                table: "Floor");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolType_Employee_DeletedByUserId",
                table: "SchoolType");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolType_Employee_InsertedByUserId",
                table: "SchoolType");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolType_Employee_UpdatedByUserId",
                table: "SchoolType");

            migrationBuilder.DropForeignKey(
                name: "FK_Section_Employee_DeletedByUserId",
                table: "Section");

            migrationBuilder.DropForeignKey(
                name: "FK_Section_Employee_InsertedByUserId",
                table: "Section");

            migrationBuilder.DropForeignKey(
                name: "FK_Section_Employee_UpdatedByUserId",
                table: "Section");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Employee_DeletedByUserId",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Employee_InsertedByUserId",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Employee_UpdatedByUserId",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectCategory_Employee_DeletedByUserId",
                table: "SubjectCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectCategory_Employee_InsertedByUserId",
                table: "SubjectCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectCategory_Employee_UpdatedByUserId",
                table: "SubjectCategory");

            migrationBuilder.DropIndex(
                name: "IX_SubjectCategory_DeletedByUserId",
                table: "SubjectCategory");

            migrationBuilder.DropIndex(
                name: "IX_SubjectCategory_InsertedByUserId",
                table: "SubjectCategory");

            migrationBuilder.DropIndex(
                name: "IX_SubjectCategory_UpdatedByUserId",
                table: "SubjectCategory");

            migrationBuilder.DropIndex(
                name: "IX_Subject_DeletedByUserId",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Subject_InsertedByUserId",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Subject_UpdatedByUserId",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Section_DeletedByUserId",
                table: "Section");

            migrationBuilder.DropIndex(
                name: "IX_Section_InsertedByUserId",
                table: "Section");

            migrationBuilder.DropIndex(
                name: "IX_Section_UpdatedByUserId",
                table: "Section");

            migrationBuilder.DropIndex(
                name: "IX_SchoolType_DeletedByUserId",
                table: "SchoolType");

            migrationBuilder.DropIndex(
                name: "IX_SchoolType_InsertedByUserId",
                table: "SchoolType");

            migrationBuilder.DropIndex(
                name: "IX_SchoolType_UpdatedByUserId",
                table: "SchoolType");

            migrationBuilder.DropIndex(
                name: "IX_Floor_DeletedByUserId",
                table: "Floor");

            migrationBuilder.DropIndex(
                name: "IX_Floor_InsertedByUserId",
                table: "Floor");

            migrationBuilder.DropIndex(
                name: "IX_Floor_UpdatedByUserId",
                table: "Floor");

            migrationBuilder.DropIndex(
                name: "IX_Classroom_DeletedByUserId",
                table: "Classroom");

            migrationBuilder.DropIndex(
                name: "IX_Classroom_InsertedByUserId",
                table: "Classroom");

            migrationBuilder.DropIndex(
                name: "IX_Classroom_UpdatedByUserId",
                table: "Classroom");

            migrationBuilder.DropIndex(
                name: "IX_Building_DeletedByUserId",
                table: "Building");

            migrationBuilder.DropIndex(
                name: "IX_Building_InsertedByUserId",
                table: "Building");

            migrationBuilder.DropIndex(
                name: "IX_Building_UpdatedByUserId",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "SubjectCategory");

            migrationBuilder.DropColumn(
                name: "DeletedByOctaId",
                table: "SubjectCategory");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "SubjectCategory");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "SubjectCategory");

            migrationBuilder.DropColumn(
                name: "InsertedByOctaId",
                table: "SubjectCategory");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "SubjectCategory");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SubjectCategory");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "SubjectCategory");

            migrationBuilder.DropColumn(
                name: "UpdatedByOctaId",
                table: "SubjectCategory");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "SubjectCategory");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "DeletedByOctaId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "InsertedByOctaId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "UpdatedByOctaId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "DeletedByOctaId",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "InsertedByOctaId",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "UpdatedByOctaId",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "SchoolType");

            migrationBuilder.DropColumn(
                name: "DeletedByOctaId",
                table: "SchoolType");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "SchoolType");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "SchoolType");

            migrationBuilder.DropColumn(
                name: "InsertedByOctaId",
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
                name: "UpdatedByOctaId",
                table: "SchoolType");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "SchoolType");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Floor");

            migrationBuilder.DropColumn(
                name: "DeletedByOctaId",
                table: "Floor");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Floor");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "Floor");

            migrationBuilder.DropColumn(
                name: "InsertedByOctaId",
                table: "Floor");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "Floor");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Floor");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Floor");

            migrationBuilder.DropColumn(
                name: "UpdatedByOctaId",
                table: "Floor");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Floor");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Classroom");

            migrationBuilder.DropColumn(
                name: "DeletedByOctaId",
                table: "Classroom");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Classroom");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "Classroom");

            migrationBuilder.DropColumn(
                name: "InsertedByOctaId",
                table: "Classroom");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "Classroom");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Classroom");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Classroom");

            migrationBuilder.DropColumn(
                name: "UpdatedByOctaId",
                table: "Classroom");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Classroom");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "DeletedByOctaId",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "InsertedByOctaId",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "UpdatedByOctaId",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Building");

            migrationBuilder.CreateTable(
                name: "Violations",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Violations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTypeViolation",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeTypeID = table.Column<long>(type: "bigint", nullable: true),
                    ViolationID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTypeViolation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EmployeeTypeViolation_EmployeeType_EmployeeTypeID",
                        column: x => x.EmployeeTypeID,
                        principalTable: "EmployeeType",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EmployeeTypeViolation_Violations_ViolationID",
                        column: x => x.ViolationID,
                        principalTable: "Violations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTypeViolation_EmployeeTypeID",
                table: "EmployeeTypeViolation",
                column: "EmployeeTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTypeViolation_ViolationID",
                table: "EmployeeTypeViolation",
                column: "ViolationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Employee_DeletedByUserId",
                table: "Bus",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
