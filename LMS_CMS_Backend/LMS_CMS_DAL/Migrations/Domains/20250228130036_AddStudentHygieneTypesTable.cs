using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class AddStudentHygieneTypesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StudentHygieneTypesId",
                table: "HygieneTypes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HygieneForms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolId = table.Column<long>(type: "bigint", nullable: false),
                    ClassRoomID = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsertedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    InsertedByOctaId = table.Column<long>(type: "bigint", nullable: true),
                    InsertedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByOctaId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletedByOctaId = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HygieneForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HygieneForms_Classroom_ClassRoomID",
                        column: x => x.ClassRoomID,
                        principalTable: "Classroom",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HygieneForms_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_HygieneForms_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_HygieneForms_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_HygieneForms_School_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "School",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentHygieneTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<long>(type: "bigint", nullable: false),
                    Attendance = table.Column<bool>(type: "bit", nullable: false),
                    SelectAll = table.Column<bool>(type: "bit", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionTaken = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentHygieneTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentHygieneTypes_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HygieneTypes_StudentHygieneTypesId",
                table: "HygieneTypes",
                column: "StudentHygieneTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_HygieneForms_ClassRoomID",
                table: "HygieneForms",
                column: "ClassRoomID");

            migrationBuilder.CreateIndex(
                name: "IX_HygieneForms_DeletedByUserId",
                table: "HygieneForms",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HygieneForms_InsertedByUserId",
                table: "HygieneForms",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HygieneForms_SchoolId",
                table: "HygieneForms",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_HygieneForms_UpdatedByUserId",
                table: "HygieneForms",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentHygieneTypes_StudentId",
                table: "StudentHygieneTypes",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_HygieneTypes_StudentHygieneTypes_StudentHygieneTypesId",
                table: "HygieneTypes",
                column: "StudentHygieneTypesId",
                principalTable: "StudentHygieneTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HygieneTypes_StudentHygieneTypes_StudentHygieneTypesId",
                table: "HygieneTypes");

            migrationBuilder.DropTable(
                name: "HygieneForms");

            migrationBuilder.DropTable(
                name: "StudentHygieneTypes");

            migrationBuilder.DropIndex(
                name: "IX_HygieneTypes_StudentHygieneTypesId",
                table: "HygieneTypes");

            migrationBuilder.DropColumn(
                name: "StudentHygieneTypesId",
                table: "HygieneTypes");
        }
    }
}
