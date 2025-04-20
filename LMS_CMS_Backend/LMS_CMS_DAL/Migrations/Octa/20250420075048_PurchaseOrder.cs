using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class PurchaseOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Page SET Page_ID = 85 WHERE ID = 91");
            migrationBuilder.Sql("UPDATE Page SET Page_ID = 85 WHERE ID = 92");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Page SET Page_ID = 76 WHERE ID = 91");
            migrationBuilder.Sql("UPDATE Page SET Page_ID = 76 WHERE ID = 92");

        }
    }
}
