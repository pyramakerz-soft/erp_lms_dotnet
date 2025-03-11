using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class InsertPageDataMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Page(ID, en_name, ar_name, Page_ID, IsDisplay) VALUES
                (1, 'Buses', N'الحافلات', NULL, 1),
                (2, 'Bus Details', N'تفاصيل الحافلات', 1, 1),
                (3, 'Bus Types', N'أنواع الحافلات', 1, 1),
                (4, 'Bus Districts', N'طرق الحافلات', 1, 1),
                (5, 'Bus Categories', N'فئات الحافلات', 1, 1),
                (6, 'Bus Status', N'حالة الحافلات', 1, 1),
                (7, 'Bus Companies', N'شركات الحافلات', 1, 1),
                (8, 'Bus Students', N'طلاب الحافلات', 1, 0),
                (9, 'Print Name Tag', N'طباعة بطاقة الاسم', 1, 1),
                (10, 'Violations', N'المخالفات', 1, 1)
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Page WHERE ID IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10)");
        }
    }
}
