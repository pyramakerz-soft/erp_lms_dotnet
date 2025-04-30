using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class RemoveWeekDaysMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonLive_WeekDay_WeekDayID",
                table: "LessonLive");

            migrationBuilder.DropForeignKey(
                name: "FK_Semester_WeekDay_WeekEndDayID",
                table: "Semester");

            migrationBuilder.DropForeignKey(
                name: "FK_Semester_WeekDay_WeekStartDayID",
                table: "Semester");

            migrationBuilder.DropTable(
                name: "WeekDay");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonLive_Days_WeekDayID",
                table: "LessonLive",
                column: "WeekDayID",
                principalTable: "Days",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Semester_Days_WeekEndDayID",
                table: "Semester",
                column: "WeekEndDayID",
                principalTable: "Days",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Semester_Days_WeekStartDayID",
                table: "Semester",
                column: "WeekStartDayID",
                principalTable: "Days",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonLive_Days_WeekDayID",
                table: "LessonLive");

            migrationBuilder.DropForeignKey(
                name: "FK_Semester_Days_WeekEndDayID",
                table: "Semester");

            migrationBuilder.DropForeignKey(
                name: "FK_Semester_Days_WeekStartDayID",
                table: "Semester");

            migrationBuilder.CreateTable(
                name: "WeekDay",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    DeletedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    InsertedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    ArabicName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByOctaId = table.Column<long>(type: "bigint", nullable: true),
                    EnglishName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InsertedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertedByOctaId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByOctaId = table.Column<long>(type: "bigint", nullable: true)
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

            migrationBuilder.AddForeignKey(
                name: "FK_LessonLive_WeekDay_WeekDayID",
                table: "LessonLive",
                column: "WeekDayID",
                principalTable: "WeekDay",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

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
    }
}
