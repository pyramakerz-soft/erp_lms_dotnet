using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class AddConfirmCodeInParentAndMakeEmailNotUniqueInRegistrationFormMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RegisterationFormParent_Email",
                table: "RegisterationFormParent");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "RegisterationFormParent",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ConfirmationCode",
                table: "Parent",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmationCode",
                table: "Parent");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "RegisterationFormParent",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormParent_Email",
                table: "RegisterationFormParent",
                column: "Email",
                unique: true);
        }
    }
}
