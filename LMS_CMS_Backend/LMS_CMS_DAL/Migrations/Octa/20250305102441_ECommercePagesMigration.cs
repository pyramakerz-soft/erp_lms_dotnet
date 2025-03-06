using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class ECommercePagesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Page(ID, en_name, ar_name, Page_ID, IsDisplay) VALUES  
                (125, 'E-Commerce', N'التجارة الالكترونية', Null, 1),
                (126, 'The Shop', N'تسوق', 125, 1),
                (127, 'ShopItem', N'تسوق السلعة', 125, 0),
                (128, 'Cart', N'العربة', 125, 0),
                (129, 'Order', N'الطلبات', 125, 0);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Page WHERE ID BETWEEN 125 AND 129");
        }
    }
}
