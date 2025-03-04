using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class UpdateHygieneFormTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HygieneTypes_StudentHygieneTypes_StudentHygieneTypesId",
                table: "HygieneTypes");

            migrationBuilder.DropIndex(
                name: "IX_HygieneTypes_StudentHygieneTypesId",
                table: "HygieneTypes");

            migrationBuilder.DropColumn(
                name: "StudentHygieneTypesId",
                table: "HygieneTypes");

            migrationBuilder.AddColumn<long>(
                name: "HygieneId",
                table: "StudentHygieneTypes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_StudentHygieneTypes_HygieneId",
                table: "StudentHygieneTypes",
                column: "HygieneId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentHygieneTypes_HygieneTypes_HygieneId",
                table: "StudentHygieneTypes",
                column: "HygieneId",
                principalTable: "HygieneTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentHygieneTypes_HygieneTypes_HygieneId",
                table: "StudentHygieneTypes");

            migrationBuilder.DropIndex(
                name: "IX_StudentHygieneTypes_HygieneId",
                table: "StudentHygieneTypes");

            migrationBuilder.DropColumn(
                name: "HygieneId",
                table: "StudentHygieneTypes");

            migrationBuilder.AddColumn<long>(
                name: "StudentHygieneTypesId",
                table: "HygieneTypes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HygieneTypes_StudentHygieneTypesId",
                table: "HygieneTypes",
                column: "StudentHygieneTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_HygieneTypes_StudentHygieneTypes_StudentHygieneTypesId",
                table: "HygieneTypes",
                column: "StudentHygieneTypesId",
                principalTable: "StudentHygieneTypes",
                principalColumn: "Id");
        }
    }
}
