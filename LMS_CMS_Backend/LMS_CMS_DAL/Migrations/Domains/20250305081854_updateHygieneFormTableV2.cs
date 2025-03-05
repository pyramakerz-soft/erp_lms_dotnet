using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class updateHygieneFormTableV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentHygieneTypes_HygieneTypes_HygieneTypeId",
                table: "StudentHygieneTypes");

            migrationBuilder.DropIndex(
                name: "IX_StudentHygieneTypes_HygieneTypeId",
                table: "StudentHygieneTypes");

            migrationBuilder.DropColumn(
                name: "HygieneTypeId",
                table: "StudentHygieneTypes");

            migrationBuilder.DropColumn(
                name: "SelectAll",
                table: "StudentHygieneTypes");

            migrationBuilder.CreateTable(
                name: "HygieneTypeStudentHygieneTypes",
                columns: table => new
                {
                    HygieneTypesId = table.Column<long>(type: "bigint", nullable: false),
                    StudentHygieneTypesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HygieneTypeStudentHygieneTypes", x => new { x.HygieneTypesId, x.StudentHygieneTypesId });
                    table.ForeignKey(
                        name: "FK_HygieneTypeStudentHygieneTypes_HygieneTypes_HygieneTypesId",
                        column: x => x.HygieneTypesId,
                        principalTable: "HygieneTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HygieneTypeStudentHygieneTypes_StudentHygieneTypes_StudentHygieneTypesId",
                        column: x => x.StudentHygieneTypesId,
                        principalTable: "StudentHygieneTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HygieneTypeStudentHygieneTypes_StudentHygieneTypesId",
                table: "HygieneTypeStudentHygieneTypes",
                column: "StudentHygieneTypesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HygieneTypeStudentHygieneTypes");

            migrationBuilder.AddColumn<long>(
                name: "HygieneTypeId",
                table: "StudentHygieneTypes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "SelectAll",
                table: "StudentHygieneTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_StudentHygieneTypes_HygieneTypeId",
                table: "StudentHygieneTypes",
                column: "HygieneTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentHygieneTypes_HygieneTypes_HygieneTypeId",
                table: "StudentHygieneTypes",
                column: "HygieneTypeId",
                principalTable: "HygieneTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
