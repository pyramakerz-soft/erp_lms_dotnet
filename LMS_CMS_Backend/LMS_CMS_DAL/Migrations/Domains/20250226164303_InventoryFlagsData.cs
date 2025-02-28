using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class InventoryFlagsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM InventoryFlags WHERE ID IN (1,2,3,4,5,6,7)");

            migrationBuilder.Sql(@"
               INSERT INTO InventoryFlags(ID, Name, FlagValue, ItemInOut, Title) VALUES  
                    (1, N'ارصدة افتتحاية', 1, 1, N''),
                    (2, N'اضافة', 1, 1, N''),
                    (3, N'تسوية اضافة', 1, 1, N''),
                    (4, N'صرف', -1, -1, N''),
                    (5, N'تسوية صرف', -1, -1, N''),
                    (6, N'هدايا', -1, -1, N''),
                    (7, N'هالك', -1, -1, N''),
                    (8, N'تحويل الي مخزن', -1, -1, N'المخزن المحول اليه'),
                    (9, N'مشتريات', 1, 1, N'المورد'),
                    (10, N'مردودات مشتريات', -1, -1, N'المورد'),
                    (11, N'مبيعات', 1, -1, N'العميل'),
                    (12, N'مردودات مبيعات', -1, 1, N'العميل');

            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM InventoryFlags WHERE ID IN (1,2,3,4,5,6,7,8,9,10,11,12)");
        }
    }
}
