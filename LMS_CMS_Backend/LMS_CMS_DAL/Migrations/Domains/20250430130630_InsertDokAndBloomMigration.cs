using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class InsertDokAndBloomMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO DokLevel(ID, EnglishName, ArabicName) VALUES  
                (1, 'Recall and Reproduction', N'الاستدعاء وإعادة الإنتاج'),
                (2, 'Skills and Concepts', N'المهارات والمفاهيم'),
                (3, 'Strategic Thinking', N'التفكير الاستراتيجي'),
                (4, 'Extended Thinking', N'التفكير المتعمق');
            ");

            migrationBuilder.Sql(@"
                INSERT INTO BloomLevel(ID, EnglishName, ArabicName) VALUES  
                (1, 'Remember', N'التذكر'),
                (2, 'Understand', N'الفهم'),
                (3, 'Apply', N'التطبيق'),
                (4, 'Analyze', N'التحليل'),
                (5, 'Evaluate', N'التقييم'),
                (6, 'Create', N'الإنشاء');
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
               DELETE FROM DokLevel WHERE ID IN (1, 2, 3, 4);
            ");
            
            migrationBuilder.Sql(@"
                DELETE FROM BloomLevel WHERE ID IN (1, 2, 3, 4, 5, 6);
            ");
        }
    }
}
