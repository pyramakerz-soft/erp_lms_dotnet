using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class InVentoryTransactionPages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Rename pages
            migrationBuilder.Sql("UPDATE Page SET en_name = 'Purchase Transaction', ar_name = N'حركة المشتريات' WHERE ID = 85");
            migrationBuilder.Sql("UPDATE Page SET en_name = 'Sales Transaction', ar_name = N'حركة المبيعات' WHERE ID = 77");
            migrationBuilder.Sql("UPDATE Page SET en_name = 'Master Data', ar_name = N'الصفحات الاساسية' WHERE ID = 79");

            // Set parent IDs
            migrationBuilder.Sql("UPDATE Page SET Page_ID = 85 WHERE ID = 86");
            migrationBuilder.Sql("UPDATE Page SET Page_ID = 77 WHERE ID = 78");
            migrationBuilder.Sql("UPDATE Page SET Page_ID = 79 WHERE ID = 80");
            migrationBuilder.Sql("UPDATE Page SET Page_ID = 79 WHERE ID = 81");
            migrationBuilder.Sql("UPDATE Page SET Page_ID = 79 WHERE ID = 82");
            migrationBuilder.Sql("UPDATE Page SET Page_ID = 79 WHERE ID = 83");

            // Insert new pages
            migrationBuilder.Sql(@"
                INSERT INTO Page (ID, en_name, ar_name, Page_ID, IsDisplay) VALUES  
                (134, 'Purchase', N'المشتريات', 85, 1),
                (135, 'Sales', N'المبيعات', 77, 1)
            ");

            migrationBuilder.Sql(@"
                INSERT INTO Page (ID, en_name, ar_name, Page_ID, IsDisplay) VALUES  
                (136, 'Inventory Categories', N'فئات المخزون', 79, 1)
            ");

            // Rename ID 82
            migrationBuilder.Sql("UPDATE Page SET en_name = 'Items' WHERE ID = 82");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert insertions
            migrationBuilder.Sql("DELETE FROM Page WHERE ID IN (134, 135, 136)");

            // Revert renames
            migrationBuilder.Sql("UPDATE Page SET en_name = 'Sales', ar_name = N'المبيعات' WHERE ID = 77");
            migrationBuilder.Sql("UPDATE Page SET en_name = 'Purchases', ar_name = N'مشتريات' WHERE ID = 85");
            migrationBuilder.Sql("UPDATE Page SET en_name = 'Inventory Categories', ar_name = N'فئات المخزون' WHERE ID = 79");

            // Revert parent IDs
            migrationBuilder.Sql("UPDATE Page SET Page_ID = 76 WHERE ID = 78");
            migrationBuilder.Sql("UPDATE Page SET Page_ID = 76 WHERE ID = 86");
            migrationBuilder.Sql("UPDATE Page SET Page_ID = 76 WHERE ID IN (80, 81, 82, 83)");

            // Revert item rename
            migrationBuilder.Sql("UPDATE Page SET en_name = 'Shop' WHERE ID = 82");
        }
    }
}
