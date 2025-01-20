using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class RegistrationFormOptionsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE FieldType
                SET Name = 'Multi Checkboxes'
                WHERE ID = 4;
            ");

            migrationBuilder.Sql(@"
                INSERT INTO FieldType (ID, Name)
                VALUES (7, 'Dropdown');
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE FieldType
                SET Name = 'Checkbox'
                WHERE ID = 4;
            ");

            migrationBuilder.Sql(@"
                DELETE FROM FieldType
                WHERE ID = 7;
            ");
        }
    }
}
