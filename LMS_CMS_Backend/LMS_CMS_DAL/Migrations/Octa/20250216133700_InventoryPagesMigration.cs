using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class InventoryPagesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Page(ID, en_name, ar_name, Page_ID, IsDisplay) VALUES  
                (77, 'Inventory', N'المخزون', NULL, 1),
                (78, 'Sales', N'المبيعات', 77, 1),
                (79, 'Sales Item', N'عنصر المبيعات', 77, 1),
                (80, 'Inventory Categories', N'فئات المخزون', 77, 1),
                (81, 'Inventory Sub Categories', N'فئات المخزون الفرعية', 77, 1),
                (82, 'Stores', N'المتاجر', 77, 1),
                (83, 'Shop', N'المتجر', 77, 1),
                (84, 'Shop Item', N'عنصر المتجر', 77, 1);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Page WHERE ID IN (77,78,79,80,81,82,83,84)");
        }
    }
}
