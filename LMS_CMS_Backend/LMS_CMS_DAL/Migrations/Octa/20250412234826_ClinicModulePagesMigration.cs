using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class ClinicModulePagesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Page(ID, en_name, ar_name, Page_ID, IsDisplay) VALUES  
                (111, 'Clinic', N'العيادة', NULL, 1),
                (112, 'Hygiene Types', N'اصناف النظاقة الشخصية', 111, 1),
                (113, 'Diagnosis', N'التشخيص', 111, 1),
                (114, 'Drugs', N'الأدوية', 111, 1),
                (115, 'Hygiene Form Medical Report', N'نموذج النظافة الشخصية', 111, 1),
                (116, 'Hygiene Form Data', N'بيانات نموذج النظافة الشخصية', 111, 0),
                (117, 'Add Hygiene', N'إضافة نموذج النظافة الشخصية', 111, 0),
                (118, 'Follow Up', N'المتابعة - التاريخ الطبي', 111, 1),
                (119, 'Medical History', N'التاريخ الطبي', 111, 1),
                (120, 'Medical Report', N'التقرير الطبي', 111, 1),
                (121, 'Medical Report View Parent', N'تفاصيل التقرير الطبي للوالدين', 111, 0),
                (122, 'Medical Report By Parent Form', N'نموذج التقرير الطبي للوالدين', 111, 0),
                (123, 'Medical Report View Doctor', N'تفاصيل التقرير الطبي للطبيب', 111, 0),
                (124, 'Medical Report By Parent Form Hygiene', N'التقرير الطبي - نموذج النظافة الشخصية للوالدين', 111, 0),
                (125, 'Medical Report By Parent Form Follow Up', N'التقرير الطبي - المتابعة للوالدين', 111, 0),
                (126, 'Doses', N'الجرعات', 111, 1);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Page WHERE ID BETWEEN 111 AND 126");
        }
    }
}
