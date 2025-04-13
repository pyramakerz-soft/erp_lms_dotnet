using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class ECommerceModulePagesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Page(ID, en_name, ar_name, Page_ID, IsDisplay) VALUES  
                (127, 'E-Commerce', N'التجارة الالكترونية', Null, 1),
                (128, 'The Shop', N'تسوق', 127, 1),
                (129, 'ShopItem', N'تسوق السلعة', 127, 0),
                (130, 'Cart', N'العربة', 127, 0),
                (131, 'Order', N'الطلبات', 127, 0),
                (132, 'Order History', N'سجل الطلبات', 127, 1);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Page WHERE ID BETWEEN 127 AND 132");
        }
    }
}
