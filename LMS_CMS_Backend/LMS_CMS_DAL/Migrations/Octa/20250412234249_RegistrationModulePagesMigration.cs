using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class RegistrationModulePagesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Page(ID, en_name, ar_name, Page_ID, IsDisplay) VALUES 
                (37, 'Registration', N'التسجيلات', NULL, 1),
                (38, 'Registration Form Field', N'حقل نموذج التسجيل', 37, 1),
                (39, 'Registration Form', N'نموذج التسجيل', 37, 1),
                (40, 'Admission Test', N'اختبار القبول', 37, 1),
                (41, 'Question', N'الأسئلة', 37, 0),
                (42, 'Classroom Accommodation', N'أماكن إقامة في الفصول الدراسية', 37, 1),
                (43, 'Registration Confirmation', N'تأكيد التسجيل', 37, 1),
                (44, 'Registration Confirmation Test', N'اختبار تأكيد التسجيل', 37, 0),
                (45, 'Interview Time Table', N'جدول المقابلات', 37, 1),
                (46, 'Interview Registration', N'تسجيل المقابلات', 37, 0),
                (47, 'Category Fields', N'حقول الفئة', 37, 0);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Page WHERE ID BETWEEN 37 AND 47");
        }
    }
}
