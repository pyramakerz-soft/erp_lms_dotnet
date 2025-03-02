using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class AddFollowUpTypesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "HygieneFormId",
                table: "StudentHygieneTypes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentHygieneTypes_HygieneFormId",
                table: "StudentHygieneTypes",
                column: "HygieneFormId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentHygieneTypes_HygieneForms_HygieneFormId",
                table: "StudentHygieneTypes",
                column: "HygieneFormId",
                principalTable: "HygieneForms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentHygieneTypes_HygieneForms_HygieneFormId",
                table: "StudentHygieneTypes");

            migrationBuilder.DropIndex(
                name: "IX_StudentHygieneTypes_HygieneFormId",
                table: "StudentHygieneTypes");

            migrationBuilder.DropColumn(
                name: "HygieneFormId",
                table: "StudentHygieneTypes");
        }
    }
}
