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
                (1, 'Busses', N'الحافلات', NULL, 1),
                (2, 'Bus Details', N'تفاصيل الحافلات', 1, 1),
                (3, 'Bus Types', N'أنواع الحافلات', 1, 1),
                (4, 'Bus Districts', N'طرق الحافلات', 1, 1),
                (5, 'Bus Categories', N'فئات الحافلات', 1, 1),
                (6, 'Bus Status', N'حالة الحافلات', 1, 1),
                (7, 'Bus Companies', N'شركات الحافلات', 1, 1),
                (8, 'Bus Students', N'طلاب الحافلات', 1, 0),
                (9, 'Print Name Tag', N'طباعة بطاقة الاسم', 1, 1),
                (10, 'Violations', N'المخالفات', 1, 1),
                (11, 'Administrator', N'الادارة', NULL, 1),
                (12, 'Role', N'الصلحيات', 11, 1),
                (13, 'Accounts Domain', N'مديرين المدارس', 11, 1),
                (14, 'Employee', N'الموظفين', 11, 1),
                (15, 'Violation Types', N'انواع المخالفات', 11, 1),
                (16, 'Academic Years', N'الاعوام الدراسية', 11, 1),
                (17, 'Semester', N'الفصول الدراسية', 11, 0),
                (18, 'Sections & Grade Levels', N'المراحل الدراسية', 11, 1),
                (19, 'Buildings & Floors', N'المباني والادوار', 11, 1),
                (20, 'Classrooms', N'الفصول', 11, 1),
                (21, 'LMS', N'ادارة المدارس', NULL, 1),
                (22, 'Subject Categories', N'انواع المواد', 21, 1),
                (23, 'Subject', N'المواد الدراسية', 21, 1);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Page WHERE ID IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10,11,12,13,14,15,16,17,18,19,20,21,22)");
        }
    }
}
