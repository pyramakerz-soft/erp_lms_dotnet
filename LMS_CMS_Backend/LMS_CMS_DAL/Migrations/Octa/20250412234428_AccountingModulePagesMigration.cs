using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class AccountingModulePagesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Page(ID, en_name, ar_name, Page_ID, IsDisplay) VALUES  
                (48, 'Accounting', N'المحاسبة', Null, 1),
                (49, 'Student Accounting', N'الطلاب في الحسابات', 48, 1),
                (50, 'Student Edit Accounting', N'تعديل الطالب في الحسابات', 48, 0),
                (51, 'Supplier', N'الموردين', 48, 1),
                (52, 'Debit', N'المدينون', 48, 1),
                (53, 'Credit', N'الدائنين', 48, 1),
                (54, 'Bank', N'البنوك', 48, 1),
                (55, 'Safe', N'الخزائن', 48, 1),
                (56, 'Outcome', N'المصروفات', 48, 1),
                (57, 'Income', N'ايرادات', 48, 1),
                (58, 'Asset', N'الأصول', 48, 1),
                (59, 'Tuition Fees Type', N'أنواع الرسوم الدراسية', 48, 1),
                (60, 'Tuition Discount Type', N'أنواع خصومات الرسوم الدراسية', 48, 1),
                (61, 'Accounting Entries Doc Type', N'أنواع المستندات الخاصة بالقيدات المحاسبية', 48, 1),
                (62, 'Employee Accounting', N'الموظفين في الحسابات', 48, 1),
                (63, 'Employee Edit Accounting', N'تعديل الموظف في الحسابات', 48, 0),
                (64, 'Payable Doc Type', N'نوع المستند المستحق الدفع', 48, 1),
                (65, 'Receivable Doc Type', N'نوع المستند المستحق', 48, 1),
                (66, 'Add Children', N'إضافة الأطفال', 48, 1),
                (67, 'Fees Activation', N'تفعيل الرسوم', 48, 1),
                (68, 'Receivable', N'مستحق القبض', 48, 1),
                (69, 'Receivable Details', N'تفاصيل المستحقات', 48, 0),
                (70, 'Payable', N'مستحق الدفع', 48, 1),
                (71, 'Payable Details', N'تفاصيل الدفع', 48, 0),
                (72, 'Installment Deduction', N'خصم الأقساط', 48, 1),
                (73, 'Installment Deduction Details', N'تفاصيل خصم الأقساط', 48, 0),
                (74, 'Accounting Entries', N'القيود المحاسبية', 48, 1),
                (75, 'Accounting Entries Details', N'تفاصيل القيود المحاسبية', 48, 0);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Page WHERE ID BETWEEN 48 AND 75");
        }
    }
}
