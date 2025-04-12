using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class InventoryModulePagesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Page(ID, en_name, ar_name, Page_ID, IsDisplay) VALUES  
                (76, 'Inventory', N'المخزون', NULL, 1),
                (77, 'Sales', N'المبيعات', 76, 1),
                (78, 'Sales Returns', N'مردودات مبيعات', 76, 1),
                (79, 'Inventory Categories', N'فئات المخزون', 76, 1),
                (80, 'Inventory Sub Categories', N'فئات المخزون الفرعية', 76, 0),
                (81, 'Stores', N'المتاجر', 76, 1),
                (82, 'Shop', N'المتجر', 76, 1),
                (83, 'Shop Item', N'عنصر المتجر', 76, 0),
                (84, 'Inventory Transaction', N'حركة المخازن', 76, 1),
                (85, 'Purchases', N'مشتريات', 76, 1),
                (86, 'Purchase Returns', N'مردودات مشتريات', 76, 1),
                (87, 'Sales Item', N'عنصر مبيعات', 76, 0),
                (88, 'Sales Returns Item', N'عنصر مرتجعات مبيعات', 76, 0),
                (89, 'Purchases Item', N'عنصر مشتريات', 76, 0),
                (90, 'Purchase Returns Item', N'عنصر مرتجعات مشتريات', 76, 0),
                (91, 'Purchase Order', N'أمر الشراء', 76, 1),
                (92, 'Purchase Order Item', N'عنصر أمر الشراء', 76, 0),
                (93, 'Stocking', N'الجرد', 76, 1),
                (94, 'Stocking Item', N'عنصر الجرد', 76, 0), 
                (95, 'Opening Balances', N'ارصدة افتتاحية', 84, 1),
                (96, 'Addition', N'اضافة', 84, 1),
                (97, 'Addition Adjustment', N'تسوية اضافة', 84, 1),
                (98, 'Disbursement', N'صرف', 84, 1),
                (99, 'Disbursement Adjustment', N'تسوية صرف', 84, 1),
                (100, 'Gifts', N'هدايا', 84, 1),
                (101, 'Transfer to Store', N'تحويل الي مخزن', 84, 1),
                (102, 'Damaged', N'تالف', 84, 1),
                (103, 'Opening Balances Item', N'عنصر ارصدة افتتاحية', 84, 0),
                (104, 'Addition Item', N'عنصر إضافة', 84, 0),
                (105, 'Addition Adjustment Item', N'عنصر تسوية إضافة', 84, 0),
                (106, 'Disbursement Item', N'عنصر صرف', 84, 0),
                (107, 'Disbursement Adjustment Item', N'عنصر تسوية صرف', 84, 0),
                (108, 'Gifts Item', N'عنصر هدايا', 84, 0),
                (109, 'Transfer to Store Item', N'عنصر تحويل إلى مخزن', 84, 0),
                (110, 'Damaged Item', N'عنصر تالف', 84, 0);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Page WHERE ID BETWEEN 76 AND 110");
        }
    }
}
