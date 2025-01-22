using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class InsertLinkFileMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO LinkFile(ID, Name, ArName) VALUES
                (1, 'Clients', N'العملاء'),
                (2, 'Suppliers', N'الموردين'),
                (3, 'Debit', N'المدينون'),
                (4, 'Credits', N'الدائنين'),
                (5, 'Saves', N'الخزائن'),
                (6, 'Banks', N'البنوك'),
                (7, 'Incomes', N'ايرادات'),
                (8, 'Outcomes', N'المصروفات'),
                (9, 'Assets', N'الأصول'),
                (10, 'Employees', N'الموظفين'),
                (11, 'Tuition Fees Types', N'أنواع الرسوم الدراسية'),
                (12, 'Tuition Discount Types', N'أنواع خصومات الرسوم الدراسية');
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM LinkFile WHERE ID IN (1,2,3,4,5,6,7,8,9,10,11,12)");
        }
    }
}
