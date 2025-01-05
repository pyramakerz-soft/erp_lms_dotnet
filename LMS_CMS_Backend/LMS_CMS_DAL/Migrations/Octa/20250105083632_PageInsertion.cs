using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class PageInsertion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Page(ID, en_name, ar_name, Page_ID, IsDisplay) VALUES
                (11, 'Administrator', N'الادارة', NULL, 1),
                (12, 'Role', N'الصلاحيات', 11, 1),
                (13, 'Employee', N'الموظفين', 11, 1),
                (14, 'Violation Types', N'انواع المخالفات', 11, 1), 
                (15, 'Academic Years', N'الاعوام الدراسية', 11, 1),
                (16, 'Semester', N'الفصول الدراسية', 11, 0),
                (17, 'Section', N'المراحل الدراسية', 11, 1),
                (18, 'Grade', N'مستوى الصف', 11, 0),
                (19, 'Building', N'المباني', 11, 1),
                (20, 'Floor', N'الادوار', 11, 0),
                (21, 'Classroom', N'الفصول', 11, 1),
                (22, 'LMS', N'ادارة المدارس', NULL, 1),
                (23, 'Subject Categories', N'انواع المواد', 22, 1),
                (24, 'Subject', N'المواد الدراسية', 22, 1);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Page WHERE ID IN (11,12,13,14,15,16,17,18,19,20,21,22,23,24)");
        }
    }
}
