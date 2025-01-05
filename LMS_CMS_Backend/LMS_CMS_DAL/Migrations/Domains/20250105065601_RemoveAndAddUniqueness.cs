using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class RemoveAndAddUniqueness : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subject_ar_name",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Subject_en_name",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Section_Name",
                table: "Section");

            migrationBuilder.DropIndex(
                name: "IX_Page_ar_name",
                table: "Page");

            migrationBuilder.DropIndex(
                name: "IX_Page_en_name",
                table: "Page");

            migrationBuilder.DropIndex(
                name: "IX_Grade_Name",
                table: "Grade");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeAttachment_Link",
                table: "EmployeeAttachment");

            migrationBuilder.AlterColumn<string>(
                name: "Link",
                table: "EmployeeAttachment",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Violation_Name",
                table: "Violation",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeType_Name",
                table: "EmployeeType",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Violation_Name",
                table: "Violation");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeType_Name",
                table: "EmployeeType");

            migrationBuilder.AlterColumn<string>(
                name: "Link",
                table: "EmployeeAttachment",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_ar_name",
                table: "Subject",
                column: "ar_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subject_en_name",
                table: "Subject",
                column: "en_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Section_Name",
                table: "Section",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Page_ar_name",
                table: "Page",
                column: "ar_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Page_en_name",
                table: "Page",
                column: "en_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grade_Name",
                table: "Grade",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttachment_Link",
                table: "EmployeeAttachment",
                column: "Link",
                unique: true);
        }
    }
}
