using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class OptionFieldInRegistrationFormSubmittionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Answer",
                table: "RegisterationFormSubmittion",
                newName: "TextAnswer");

            migrationBuilder.AddColumn<long>(
                name: "SelectedFieldOptionID",
                table: "RegisterationFormSubmittion",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormSubmittion_SelectedFieldOptionID",
                table: "RegisterationFormSubmittion",
                column: "SelectedFieldOptionID");

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterationFormSubmittion_FieldOption_SelectedFieldOptionID",
                table: "RegisterationFormSubmittion",
                column: "SelectedFieldOptionID",
                principalTable: "FieldOption",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegisterationFormSubmittion_FieldOption_SelectedFieldOptionID",
                table: "RegisterationFormSubmittion");

            migrationBuilder.DropIndex(
                name: "IX_RegisterationFormSubmittion_SelectedFieldOptionID",
                table: "RegisterationFormSubmittion");

            migrationBuilder.DropColumn(
                name: "SelectedFieldOptionID",
                table: "RegisterationFormSubmittion");

            migrationBuilder.RenameColumn(
                name: "TextAnswer",
                table: "RegisterationFormSubmittion",
                newName: "Answer");
        }
    }
}
