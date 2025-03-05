using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class UpdateStudentHygieneTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentHygieneTypes_HygieneTypes_HygieneId",
                table: "StudentHygieneTypes");

            migrationBuilder.RenameColumn(
                name: "HygieneId",
                table: "StudentHygieneTypes",
                newName: "HygieneTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentHygieneTypes_HygieneId",
                table: "StudentHygieneTypes",
                newName: "IX_StudentHygieneTypes_HygieneTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentHygieneTypes_HygieneTypes_HygieneTypeId",
                table: "StudentHygieneTypes",
                column: "HygieneTypeId",
                principalTable: "HygieneTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentHygieneTypes_HygieneTypes_HygieneTypeId",
                table: "StudentHygieneTypes");

            migrationBuilder.RenameColumn(
                name: "HygieneTypeId",
                table: "StudentHygieneTypes",
                newName: "HygieneId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentHygieneTypes_HygieneTypeId",
                table: "StudentHygieneTypes",
                newName: "IX_StudentHygieneTypes_HygieneId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentHygieneTypes_HygieneTypes_HygieneId",
                table: "StudentHygieneTypes",
                column: "HygieneId",
                principalTable: "HygieneTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
