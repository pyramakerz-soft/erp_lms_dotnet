using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class LMSLessonsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Semester",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WeekEndDayID",
                table: "Semester",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WeekStartDayID",
                table: "Semester",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BloomLevel",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    EnglishName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ArabicName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloomLevel", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DokLevel",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    EnglishName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ArabicName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DokLevel", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LessonActivityType",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnglishName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ArabicName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("PK_LessonActivityType", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LessonActivityType_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LessonActivityType_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LessonActivityType_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "LessonResourceType",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnglishName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ArabicName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("PK_LessonResourceType", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LessonResourceType_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LessonResourceType_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LessonResourceType_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Medal",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnglishName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ArabicName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Medal", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Medal_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Medal_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Medal_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PerformanceType",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnglishName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ArabicName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("PK_PerformanceType", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PerformanceType_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PerformanceType_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PerformanceType_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "SemesterWorkingWeek",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnglishName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ArabicName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SemesterID = table.Column<long>(type: "bigint", nullable: false),
                    DateFrom = table.Column<DateOnly>(type: "date", nullable: false),
                    DateTo = table.Column<DateOnly>(type: "date", nullable: false),
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
                    table.PrimaryKey("PK_SemesterWorkingWeek", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SemesterWorkingWeek_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SemesterWorkingWeek_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SemesterWorkingWeek_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SemesterWorkingWeek_Semester_SemesterID",
                        column: x => x.SemesterID,
                        principalTable: "Semester",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
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
                    table.PrimaryKey("PK_Tag", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tag_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Tag_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Tag_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "WeekDay",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    EnglishName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ArabicName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("PK_WeekDay", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WeekDay_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_WeekDay_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_WeekDay_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "WeightType",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnglishName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ArabicName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("PK_WeightType", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WeightType_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_WeightType_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_WeightType_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "StudentMedal",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<long>(type: "bigint", nullable: false),
                    MedalID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_StudentMedal", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StudentMedal_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_StudentMedal_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_StudentMedal_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_StudentMedal_Medal_MedalID",
                        column: x => x.MedalID,
                        principalTable: "Medal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentMedal_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentPerformance",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<long>(type: "bigint", nullable: false),
                    PerformanceTypeID = table.Column<long>(type: "bigint", nullable: false),
                    SubjectID = table.Column<long>(type: "bigint", nullable: false),
                    Stars = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_StudentPerformance", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StudentPerformance_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_StudentPerformance_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_StudentPerformance_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_StudentPerformance_PerformanceType_PerformanceTypeID",
                        column: x => x.PerformanceTypeID,
                        principalTable: "PerformanceType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentPerformance_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentPerformance_Subject_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subject",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lesson",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnglishTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ArabicTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    SubjectID = table.Column<long>(type: "bigint", nullable: false),
                    SemesterWorkingWeekID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Lesson", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Lesson_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Lesson_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Lesson_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Lesson_SemesterWorkingWeek_SemesterWorkingWeekID",
                        column: x => x.SemesterWorkingWeekID,
                        principalTable: "SemesterWorkingWeek",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lesson_Subject_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subject",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LessonLive",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Period = table.Column<int>(type: "int", nullable: false),
                    LiveLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecordLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeekDayID = table.Column<long>(type: "bigint", nullable: false),
                    ClassroomID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_LessonLive", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LessonLive_Classroom_ClassroomID",
                        column: x => x.ClassroomID,
                        principalTable: "Classroom",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LessonLive_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LessonLive_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LessonLive_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LessonLive_Subject_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subject",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LessonLive_WeekDay_WeekDayID",
                        column: x => x.WeekDayID,
                        principalTable: "WeekDay",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubjectWeightType",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeightTypeID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_SubjectWeightType", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubjectWeightType_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SubjectWeightType_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SubjectWeightType_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SubjectWeightType_Subject_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subject",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectWeightType_WeightType_WeightTypeID",
                        column: x => x.WeightTypeID,
                        principalTable: "WeightType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LessonActivity",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnglishTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ArabicTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AttachmentLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonID = table.Column<long>(type: "bigint", nullable: false),
                    LessonActivityTypeID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_LessonActivity", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LessonActivity_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LessonActivity_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LessonActivity_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LessonActivity_LessonActivityType_LessonActivityTypeID",
                        column: x => x.LessonActivityTypeID,
                        principalTable: "LessonActivityType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LessonActivity_Lesson_LessonID",
                        column: x => x.LessonID,
                        principalTable: "Lesson",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LessonResource",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnglishTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ArabicTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AttachmentLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonResourceTypeID = table.Column<long>(type: "bigint", nullable: false),
                    LessonID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_LessonResource", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LessonResource_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LessonResource_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LessonResource_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LessonResource_LessonResourceType_LessonResourceTypeID",
                        column: x => x.LessonResourceTypeID,
                        principalTable: "LessonResourceType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LessonResource_Lesson_LessonID",
                        column: x => x.LessonID,
                        principalTable: "Lesson",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LessonTag",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonID = table.Column<long>(type: "bigint", nullable: false),
                    TagID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_LessonTag", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LessonTag_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LessonTag_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LessonTag_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LessonTag_Lesson_LessonID",
                        column: x => x.LessonID,
                        principalTable: "Lesson",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LessonTag_Tag_TagID",
                        column: x => x.TagID,
                        principalTable: "Tag",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LessonResourceClassroom",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassroomID = table.Column<long>(type: "bigint", nullable: false),
                    LessonResourceID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_LessonResourceClassroom", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LessonResourceClassroom_Classroom_ClassroomID",
                        column: x => x.ClassroomID,
                        principalTable: "Classroom",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LessonResourceClassroom_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LessonResourceClassroom_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LessonResourceClassroom_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LessonResourceClassroom_LessonResource_LessonResourceID",
                        column: x => x.LessonResourceID,
                        principalTable: "LessonResource",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Semester_WeekEndDayID",
                table: "Semester",
                column: "WeekEndDayID");

            migrationBuilder.CreateIndex(
                name: "IX_Semester_WeekStartDayID",
                table: "Semester",
                column: "WeekStartDayID");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_DeletedByUserId",
                table: "Lesson",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_InsertedByUserId",
                table: "Lesson",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_SemesterWorkingWeekID",
                table: "Lesson",
                column: "SemesterWorkingWeekID");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_SubjectID",
                table: "Lesson",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_UpdatedByUserId",
                table: "Lesson",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonActivity_DeletedByUserId",
                table: "LessonActivity",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonActivity_InsertedByUserId",
                table: "LessonActivity",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonActivity_LessonActivityTypeID",
                table: "LessonActivity",
                column: "LessonActivityTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_LessonActivity_LessonID",
                table: "LessonActivity",
                column: "LessonID");

            migrationBuilder.CreateIndex(
                name: "IX_LessonActivity_UpdatedByUserId",
                table: "LessonActivity",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonActivityType_DeletedByUserId",
                table: "LessonActivityType",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonActivityType_InsertedByUserId",
                table: "LessonActivityType",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonActivityType_UpdatedByUserId",
                table: "LessonActivityType",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonLive_ClassroomID",
                table: "LessonLive",
                column: "ClassroomID");

            migrationBuilder.CreateIndex(
                name: "IX_LessonLive_DeletedByUserId",
                table: "LessonLive",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonLive_InsertedByUserId",
                table: "LessonLive",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonLive_SubjectID",
                table: "LessonLive",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_LessonLive_UpdatedByUserId",
                table: "LessonLive",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonLive_WeekDayID",
                table: "LessonLive",
                column: "WeekDayID");

            migrationBuilder.CreateIndex(
                name: "IX_LessonResource_DeletedByUserId",
                table: "LessonResource",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonResource_InsertedByUserId",
                table: "LessonResource",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonResource_LessonID",
                table: "LessonResource",
                column: "LessonID");

            migrationBuilder.CreateIndex(
                name: "IX_LessonResource_LessonResourceTypeID",
                table: "LessonResource",
                column: "LessonResourceTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_LessonResource_UpdatedByUserId",
                table: "LessonResource",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonResourceClassroom_ClassroomID",
                table: "LessonResourceClassroom",
                column: "ClassroomID");

            migrationBuilder.CreateIndex(
                name: "IX_LessonResourceClassroom_DeletedByUserId",
                table: "LessonResourceClassroom",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonResourceClassroom_InsertedByUserId",
                table: "LessonResourceClassroom",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonResourceClassroom_LessonResourceID",
                table: "LessonResourceClassroom",
                column: "LessonResourceID");

            migrationBuilder.CreateIndex(
                name: "IX_LessonResourceClassroom_UpdatedByUserId",
                table: "LessonResourceClassroom",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonResourceType_DeletedByUserId",
                table: "LessonResourceType",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonResourceType_InsertedByUserId",
                table: "LessonResourceType",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonResourceType_UpdatedByUserId",
                table: "LessonResourceType",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTag_DeletedByUserId",
                table: "LessonTag",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTag_InsertedByUserId",
                table: "LessonTag",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTag_LessonID",
                table: "LessonTag",
                column: "LessonID");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTag_TagID",
                table: "LessonTag",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTag_UpdatedByUserId",
                table: "LessonTag",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Medal_DeletedByUserId",
                table: "Medal",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Medal_InsertedByUserId",
                table: "Medal",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Medal_UpdatedByUserId",
                table: "Medal",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceType_DeletedByUserId",
                table: "PerformanceType",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceType_InsertedByUserId",
                table: "PerformanceType",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceType_UpdatedByUserId",
                table: "PerformanceType",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterWorkingWeek_DeletedByUserId",
                table: "SemesterWorkingWeek",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterWorkingWeek_InsertedByUserId",
                table: "SemesterWorkingWeek",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterWorkingWeek_SemesterID",
                table: "SemesterWorkingWeek",
                column: "SemesterID");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterWorkingWeek_UpdatedByUserId",
                table: "SemesterWorkingWeek",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMedal_DeletedByUserId",
                table: "StudentMedal",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMedal_InsertedByUserId",
                table: "StudentMedal",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMedal_MedalID",
                table: "StudentMedal",
                column: "MedalID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMedal_StudentID",
                table: "StudentMedal",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMedal_UpdatedByUserId",
                table: "StudentMedal",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentPerformance_DeletedByUserId",
                table: "StudentPerformance",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentPerformance_InsertedByUserId",
                table: "StudentPerformance",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentPerformance_PerformanceTypeID",
                table: "StudentPerformance",
                column: "PerformanceTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentPerformance_StudentID",
                table: "StudentPerformance",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentPerformance_SubjectID",
                table: "StudentPerformance",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentPerformance_UpdatedByUserId",
                table: "StudentPerformance",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectWeightType_DeletedByUserId",
                table: "SubjectWeightType",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectWeightType_InsertedByUserId",
                table: "SubjectWeightType",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectWeightType_SubjectID",
                table: "SubjectWeightType",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectWeightType_UpdatedByUserId",
                table: "SubjectWeightType",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectWeightType_WeightTypeID",
                table: "SubjectWeightType",
                column: "WeightTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_DeletedByUserId",
                table: "Tag",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_InsertedByUserId",
                table: "Tag",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_UpdatedByUserId",
                table: "Tag",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WeekDay_DeletedByUserId",
                table: "WeekDay",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WeekDay_InsertedByUserId",
                table: "WeekDay",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WeekDay_UpdatedByUserId",
                table: "WeekDay",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WeightType_DeletedByUserId",
                table: "WeightType",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WeightType_InsertedByUserId",
                table: "WeightType",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WeightType_UpdatedByUserId",
                table: "WeightType",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Semester_WeekDay_WeekEndDayID",
                table: "Semester",
                column: "WeekEndDayID",
                principalTable: "WeekDay",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Semester_WeekDay_WeekStartDayID",
                table: "Semester",
                column: "WeekStartDayID",
                principalTable: "WeekDay",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Semester_WeekDay_WeekEndDayID",
                table: "Semester");

            migrationBuilder.DropForeignKey(
                name: "FK_Semester_WeekDay_WeekStartDayID",
                table: "Semester");

            migrationBuilder.DropTable(
                name: "BloomLevel");

            migrationBuilder.DropTable(
                name: "DokLevel");

            migrationBuilder.DropTable(
                name: "LessonActivity");

            migrationBuilder.DropTable(
                name: "LessonLive");

            migrationBuilder.DropTable(
                name: "LessonResourceClassroom");

            migrationBuilder.DropTable(
                name: "LessonTag");

            migrationBuilder.DropTable(
                name: "StudentMedal");

            migrationBuilder.DropTable(
                name: "StudentPerformance");

            migrationBuilder.DropTable(
                name: "SubjectWeightType");

            migrationBuilder.DropTable(
                name: "LessonActivityType");

            migrationBuilder.DropTable(
                name: "WeekDay");

            migrationBuilder.DropTable(
                name: "LessonResource");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Medal");

            migrationBuilder.DropTable(
                name: "PerformanceType");

            migrationBuilder.DropTable(
                name: "WeightType");

            migrationBuilder.DropTable(
                name: "LessonResourceType");

            migrationBuilder.DropTable(
                name: "Lesson");

            migrationBuilder.DropTable(
                name: "SemesterWorkingWeek");

            migrationBuilder.DropIndex(
                name: "IX_Semester_WeekEndDayID",
                table: "Semester");

            migrationBuilder.DropIndex(
                name: "IX_Semester_WeekStartDayID",
                table: "Semester");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Semester");

            migrationBuilder.DropColumn(
                name: "WeekEndDayID",
                table: "Semester");

            migrationBuilder.DropColumn(
                name: "WeekStartDayID",
                table: "Semester");
        }
    }
}
