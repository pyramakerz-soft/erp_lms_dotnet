using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class addPageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Page",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    en_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ar_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Page_ID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Page_Page_Page_ID",
                        column: x => x.Page_ID,
                        principalTable: "Page",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_Page_Page_ID",
                table: "Page",
                column: "Page_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Page");
        }
    }
}
