using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class StudentRegistrationFormIDMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RegistrationFormParentID",
                table: "Student",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_RegistrationFormParentID",
                table: "Student",
                column: "RegistrationFormParentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_RegisterationFormParent_RegistrationFormParentID",
                table: "Student",
                column: "RegistrationFormParentID",
                principalTable: "RegisterationFormParent",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_RegisterationFormParent_RegistrationFormParentID",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_RegistrationFormParentID",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "RegistrationFormParentID",
                table: "Student");
        }
    }
}
