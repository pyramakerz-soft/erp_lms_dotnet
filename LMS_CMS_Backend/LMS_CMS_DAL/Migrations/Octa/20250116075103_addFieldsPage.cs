using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class addFieldsPage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Page(ID, en_name, ar_name, Page_ID, IsDisplay) VALUES
                (36, 'Category Fields', N'حقول الفئة', 26, 0);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Page WHERE ID IN (36)");
        }
    }
}
