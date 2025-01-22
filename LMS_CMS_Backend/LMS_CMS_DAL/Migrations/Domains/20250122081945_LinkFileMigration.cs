using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class LinkFileMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LinkFileID",
                table: "AccountingTreeCharts",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LinkFile",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkFile", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountingTreeCharts_LinkFileID",
                table: "AccountingTreeCharts",
                column: "LinkFileID");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingTreeCharts_LinkFile_LinkFileID",
                table: "AccountingTreeCharts",
                column: "LinkFileID",
                principalTable: "LinkFile",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountingTreeCharts_LinkFile_LinkFileID",
                table: "AccountingTreeCharts");

            migrationBuilder.DropTable(
                name: "LinkFile");

            migrationBuilder.DropIndex(
                name: "IX_AccountingTreeCharts_LinkFileID",
                table: "AccountingTreeCharts");

            migrationBuilder.DropColumn(
                name: "LinkFileID",
                table: "AccountingTreeCharts");
        }
    }
}
