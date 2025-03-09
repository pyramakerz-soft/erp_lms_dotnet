using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class PurchaseOrderMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO InventoryFlags (ID, enName, FlagValue, ItemInOut, en_Title, arName, ar_Title)
                VALUES (13, 'Purchase Order', 1, 0, 'Supplier', N'أمر الشراء', N'المورد')
                 ");
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM InventoryFlags
                WHERE ID = 13
                ");
        }
    }
}
