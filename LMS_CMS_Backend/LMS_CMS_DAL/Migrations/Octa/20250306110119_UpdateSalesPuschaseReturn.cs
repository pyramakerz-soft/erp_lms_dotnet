using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class UpdateSalesPuschaseReturn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE Page
                SET Page_ID = 77
                WHERE ID IN (79, 111, 122, 121, 123, 124, 131);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE Page SET Page_ID = 78 WHERE ID = 79;
                UPDATE Page SET Page_ID = 110 WHERE ID = 111;
                UPDATE Page SET Page_ID = 78 WHERE ID = 122;
                UPDATE Page SET Page_ID = 78 WHERE ID = 121;
                UPDATE Page SET Page_ID = 110 WHERE ID = 123;
                UPDATE Page SET Page_ID = 110 WHERE ID = 124;
                UPDATE Page SET Page_ID = 130 WHERE ID = 131;
           ");
        }
    }
}
