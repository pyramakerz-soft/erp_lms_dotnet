using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class RegisterationTablesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FieldType",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "InterviewState",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewState", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "InterviewTime",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Reserved = table.Column<int>(type: "int", nullable: false),
                    AcademicYearID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_InterviewTime", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InterviewTime_AcademicYear_AcademicYearID",
                        column: x => x.AcademicYearID,
                        principalTable: "AcademicYear",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InterviewTime_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InterviewTime_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InterviewTime_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "QuestionType",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RegisterationFormState",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterationFormState", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationForm",
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
                    table.PrimaryKey("PK_RegistrationForm", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RegistrationForm_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegistrationForm_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegistrationForm_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalMark = table.Column<double>(type: "float", nullable: false),
                    AcademicYearID = table.Column<long>(type: "bigint", nullable: false),
                    SubjectID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Test", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Test_AcademicYear_AcademicYearID",
                        column: x => x.AcademicYearID,
                        principalTable: "AcademicYear",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Test_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Test_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Test_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Test_Subject_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subject",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestState",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestState", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RegisterationFormParent",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GradeID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterationFormStateID = table.Column<long>(type: "bigint", nullable: true),
                    ParentID = table.Column<long>(type: "bigint", nullable: false),
                    RegistrationFormID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_RegisterationFormParent", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RegisterationFormParent_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegisterationFormParent_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegisterationFormParent_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegisterationFormParent_Parent_ParentID",
                        column: x => x.ParentID,
                        principalTable: "Parent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegisterationFormParent_RegisterationFormState_RegisterationFormStateID",
                        column: x => x.RegisterationFormStateID,
                        principalTable: "RegisterationFormState",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegisterationFormParent_RegistrationForm_RegistrationFormID",
                        column: x => x.RegistrationFormID,
                        principalTable: "RegistrationForm",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationCategory",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ArName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OrderInForm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationFormID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_RegistrationCategory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RegistrationCategory_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegistrationCategory_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegistrationCategory_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegistrationCategory_RegistrationForm_RegistrationFormID",
                        column: x => x.RegistrationFormID,
                        principalTable: "RegistrationForm",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegisterationFormInterview",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterviewStateID = table.Column<long>(type: "bigint", nullable: true),
                    RegisterationFormParentID = table.Column<long>(type: "bigint", nullable: false),
                    InterviewTimeID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_RegisterationFormInterview", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RegisterationFormInterview_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegisterationFormInterview_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegisterationFormInterview_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegisterationFormInterview_InterviewState_InterviewStateID",
                        column: x => x.InterviewStateID,
                        principalTable: "InterviewState",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegisterationFormInterview_InterviewTime_InterviewTimeID",
                        column: x => x.InterviewTimeID,
                        principalTable: "InterviewTime",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegisterationFormInterview_RegisterationFormParent_RegisterationFormParentID",
                        column: x => x.RegisterationFormParentID,
                        principalTable: "RegisterationFormParent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegisterationFormTest",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mark = table.Column<double>(type: "float", nullable: false),
                    VisibleToParent = table.Column<bool>(type: "bit", nullable: false),
                    TestID = table.Column<long>(type: "bigint", nullable: false),
                    StateID = table.Column<long>(type: "bigint", nullable: false),
                    RegisterationFormParentID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_RegisterationFormTest", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RegisterationFormTest_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegisterationFormTest_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegisterationFormTest_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegisterationFormTest_RegisterationFormParent_RegisterationFormParentID",
                        column: x => x.RegisterationFormParentID,
                        principalTable: "RegisterationFormParent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegisterationFormTest_TestState_StateID",
                        column: x => x.StateID,
                        principalTable: "TestState",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegisterationFormTest_Test_TestID",
                        column: x => x.TestID,
                        principalTable: "Test",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryField",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ArName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OrderInForm = table.Column<int>(type: "int", nullable: false),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    RegistrationCategoryID = table.Column<long>(type: "bigint", nullable: false),
                    FieldTypeID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_CategoryField", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CategoryField_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_CategoryField_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_CategoryField_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_CategoryField_FieldType_FieldTypeID",
                        column: x => x.FieldTypeID,
                        principalTable: "FieldType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoryField_RegistrationCategory_RegistrationCategoryID",
                        column: x => x.RegistrationCategoryID,
                        principalTable: "RegistrationCategory",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FieldOption",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CategoryFieldID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_FieldOption", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FieldOption_CategoryField_CategoryFieldID",
                        column: x => x.CategoryFieldID,
                        principalTable: "CategoryField",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FieldOption_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FieldOption_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FieldOption_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "RegisterationFormSubmittion",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterationFormParentID = table.Column<long>(type: "bigint", nullable: false),
                    CategoryFieldID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_RegisterationFormSubmittion", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RegisterationFormSubmittion_CategoryField_CategoryFieldID",
                        column: x => x.CategoryFieldID,
                        principalTable: "CategoryField",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegisterationFormSubmittion_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegisterationFormSubmittion_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegisterationFormSubmittion_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegisterationFormSubmittion_RegisterationFormParent_RegisterationFormParentID",
                        column: x => x.RegisterationFormParentID,
                        principalTable: "RegisterationFormParent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MCQQuestionOption",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Question_ID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_MCQQuestionOption", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MCQQuestionOption_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_MCQQuestionOption_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_MCQQuestionOption_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Video = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorrectAnswerID = table.Column<long>(type: "bigint", nullable: false),
                    QuestionTypeID = table.Column<long>(type: "bigint", nullable: false),
                    TestID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Question", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Question_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Question_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Question_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Question_MCQQuestionOption_CorrectAnswerID",
                        column: x => x.CorrectAnswerID,
                        principalTable: "MCQQuestionOption",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Question_QuestionType_QuestionTypeID",
                        column: x => x.QuestionTypeID,
                        principalTable: "QuestionType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Question_Test_TestID",
                        column: x => x.TestID,
                        principalTable: "Test",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegisterationFormTestAnswer",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EssayAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionID = table.Column<long>(type: "bigint", nullable: false),
                    AnswerID = table.Column<long>(type: "bigint", nullable: false),
                    RegisterationFormParentID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_RegisterationFormTestAnswer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RegisterationFormTestAnswer_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegisterationFormTestAnswer_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegisterationFormTestAnswer_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegisterationFormTestAnswer_MCQQuestionOption_AnswerID",
                        column: x => x.AnswerID,
                        principalTable: "MCQQuestionOption",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegisterationFormTestAnswer_Question_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Question",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegisterationFormTestAnswer_RegisterationFormParent_RegisterationFormParentID",
                        column: x => x.RegisterationFormParentID,
                        principalTable: "RegisterationFormParent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryField_DeletedByUserId",
                table: "CategoryField",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryField_FieldTypeID",
                table: "CategoryField",
                column: "FieldTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryField_InsertedByUserId",
                table: "CategoryField",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryField_RegistrationCategoryID",
                table: "CategoryField",
                column: "RegistrationCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryField_UpdatedByUserId",
                table: "CategoryField",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldOption_CategoryFieldID",
                table: "FieldOption",
                column: "CategoryFieldID");

            migrationBuilder.CreateIndex(
                name: "IX_FieldOption_DeletedByUserId",
                table: "FieldOption",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldOption_InsertedByUserId",
                table: "FieldOption",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldOption_UpdatedByUserId",
                table: "FieldOption",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewState_Name",
                table: "InterviewState",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTime_AcademicYearID",
                table: "InterviewTime",
                column: "AcademicYearID");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTime_DeletedByUserId",
                table: "InterviewTime",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTime_InsertedByUserId",
                table: "InterviewTime",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTime_UpdatedByUserId",
                table: "InterviewTime",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MCQQuestionOption_DeletedByUserId",
                table: "MCQQuestionOption",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MCQQuestionOption_InsertedByUserId",
                table: "MCQQuestionOption",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MCQQuestionOption_Question_ID",
                table: "MCQQuestionOption",
                column: "Question_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MCQQuestionOption_UpdatedByUserId",
                table: "MCQQuestionOption",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_CorrectAnswerID",
                table: "Question",
                column: "CorrectAnswerID");

            migrationBuilder.CreateIndex(
                name: "IX_Question_DeletedByUserId",
                table: "Question",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_InsertedByUserId",
                table: "Question",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_QuestionTypeID",
                table: "Question",
                column: "QuestionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Question_TestID",
                table: "Question",
                column: "TestID");

            migrationBuilder.CreateIndex(
                name: "IX_Question_UpdatedByUserId",
                table: "Question",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormInterview_DeletedByUserId",
                table: "RegisterationFormInterview",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormInterview_InsertedByUserId",
                table: "RegisterationFormInterview",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormInterview_InterviewStateID",
                table: "RegisterationFormInterview",
                column: "InterviewStateID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormInterview_InterviewTimeID",
                table: "RegisterationFormInterview",
                column: "InterviewTimeID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormInterview_RegisterationFormParentID",
                table: "RegisterationFormInterview",
                column: "RegisterationFormParentID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormInterview_UpdatedByUserId",
                table: "RegisterationFormInterview",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormParent_DeletedByUserId",
                table: "RegisterationFormParent",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormParent_InsertedByUserId",
                table: "RegisterationFormParent",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormParent_ParentID",
                table: "RegisterationFormParent",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormParent_RegisterationFormStateID",
                table: "RegisterationFormParent",
                column: "RegisterationFormStateID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormParent_RegistrationFormID",
                table: "RegisterationFormParent",
                column: "RegistrationFormID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormParent_UpdatedByUserId",
                table: "RegisterationFormParent",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormState_Name",
                table: "RegisterationFormState",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormSubmittion_CategoryFieldID",
                table: "RegisterationFormSubmittion",
                column: "CategoryFieldID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormSubmittion_DeletedByUserId",
                table: "RegisterationFormSubmittion",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormSubmittion_InsertedByUserId",
                table: "RegisterationFormSubmittion",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormSubmittion_RegisterationFormParentID",
                table: "RegisterationFormSubmittion",
                column: "RegisterationFormParentID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormSubmittion_UpdatedByUserId",
                table: "RegisterationFormSubmittion",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormTest_DeletedByUserId",
                table: "RegisterationFormTest",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormTest_InsertedByUserId",
                table: "RegisterationFormTest",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormTest_RegisterationFormParentID",
                table: "RegisterationFormTest",
                column: "RegisterationFormParentID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormTest_StateID",
                table: "RegisterationFormTest",
                column: "StateID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormTest_TestID",
                table: "RegisterationFormTest",
                column: "TestID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormTest_UpdatedByUserId",
                table: "RegisterationFormTest",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormTestAnswer_AnswerID",
                table: "RegisterationFormTestAnswer",
                column: "AnswerID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormTestAnswer_DeletedByUserId",
                table: "RegisterationFormTestAnswer",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormTestAnswer_InsertedByUserId",
                table: "RegisterationFormTestAnswer",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormTestAnswer_QuestionID",
                table: "RegisterationFormTestAnswer",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormTestAnswer_RegisterationFormParentID",
                table: "RegisterationFormTestAnswer",
                column: "RegisterationFormParentID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormTestAnswer_UpdatedByUserId",
                table: "RegisterationFormTestAnswer",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationCategory_DeletedByUserId",
                table: "RegistrationCategory",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationCategory_InsertedByUserId",
                table: "RegistrationCategory",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationCategory_RegistrationFormID",
                table: "RegistrationCategory",
                column: "RegistrationFormID");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationCategory_UpdatedByUserId",
                table: "RegistrationCategory",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationForm_DeletedByUserId",
                table: "RegistrationForm",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationForm_InsertedByUserId",
                table: "RegistrationForm",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationForm_UpdatedByUserId",
                table: "RegistrationForm",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Test_AcademicYearID",
                table: "Test",
                column: "AcademicYearID");

            migrationBuilder.CreateIndex(
                name: "IX_Test_DeletedByUserId",
                table: "Test",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Test_InsertedByUserId",
                table: "Test",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Test_SubjectID",
                table: "Test",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Test_UpdatedByUserId",
                table: "Test",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TestState_Name",
                table: "TestState",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MCQQuestionOption_Question_Question_ID",
                table: "MCQQuestionOption",
                column: "Question_ID",
                principalTable: "Question",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MCQQuestionOption_Question_Question_ID",
                table: "MCQQuestionOption");

            migrationBuilder.DropTable(
                name: "FieldOption");

            migrationBuilder.DropTable(
                name: "RegisterationFormInterview");

            migrationBuilder.DropTable(
                name: "RegisterationFormSubmittion");

            migrationBuilder.DropTable(
                name: "RegisterationFormTest");

            migrationBuilder.DropTable(
                name: "RegisterationFormTestAnswer");

            migrationBuilder.DropTable(
                name: "InterviewState");

            migrationBuilder.DropTable(
                name: "InterviewTime");

            migrationBuilder.DropTable(
                name: "CategoryField");

            migrationBuilder.DropTable(
                name: "TestState");

            migrationBuilder.DropTable(
                name: "RegisterationFormParent");

            migrationBuilder.DropTable(
                name: "FieldType");

            migrationBuilder.DropTable(
                name: "RegistrationCategory");

            migrationBuilder.DropTable(
                name: "RegisterationFormState");

            migrationBuilder.DropTable(
                name: "RegistrationForm");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "MCQQuestionOption");

            migrationBuilder.DropTable(
                name: "QuestionType");

            migrationBuilder.DropTable(
                name: "Test");
        }
    }
}
