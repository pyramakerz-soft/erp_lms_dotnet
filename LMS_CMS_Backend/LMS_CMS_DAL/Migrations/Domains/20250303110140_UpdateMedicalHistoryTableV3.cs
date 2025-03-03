using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class UpdateMedicalHistoryTableV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalHistories_Classroom_ClassRoomID",
                table: "MedicalHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalHistories_Grade_GradeId",
                table: "MedicalHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalHistories_School_SchoolId",
                table: "MedicalHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalHistories_Student_StudentId",
                table: "MedicalHistories");

            migrationBuilder.AlterColumn<long>(
                name: "StudentId",
                table: "MedicalHistories",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "SchoolId",
                table: "MedicalHistories",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "GradeId",
                table: "MedicalHistories",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "ClassRoomID",
                table: "MedicalHistories",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalHistories_Classroom_ClassRoomID",
                table: "MedicalHistories",
                column: "ClassRoomID",
                principalTable: "Classroom",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalHistories_Grade_GradeId",
                table: "MedicalHistories",
                column: "GradeId",
                principalTable: "Grade",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalHistories_School_SchoolId",
                table: "MedicalHistories",
                column: "SchoolId",
                principalTable: "School",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalHistories_Student_StudentId",
                table: "MedicalHistories",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalHistories_Classroom_ClassRoomID",
                table: "MedicalHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalHistories_Grade_GradeId",
                table: "MedicalHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalHistories_School_SchoolId",
                table: "MedicalHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalHistories_Student_StudentId",
                table: "MedicalHistories");

            migrationBuilder.AlterColumn<long>(
                name: "StudentId",
                table: "MedicalHistories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "SchoolId",
                table: "MedicalHistories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "GradeId",
                table: "MedicalHistories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ClassRoomID",
                table: "MedicalHistories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalHistories_Classroom_ClassRoomID",
                table: "MedicalHistories",
                column: "ClassRoomID",
                principalTable: "Classroom",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalHistories_Grade_GradeId",
                table: "MedicalHistories",
                column: "GradeId",
                principalTable: "Grade",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalHistories_School_SchoolId",
                table: "MedicalHistories",
                column: "SchoolId",
                principalTable: "School",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalHistories_Student_StudentId",
                table: "MedicalHistories",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
