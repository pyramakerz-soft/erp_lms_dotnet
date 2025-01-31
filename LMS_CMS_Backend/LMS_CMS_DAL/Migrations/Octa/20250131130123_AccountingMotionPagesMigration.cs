using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class AccountingMotionPagesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Page(ID, en_name, ar_name, Page_ID, IsDisplay) VALUES  
                (65, 'Payable Doc Type', N'نوع المستند المستحق الدفع', 48, 1),
                (66, 'Receivable Doc Type', N'نوع المستند المستحق', 48, 1),
                (67, 'Add Children', N'إضافة الأطفال', 48, 1),
                (68, 'Fees Activation', N'تفعيل الرسوم', 48, 1),
                (69, 'Receivable', N'مستحق القبض', 48, 1),
                (70, 'Receivable Details', N'تفاصيل المستحقات', 48, 1),
                (71, 'Payable', N'مستحق الدفع', 48, 1),
                (72, 'Payable Details', N'تفاصيل الدفع', 48, 1),
                (73, 'Installment Deduction', N'خصم الأقساط', 48, 1),
                (74, 'Installment Deduction Details', N'تفاصيل خصم الأقساط', 48, 1),
                (75, 'Accounting Entries', N'القيود المحاسبية', 48, 1),
                (76, 'Accounting Entries Details', N'تفاصيل القيود المحاسبية', 48, 1);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Page WHERE ID IN (65,66,67,68,69,70,71,72,73,74,75,76)");
        }
    }
}
