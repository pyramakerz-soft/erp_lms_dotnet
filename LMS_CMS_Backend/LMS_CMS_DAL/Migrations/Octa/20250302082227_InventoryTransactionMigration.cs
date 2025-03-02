using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class InventoryTransactionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Page(ID, en_name, ar_name, Page_ID, IsDisplay) VALUES  
                (101, 'Inventory Transaction', N'حركة المخازن', 77, 1),
                (102, 'Opening Balances', N'ارصدة افتتاحية', 101, 1),
                (103, 'Addition', N'اضافة', 101, 1),
                (104, 'Addition Adjustment', N'تسوية اضافة', 101, 1),
                (105, 'Disbursement', N'صرف', 101, 1),
                (106, 'Disbursement Adjustment', N'تسوية صرف', 101, 1),
                (107, 'Gifts', N'هدايا', 101, 1),
                (108, 'Transfer to Store', N'تحويل الي مخزن', 101, 1),
                (109, 'Damaged', N'تالف', 101, 1),
                (110, 'Purchases', N'مشتريات', 77, 1),
                (111, 'Purchase Returns', N'مردودات مشتريات', 77, 1);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Page WHERE ID IN (101,102,103,104,105,106,107,108,109,110,111)");
        }
    }
}
