using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class InsertInventoryTablesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
               INSERT INTO InventoryFlags([ID], [enName], [FlagValue], [ItemInOut], [en_Title], [arName], [ar_Title]) VALUES  
                    (1, 'Opening Balances', 1, 1, '', N'ارصدة افتتحاية', ''),
                    (2, 'Addition', 1, 1, '', N'اضافة', ''),
                    (3, 'Addition Adjustment', 1, 1, '', N'تسوية اضافة', ''),
                    (4, 'Disbursement', -1, -1, '', N'صرف', ''),
                    (5, 'Disbursement Adjustment', -1, -1, '', N'تسوية صرف', ''),
                    (6, 'Gifts', -1, -1, '', N'هدايا', ''),
                    (7, 'Damaged', -1, -1, '', N'هالك', ''),
                    (8, 'Transfer to Warehouse', -1, -1, 'Store', N'تحويل الي مخزن', N'المخزن المحول اليه'),
                    (9, 'Purchases', 1, 1, 'Supplier', N'مشتريات', N'المورد'),
                    (10, 'Purchase Returns', -1, -1, 'Supplier', N'مردودات مشتريات', N'المورد'),
                    (11, 'Sales', 1, -1, 'Student', N'مبيعات', N'العميل'),
                    (12, 'Sales Returns', -1, 1, 'Student', N'مردودات مبيعات', N'العميل'),
                    (13, 'Purchase Order', 1, 0, 'Supplier', N'أمر الشراء', N'المورد');
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM InventoryFlags WHERE ID IN (1,2,3,4,5,6,7,8,9,10,11,12,13)");
        }
    }
}
