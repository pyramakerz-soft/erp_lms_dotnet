using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class InventorySalesUpdateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE Page
                SET  IsDisplay= 1  ,ar_name= N'مردودات مبيعات' , en_name ='Sales Returns'
                WHERE ID = 79;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE Page
                SET IsDisplay = 0, ar_name = N'عنصر المبيعات', en_name = 'Sales Item'
                WHERE ID = 79;
            ");
        }
    }
}
