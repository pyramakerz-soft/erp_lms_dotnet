using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class inventoryItemPages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Page(ID, en_name, ar_name, Page_ID, IsDisplay) VALUES  
                (113, 'Opening Balances Item', N'عنصر ارصدة افتتاحية', 101, 0),
                (114, 'Addition Item', N'عنصر إضافة', 101, 0),
                (115, 'Addition Adjustment Item', N'عنصر تسوية إضافة', 101, 0),
                (116, 'Disbursement Item', N'عنصر صرف', 101, 0),
                (117, 'Disbursement Adjustment Item', N'عنصر تسوية صرف', 101, 0),
                (118, 'Gifts Item', N'عنصر هدايا', 101, 0),
                (119, 'Transfer to Store Item', N'عنصر تحويل إلى مخزن', 101, 0),
                (120, 'Damaged Item', N'عنصر تالف', 101, 0),
                (121, 'Sales Item', N'عنصر مبيعات', 78, 0),
                (122, 'Sales Returns Item', N'عنصر مرتجعات مبيعات', 78, 0),
                (123, 'Purchases Item', N'عنصر مشتريات', 110, 0),
                (124, 'Purchase Returns Item', N'عنصر مرتجعات مشتريات', 110, 0);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Page WHERE ID BETWEEN 113 AND 124");
        }
    }
}
