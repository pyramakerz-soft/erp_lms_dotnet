using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class LMSEvaluationMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EvaluationBookCorrection",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnglishName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArabicName = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_EvaluationBookCorrection", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EvaluationBookCorrection_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EvaluationBookCorrection_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EvaluationBookCorrection_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "EvaluationTemplate",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnglishTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArabicTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    AfterCount = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_EvaluationTemplate", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EvaluationTemplate_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EvaluationTemplate_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EvaluationTemplate_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "EvaluationEmployee",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Period = table.Column<int>(type: "int", nullable: false),
                    Narration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvaluatorID = table.Column<long>(type: "bigint", nullable: false),
                    EvaluatedID = table.Column<long>(type: "bigint", nullable: false),
                    EvaluationTemplateID = table.Column<long>(type: "bigint", nullable: false),
                    ClassroomID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_EvaluationEmployee", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EvaluationEmployee_Classroom_ClassroomID",
                        column: x => x.ClassroomID,
                        principalTable: "Classroom",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluationEmployee_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluationEmployee_Employee_EvaluatedID",
                        column: x => x.EvaluatedID,
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluationEmployee_Employee_EvaluatorID",
                        column: x => x.EvaluatorID,
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluationEmployee_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EvaluationEmployee_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EvaluationEmployee_EvaluationTemplate_EvaluationTemplateID",
                        column: x => x.EvaluationTemplateID,
                        principalTable: "EvaluationTemplate",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationTemplateGroup",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnglishTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArabicTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvaluationTemplateID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_EvaluationTemplateGroup", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EvaluationTemplateGroup_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EvaluationTemplateGroup_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EvaluationTemplateGroup_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EvaluationTemplateGroup_EvaluationTemplate_EvaluationTemplateID",
                        column: x => x.EvaluationTemplateID,
                        principalTable: "EvaluationTemplate",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationEmployeeStudentBookCorrection",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvaluationEmployeeID = table.Column<long>(type: "bigint", nullable: false),
                    EvaluationBookCorrectionID = table.Column<long>(type: "bigint", nullable: false),
                    StudentID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_EvaluationEmployeeStudentBookCorrection", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EvaluationEmployeeStudentBookCorrection_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EvaluationEmployeeStudentBookCorrection_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EvaluationEmployeeStudentBookCorrection_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EvaluationEmployeeStudentBookCorrection_EvaluationBookCorrection_EvaluationBookCorrectionID",
                        column: x => x.EvaluationBookCorrectionID,
                        principalTable: "EvaluationBookCorrection",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluationEmployeeStudentBookCorrection_EvaluationEmployee_EvaluationEmployeeID",
                        column: x => x.EvaluationEmployeeID,
                        principalTable: "EvaluationEmployee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluationEmployeeStudentBookCorrection_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationTemplateGroupQuestion",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnglishTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArabicTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mark = table.Column<int>(type: "int", nullable: false),
                    EvaluationTemplateGroupID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_EvaluationTemplateGroupQuestion", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EvaluationTemplateGroupQuestion_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EvaluationTemplateGroupQuestion_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EvaluationTemplateGroupQuestion_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EvaluationTemplateGroupQuestion_EvaluationTemplateGroup_EvaluationTemplateGroupID",
                        column: x => x.EvaluationTemplateGroupID,
                        principalTable: "EvaluationTemplateGroup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationEmployeeQuestion",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mark = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvaluationEmployeeID = table.Column<long>(type: "bigint", nullable: false),
                    EvaluationTemplateGroupQuestionID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_EvaluationEmployeeQuestion", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EvaluationEmployeeQuestion_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EvaluationEmployeeQuestion_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EvaluationEmployeeQuestion_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EvaluationEmployeeQuestion_EvaluationEmployee_EvaluationEmployeeID",
                        column: x => x.EvaluationEmployeeID,
                        principalTable: "EvaluationEmployee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluationEmployeeQuestion_EvaluationTemplateGroupQuestion_EvaluationTemplateGroupQuestionID",
                        column: x => x.EvaluationTemplateGroupQuestionID,
                        principalTable: "EvaluationTemplateGroupQuestion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationBookCorrection_DeletedByUserId",
                table: "EvaluationBookCorrection",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationBookCorrection_InsertedByUserId",
                table: "EvaluationBookCorrection",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationBookCorrection_UpdatedByUserId",
                table: "EvaluationBookCorrection",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationEmployee_ClassroomID",
                table: "EvaluationEmployee",
                column: "ClassroomID");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationEmployee_DeletedByUserId",
                table: "EvaluationEmployee",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationEmployee_EvaluatedID",
                table: "EvaluationEmployee",
                column: "EvaluatedID");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationEmployee_EvaluationTemplateID",
                table: "EvaluationEmployee",
                column: "EvaluationTemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationEmployee_EvaluatorID",
                table: "EvaluationEmployee",
                column: "EvaluatorID");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationEmployee_InsertedByUserId",
                table: "EvaluationEmployee",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationEmployee_UpdatedByUserId",
                table: "EvaluationEmployee",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationEmployeeQuestion_DeletedByUserId",
                table: "EvaluationEmployeeQuestion",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationEmployeeQuestion_EvaluationEmployeeID",
                table: "EvaluationEmployeeQuestion",
                column: "EvaluationEmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationEmployeeQuestion_EvaluationTemplateGroupQuestionID",
                table: "EvaluationEmployeeQuestion",
                column: "EvaluationTemplateGroupQuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationEmployeeQuestion_InsertedByUserId",
                table: "EvaluationEmployeeQuestion",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationEmployeeQuestion_UpdatedByUserId",
                table: "EvaluationEmployeeQuestion",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationEmployeeStudentBookCorrection_DeletedByUserId",
                table: "EvaluationEmployeeStudentBookCorrection",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationEmployeeStudentBookCorrection_EvaluationBookCorrectionID",
                table: "EvaluationEmployeeStudentBookCorrection",
                column: "EvaluationBookCorrectionID");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationEmployeeStudentBookCorrection_EvaluationEmployeeID",
                table: "EvaluationEmployeeStudentBookCorrection",
                column: "EvaluationEmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationEmployeeStudentBookCorrection_InsertedByUserId",
                table: "EvaluationEmployeeStudentBookCorrection",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationEmployeeStudentBookCorrection_StudentID",
                table: "EvaluationEmployeeStudentBookCorrection",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationEmployeeStudentBookCorrection_UpdatedByUserId",
                table: "EvaluationEmployeeStudentBookCorrection",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationTemplate_DeletedByUserId",
                table: "EvaluationTemplate",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationTemplate_InsertedByUserId",
                table: "EvaluationTemplate",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationTemplate_UpdatedByUserId",
                table: "EvaluationTemplate",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationTemplateGroup_DeletedByUserId",
                table: "EvaluationTemplateGroup",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationTemplateGroup_EvaluationTemplateID",
                table: "EvaluationTemplateGroup",
                column: "EvaluationTemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationTemplateGroup_InsertedByUserId",
                table: "EvaluationTemplateGroup",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationTemplateGroup_UpdatedByUserId",
                table: "EvaluationTemplateGroup",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationTemplateGroupQuestion_DeletedByUserId",
                table: "EvaluationTemplateGroupQuestion",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationTemplateGroupQuestion_EvaluationTemplateGroupID",
                table: "EvaluationTemplateGroupQuestion",
                column: "EvaluationTemplateGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationTemplateGroupQuestion_InsertedByUserId",
                table: "EvaluationTemplateGroupQuestion",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationTemplateGroupQuestion_UpdatedByUserId",
                table: "EvaluationTemplateGroupQuestion",
                column: "UpdatedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluationEmployeeQuestion");

            migrationBuilder.DropTable(
                name: "EvaluationEmployeeStudentBookCorrection");

            migrationBuilder.DropTable(
                name: "EvaluationTemplateGroupQuestion");

            migrationBuilder.DropTable(
                name: "EvaluationBookCorrection");

            migrationBuilder.DropTable(
                name: "EvaluationEmployee");

            migrationBuilder.DropTable(
                name: "EvaluationTemplateGroup");

            migrationBuilder.DropTable(
                name: "EvaluationTemplate");
        }
    }
}
