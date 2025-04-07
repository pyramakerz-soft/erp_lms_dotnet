using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class HomeRoomMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classroom_Employee_DeletedByUserId",
                table: "Classroom");

            migrationBuilder.AddColumn<long>(
                name: "HomeroomTeacherID",
                table: "Classroom",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classroom_HomeroomTeacherID",
                table: "Classroom",
                column: "HomeroomTeacherID");

            migrationBuilder.AddForeignKey(
                name: "FK_Classroom_Employee_DeletedByUserId",
                table: "Classroom",     
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Classroom_Employee_HomeroomTeacherID",
                table: "Classroom",
                column: "HomeroomTeacherID",
                principalTable: "Employee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classroom_Employee_DeletedByUserId",
                table: "Classroom");

            migrationBuilder.DropForeignKey(
                name: "FK_Classroom_Employee_HomeroomTeacherID",
                table: "Classroom");

            migrationBuilder.DropIndex(
                name: "IX_Classroom_HomeroomTeacherID",
                table: "Classroom");

            migrationBuilder.DropColumn(
                name: "HomeroomTeacherID",
                table: "Classroom");

            migrationBuilder.AddForeignKey(
                name: "FK_Classroom_Employee_DeletedByUserId",
                table: "Classroom",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");
        }
    }
}
