using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class InsertionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO QuestionType(ID, Name) VALUES
                (1, 'True/False'),
                (2, 'MCQ'),
                (3, 'Essay');
            ");

            migrationBuilder.Sql(@"
                INSERT INTO FieldType(ID, Name) VALUES
                (1, 'Text One Line'),
                (2, 'Text Multi Lines'),
                (3, 'Date'),
                (4, 'Checkbox'),
                (5, 'Multi Options'),
                (6, 'Attachment');
            ");

            migrationBuilder.Sql(@"
                INSERT INTO RegisterationFormState(ID, Name) VALUES
                (1, 'Pending'),
                (2, 'Accepted'),
                (3, 'Declined'),
                (4, 'Waiting List');
            ");

            migrationBuilder.Sql(@"
                INSERT INTO TestState(ID, Name) VALUES
                (1, 'Pending'),
                (2, 'Accepted'),
                (3, 'Declined');
            ");

            migrationBuilder.Sql(@"
                INSERT INTO InterviewState(ID, Name) VALUES
                (1, 'Pending'),
                (2, 'Accepted'),
                (3, 'Declined');
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM QuestionType WHERE ID IN (1,2,3)");
            migrationBuilder.Sql("DELETE FROM FieldType WHERE ID IN (1,2,3,4,5,6)");
            migrationBuilder.Sql("DELETE FROM RegisterationFormState WHERE ID IN (1,2,3,4)");
            migrationBuilder.Sql("DELETE FROM TestState WHERE ID IN (1,2,3)");
            migrationBuilder.Sql("DELETE FROM InterviewState WHERE ID IN (1,2,3)");
        }
    }
}
