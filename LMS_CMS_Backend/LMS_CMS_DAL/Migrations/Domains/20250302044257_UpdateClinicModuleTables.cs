using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class UpdateClinicModuleTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "GradeId",
                table: "HygieneForms",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "GradeId",
                table: "FollowUps",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "StudentId",
                table: "FollowUps",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_HygieneForms_GradeId",
                table: "HygieneForms",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUps_GradeId",
                table: "FollowUps",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUps_StudentId",
                table: "FollowUps",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_FollowUps_Grade_GradeId",
                table: "FollowUps",
                column: "GradeId",
                principalTable: "Grade",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FollowUps_Student_StudentId",
                table: "FollowUps",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HygieneForms_Grade_GradeId",
                table: "HygieneForms",
                column: "GradeId",
                principalTable: "Grade",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FollowUps_Grade_GradeId",
                table: "FollowUps");

            migrationBuilder.DropForeignKey(
                name: "FK_FollowUps_Student_StudentId",
                table: "FollowUps");

            migrationBuilder.DropForeignKey(
                name: "FK_HygieneForms_Grade_GradeId",
                table: "HygieneForms");

            migrationBuilder.DropIndex(
                name: "IX_HygieneForms_GradeId",
                table: "HygieneForms");

            migrationBuilder.DropIndex(
                name: "IX_FollowUps_GradeId",
                table: "FollowUps");

            migrationBuilder.DropIndex(
                name: "IX_FollowUps_StudentId",
                table: "FollowUps");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "HygieneForms");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "FollowUps");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "FollowUps");
        }
    }
}
