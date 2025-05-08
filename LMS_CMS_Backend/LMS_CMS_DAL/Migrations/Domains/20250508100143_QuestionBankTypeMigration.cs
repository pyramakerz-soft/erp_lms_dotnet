using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class QuestionBankTypeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO QuestionBankType (ID, Name) VALUES
                (1, 'True/False'),
                (2, 'MCQ'),
                (3, 'Fill in blank'),
                (4, 'Drag & Drop'),
                (5, 'Order - Sequencing'),
                (6, 'Essay');
            ");
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM QuestionBankType WHERE ID IN (1, 2, 3, 4, 5, 6);
            ");
        }

    }
}
