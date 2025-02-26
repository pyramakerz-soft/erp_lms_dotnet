using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class ClinicPagesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Page(ID, en_name, ar_name, Page_ID, IsDisplay) VALUES  
                (85, 'Clinic', N'العيادة', NULL, 1),
                (86, 'Hygiene Types', N'اصناف النظاقة الشخصية', 85, 1),
                (87, 'Diagnosis', N'التشخيص', 85, 1),
                (88, 'Drugs', N'الأدوية', 85, 1),
                (89, 'Hygiene Form Medical Report', N'نموذج النظافة الشخصية', 85, 1),
                (90, 'Hygiene Form Data', N'بيانات نموذج النظافة الشخصية', 85, 0),
                (91, 'Add Hygiene', N'إضافة نموذج النظافة الشخصية', 85, 0),
                (92, 'Follow Up', N'المتابعة - التاريخ الطبي', 85, 1),
                (93, 'Medical History', N'التاريخ الطبي', 85, 1),
                (94, 'Medical Report', N'التقرير الطبي', 85, 1),
                (95, 'Medical Report View Parent', N'تفاصيل التقرير الطبي للوالدين', 85, 0),
                (96, 'Medical Report By Parent Form', N'نموذج التقرير الطبي للوالدين', 85, 0),
                (97, 'Medical Report View Doctor', N'تفاصيل التقرير الطبي للطبيب', 85, 0),
                (98, 'Medical Report By Parent Form Hygiene', N'التقرير الطبي - نموذج النظافة الشخصية للوالدين', 85, 0),
                (99, 'Medical Report By Parent Form Follow Up', N'التقرير الطبي - المتابعة للوالدين', 85, 0),
                (100, 'Doses', N'الجرعات', 85, 1);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Page WHERE ID IN (85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100)");
        }
    }
}
