using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class InventoryFlagUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE InventoryFlags SET enName = N'Opening Balances', en_Title = '', arName = N'ارصدة افتتاحية', ar_Title = '' WHERE ID = 1;
                UPDATE InventoryFlags SET enName = N'Addition', en_Title = '', arName = N'اضافة', ar_Title = '' WHERE ID = 2;
                UPDATE InventoryFlags SET enName = N'Addition Adjustment', en_Title = '', arName = N'تسوية اضافة', ar_Title = '' WHERE ID = 3;
                UPDATE InventoryFlags SET enName = N'Disbursement', en_Title = '', arName = N'صرف', ar_Title = '' WHERE ID = 4;
                UPDATE InventoryFlags SET enName = N'Disbursement Adjustment', en_Title = '', arName = N'تسوية صرف', ar_Title = '' WHERE ID = 5;
                UPDATE InventoryFlags SET enName = N'Gifts', en_Title = '', arName = N'هدايا', ar_Title = '' WHERE ID = 6;
                UPDATE InventoryFlags SET enName = N'Damaged', en_Title = '', arName = N'هالك', ar_Title = '' WHERE ID = 7;
                UPDATE InventoryFlags SET enName = N'Transfer to Store', en_Title = 'Store', arName = N'تحويل الي مخزن', ar_Title = N'المخزن المحول اليه' WHERE ID = 8;
                UPDATE InventoryFlags SET enName = N'Purchases', en_Title = 'Supplier', arName = N'مشتريات', ar_Title = N'المورد' WHERE ID = 9;
                UPDATE InventoryFlags SET enName = N'Purchase Returns', en_Title = 'Supplier', arName = N'مردودات مشتريات', ar_Title = N'المورد' WHERE ID = 10;
                UPDATE InventoryFlags SET enName = N'Sales', en_Title = 'Student', arName = N'مبيعات', ar_Title = N'العميل' WHERE ID = 11;
                UPDATE InventoryFlags SET enName = N'Sales Returns', en_Title = 'Student', arName = N'مردودات مبيعات', ar_Title = N'العميل' WHERE ID = 12;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE InventoryFlags SET enName = NULL, en_Title = NULL, arName = NULL, ar_Title = NULL WHERE ID IN (1,2,3,4,5,6,7,8,9,10,11,12);
            ");
        }
    }
}
