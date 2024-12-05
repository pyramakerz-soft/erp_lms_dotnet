using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class ClassGradeStudentAcademicYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("PK_Class", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Class_Employees_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Class_Employees_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Class_Employees_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("PK_Grade", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Grade_Employees_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Grade_Employees_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Grade_Employees_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "StudentAcademicYear",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<long>(type: "bigint", nullable: false),
                    SchoolID = table.Column<long>(type: "bigint", nullable: false),
                    ClassID = table.Column<long>(type: "bigint", nullable: false),
                    GradeID = table.Column<long>(type: "bigint", nullable: false),
                    SemesterID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_StudentAcademicYear", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StudentAcademicYear_Class_ClassID",
                        column: x => x.ClassID,
                        principalTable: "Class",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentAcademicYear_Employees_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_StudentAcademicYear_Employees_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_StudentAcademicYear_Employees_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_StudentAcademicYear_Grade_GradeID",
                        column: x => x.GradeID,
                        principalTable: "Grade",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentAcademicYear_Schools_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "Schools",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentAcademicYear_Semester_SemesterID",
                        column: x => x.SemesterID,
                        principalTable: "Semester",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentAcademicYear_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Class_DeletedByUserId",
                table: "Class",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Class_InsertedByUserId",
                table: "Class",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Class_Name",
                table: "Class",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Class_UpdatedByUserId",
                table: "Class",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_DeletedByUserId",
                table: "Grade",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_InsertedByUserId",
                table: "Grade",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_Name",
                table: "Grade",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grade_UpdatedByUserId",
                table: "Grade",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAcademicYear_ClassID",
                table: "StudentAcademicYear",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAcademicYear_DeletedByUserId",
                table: "StudentAcademicYear",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAcademicYear_GradeID",
                table: "StudentAcademicYear",
                column: "GradeID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAcademicYear_InsertedByUserId",
                table: "StudentAcademicYear",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAcademicYear_SchoolID",
                table: "StudentAcademicYear",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAcademicYear_SemesterID",
                table: "StudentAcademicYear",
                column: "SemesterID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAcademicYear_StudentID",
                table: "StudentAcademicYear",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAcademicYear_UpdatedByUserId",
                table: "StudentAcademicYear",
                column: "UpdatedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentAcademicYear");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "Grade");
        }
    }
}
