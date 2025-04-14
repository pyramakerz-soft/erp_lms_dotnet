using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class InsertAccountingTablesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO MotionTypes (ID, Name) VALUES (1, 'Debit');
                INSERT INTO MotionTypes (ID, Name) VALUES (2, 'Credit');
            ");

            migrationBuilder.Sql(@"
                INSERT INTO SubTypes (ID, Name) VALUES (1, 'Main');
                INSERT INTO SubTypes (ID, Name) VALUES (2, 'Sub');
            ");

            migrationBuilder.Sql(@"
                INSERT INTO EndTypes (ID, Name) VALUES (1, N'تشغيل');
                INSERT INTO EndTypes (ID, Name) VALUES (2, N'متاجرة');
                INSERT INTO EndTypes (ID, Name) VALUES (3, N'ارباح و خسائر');
                INSERT INTO EndTypes (ID, Name) VALUES (4, N'ميزانية عمومية');
            ");

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
                 (12, 'Tuition Discount Types', N'أنواع خصومات الرسوم الدراسية'),
                 (13,'Student', N'الطلاب');
             ");

            migrationBuilder.Sql(@"
                INSERT INTO OrderState(ID, Name) VALUES
                (1, 'Pending'),
                (2, 'Delivered'),
                (3, 'Canceled');
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM EndTypes WHERE ID IN (1, 2, 3, 4);
            ");

            migrationBuilder.Sql(@"
                DELETE FROM SubTypes WHERE ID IN (1, 2);
            ");

            migrationBuilder.Sql(@"
                DELETE FROM MotionTypes WHERE ID IN (1, 2);
            ");

            migrationBuilder.Sql("DELETE FROM LinkFile WHERE ID IN (1,2,3,4,5,6,7,8,9,10,11,12,13)");

            migrationBuilder.Sql("DELETE FROM OrderState WHERE ID IN (1,2,3)");
        }
    }
}
