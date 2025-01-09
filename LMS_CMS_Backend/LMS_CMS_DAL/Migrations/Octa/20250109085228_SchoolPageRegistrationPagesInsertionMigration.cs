using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class SchoolPageRegistrationPagesInsertionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Page(ID, en_name, ar_name, Page_ID, IsDisplay) VALUES
                (25, 'School', N'المدارس', 11, 1),
                (26, 'Registration', N'التسجيلات', NULL, 1),
                (27, 'Registration Form Field', N'حقل نموذج التسجيل', 26, 1),
                (28, 'Registration Form', N'نموذج التسجيل', 26, 1),
                (29, 'Admission Test', N'اختبار القبول', 26, 1),
                (30, 'Question', N'الأسئلة', 26, 0),
                (31, 'Classroom Accommodation', N'أماكن إقامة في الفصول الدراسية', 26, 1),
                (32, 'Registration Confirmation', N'تأكيد التسجيل', 26, 1),
                (33, 'Registration Confirmation Test', N'اختبار تأكيد التسجيل', 26, 0),
                (34, 'Interview Time Table', N'جدول المقابلات', 26, 1),
                (35, 'Interview Registration', N'تسجيل المقابلات', 26, 0);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Page WHERE ID IN (25,26,27,28,29,30,31,32,33,34,35)");
        }
    }
}
