using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class InsertBookCorrectionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO EvaluationBookCorrection(EnglishName, ArabicName) VALUES  
                ('WB Workbook', N'دفتر العمل'),
                ('CB Copybook', N'الدفتر'),
                ('WS Worksheet', N'ورقة العمل'),
                ('BL Booklet', N'الكتيب'),
                ('B Book', N'الكتاب'),
                ('TB Textbook', N'الكتاب المدرسي');
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM EvaluationBookCorrection WHERE ID BETWEEN 1 AND 6");
        }
    }
}
