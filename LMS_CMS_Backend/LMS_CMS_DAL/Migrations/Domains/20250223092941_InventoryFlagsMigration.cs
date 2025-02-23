using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class InventoryFlagsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO InventoryFlags(ID, Name) VALUES  
                (1, N'المبيعات'),
                (2, N'اضافة'),
                (3, N'صرف'),
                (4, N'هدايا'),
                (5, N'هالك'),
                (6, N'تحويل الي مخزن'),
                (7, N'ارصدة افتتاحية');
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM InventoryFlags WHERE ID IN (1,2,3,4,5,6,7)");
        }
    }
}
