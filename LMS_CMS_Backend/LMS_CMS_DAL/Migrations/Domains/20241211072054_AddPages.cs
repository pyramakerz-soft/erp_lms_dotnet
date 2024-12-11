using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class AddPages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Pages (ID, en_name, ar_name, Page_ID, IsDisplay) VALUES (1, 'Busses', N'الحافلات', NULL, 1)");
            migrationBuilder.Sql("INSERT INTO Pages (ID, en_name, ar_name, Page_ID, IsDisplay) VALUES (2, 'Bus Details', N'تفاصيل الحافلات', 1, 1)");
            migrationBuilder.Sql("INSERT INTO Pages (ID, en_name, ar_name, Page_ID, IsDisplay) VALUES (3, 'Bus Types', N'أنواع الحافلات', 1, 1)");
            migrationBuilder.Sql("INSERT INTO Pages (ID, en_name, ar_name, Page_ID, IsDisplay) VALUES (4, 'Bus Restricts', N'طرق الحافلات', 1, 1)");
            migrationBuilder.Sql("INSERT INTO Pages (ID, en_name, ar_name, Page_ID, IsDisplay) VALUES (5, 'Bus Categories', N'فئات الحافلات', 1, 1)");
            migrationBuilder.Sql("INSERT INTO Pages (ID, en_name, ar_name, Page_ID, IsDisplay) VALUES (6, 'Bus Status', N'حالة الحافلات', 1, 1)");
            migrationBuilder.Sql("INSERT INTO Pages (ID, en_name, ar_name, Page_ID, IsDisplay) VALUES (7, 'Bus Companies', N'شركات الحافلات', 1, 1)");
            migrationBuilder.Sql("INSERT INTO Pages (ID, en_name, ar_name, Page_ID, IsDisplay) VALUES (9, 'Bus Students', N'طلاب الحافلات', 0, 1)");
            migrationBuilder.Sql("INSERT INTO Pages (ID, en_name, ar_name, Page_ID, IsDisplay) VALUES (10, 'Print Name Tag', N'طباعة بطاقة الاسم', 1, 1)");
            migrationBuilder.Sql("INSERT INTO Pages (ID, en_name, ar_name, Page_ID, IsDisplay) VALUES (11, 'Violations', N'المخالفات', 1, 1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Pages WHERE ID IN (1, 2, 3, 4, 5, 6, 7, 9, 10, 11)");
        }

    }
}
