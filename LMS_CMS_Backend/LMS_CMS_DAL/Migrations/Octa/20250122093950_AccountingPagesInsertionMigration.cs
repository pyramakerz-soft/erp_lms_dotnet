using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class AccountingPagesInsertionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Page(ID, en_name, ar_name, Page_ID, IsDisplay) VALUES 
                (43, 'Department', N'القسم', 11, 1),
                (44, 'Job', N'العمل', 11, 0),
                (45, 'Job Category', N'الفئة الوظيفية', 11, 1),
                (46, 'Academic Degree', N'الدرجة الأكاديمية', 11, 1),
                (47, 'Reasons For Leaving Work', N'أسباب ترك العمل', 11, 1),
                (48, 'Accounting', N'المحاسبة', Null, 0),
                (49, 'Student Accounting', N'الطلاب في الحسابات', 48, 1),
                (50, 'Student Edit Accounting', N'تعديل الطالب في الحسابات', 48, 1),
                (51, 'Supplier', N'الموردين', 48, 1),
                (52, 'Debit', N'المدينون', 48, 1),
                (53, 'Credit', N'الدائنين', 48, 1),
                (54, 'Bank', N'البنوك', 48, 1),
                (55, 'Save', N'الخزائن', 48, 1),
                (56, 'Outcome', N'المصروفات', 48, 1),
                (57, 'Income', N'ايرادات', 48, 1),
                (58, 'Asset', N'الأصول', 48, 1),
                (59, 'Tuition Fees Type', N'أنواع الرسوم الدراسية', 48, 1),
                (60, 'Tuition Discount Type', N'أنواع خصومات الرسوم الدراسية', 48, 1),
                (61, 'Accounting Entries Doc Type', N'أنواع المستندات الخاصة بالقيدات المحاسبية', 48, 1),
                (62, 'Employee Accounting', N'الموظفين في الحسابات', 48, 1),
                (63, 'Employee Edit Accounting', N'تعديل الموظف في الحسابات', 48, 0);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Page WHERE ID IN (43,44,45,46,47,48,49,50,52,53,54,55,56,57,58,59,60,61,62,63)");
        }
    }
}
