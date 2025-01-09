using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class FieldTypeInsertionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO FieldType(ID, Name) VALUES
                (1, 'Text One Line'),
                (2, 'Text Multi Lines'),
                (3, 'Date'),
                (4, 'Checkbox'),
                (5, 'Multi Options'),
                (6, 'Attachment');
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM FieldType WHERE ID IN (1,2,3,4,5,6)");
        }
    }
}
