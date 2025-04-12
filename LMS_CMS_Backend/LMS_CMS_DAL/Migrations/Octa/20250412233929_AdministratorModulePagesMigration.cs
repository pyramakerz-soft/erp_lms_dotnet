using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class AdministratorModulePagesMigration : Migration
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
                (22, 'School', N'المدارس', 11, 1),
                (23, 'Employee Create', N'اضافة موظفين', 11, 0),
                (24, 'Employee Edit', N' نعديل بيانات الموظف', 11, 0),
                (25, 'Employee Details', N'بيانات الموظف', 11, 0),
                (26, 'Role Edit', N'تعديل الصلاحيات', 11, 0),
                (27, 'Role Create', N'اضافة صلاحيات', 11, 0),
                (28, 'SemesterView', N' بيانات الفصل الدراسي', 11, 0),
                (29, 'Department', N'القسم', 11, 1),
                (30, 'Job', N'العمل', 11, 0),
                (31, 'Job Category', N'الفئة الوظيفية', 11, 1),
                (32, 'Academic Degree', N'الدرجة الأكاديمية', 11, 1),
                (33, 'Reasons For Leaving Work', N'أسباب ترك العمل', 11, 1);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Page WHERE ID BETWEEN 11 AND 33");
        }
    }
}
