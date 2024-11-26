using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class Insert_Page_Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Pages (ID, en_name, ar_name, Page_ID) VALUES (1, 'Busses', N'الحافلات', NULL)");
            migrationBuilder.Sql("INSERT INTO Pages (ID, en_name, ar_name, Page_ID) VALUES (2, 'Bus Details', N'تفاصيل الحافلات', 1)");
            migrationBuilder.Sql("INSERT INTO Pages (ID, en_name, ar_name, Page_ID) VALUES (3, 'Bus Types', N'أنواع الحافلات', 1)");
            migrationBuilder.Sql("INSERT INTO Pages (ID, en_name, ar_name, Page_ID) VALUES (4, 'Bus Restricts', N'طرق الحافلات', 1)");
            migrationBuilder.Sql("INSERT INTO Pages (ID, en_name, ar_name, Page_ID) VALUES (5, 'Bus Categories', N'فئات الحافلات', 1)");
            migrationBuilder.Sql("INSERT INTO Pages (ID, en_name, ar_name, Page_ID) VALUES (6, 'Bus Status', N'حالة الحافلات', 1)");
            migrationBuilder.Sql("INSERT INTO Pages (ID, en_name, ar_name, Page_ID) VALUES (7, 'Bus Companies', N'شركات الحافلات', 1)");
            migrationBuilder.Sql("INSERT INTO Pages (ID, en_name, ar_name, Page_ID) VALUES (8, 'Print Name Tag', N'طباعة بطاقة الاسم', 1)");
            migrationBuilder.Sql("INSERT INTO Pages (ID, en_name, ar_name, Page_ID) VALUES (9, 'Violations', N'المخالفات', 1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Pages WHERE ID IN (1, 2, 3, 4, 5, 6, 7, 8, 9)");
        }
    }
}
