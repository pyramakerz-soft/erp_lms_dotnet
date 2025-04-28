using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class EvaluationPages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Page(ID, en_name, ar_name, Page_ID, IsDisplay) VALUES  
                (137, 'Employee Evaluation', N'تقييم الموظف', Null, 1),
                (138, 'Evaluation Master Data', N'البيانات الاساسية', 137, 1),
                (139, 'Template', N'الانواع', 138, 1),
                (140, 'EvaluationTemplateGroup', N'مجموعة انواع التقييم', 138, 0),
                (141, 'EvaluationTemplateGroupQuestion', N'اسالة التقييم', 138, 0),
                (142, 'Book Correction', N'كتب التقييم', 138, 1),
                (143, 'Evaluation Transaction', N'المعاملات', 137, 1),
                (144, 'Evaluation', N'التقييم', 143, 1),
                (145, 'Received Evaluations', N'التقييمات المستلمة', 143, 1),
                (146, 'Created Evaluations', N'التقييمات المُنشأة', 143, 1);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM Page WHERE ID IN (137, 138, 139, 140, 141, 142, 143, 144, 145, 146);
            ");
        }
    }
}
