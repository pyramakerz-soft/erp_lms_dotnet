using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class RemoveRegistrationFormIDFromRegistrationCategoryMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistrationCategory_RegistrationForm_RegistrationFormID",
                table: "RegistrationCategory");

            migrationBuilder.DropIndex(
                name: "IX_RegistrationCategory_RegistrationFormID",
                table: "RegistrationCategory");

            migrationBuilder.DropColumn(
                name: "RegistrationFormID",
                table: "RegistrationCategory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RegistrationFormID",
                table: "RegistrationCategory",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationCategory_RegistrationFormID",
                table: "RegistrationCategory",
                column: "RegistrationFormID");

            migrationBuilder.AddForeignKey(
                name: "FK_RegistrationCategory_RegistrationForm_RegistrationFormID",
                table: "RegistrationCategory",
                column: "RegistrationFormID",
                principalTable: "RegistrationForm",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
