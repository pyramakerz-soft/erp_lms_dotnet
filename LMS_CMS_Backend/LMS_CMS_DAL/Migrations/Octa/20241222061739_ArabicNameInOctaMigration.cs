using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class ArabicNameInOctaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Octa_Email",
                table: "Octa");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Octa");

            migrationBuilder.AddColumn<string>(
                name: "Arabic_Name",
                table: "Octa",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Arabic_Name",
                table: "Octa");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Octa",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Octa_Email",
                table: "Octa",
                column: "Email",
                unique: true);
        }
    }
}
