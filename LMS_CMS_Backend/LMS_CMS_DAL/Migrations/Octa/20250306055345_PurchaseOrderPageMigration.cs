using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class PurchaseOrderPageMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Page(ID, en_name, ar_name, Page_ID, IsDisplay) VALUES  
                (130, 'Purchase Order', N'أمر الشراء', 77, 1),
                (131, 'Purchase Order Item', N'عنصر أمر الشراء', 77, 0);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Page WHERE ID IN (130 ,131 )");
        }
    }
}
