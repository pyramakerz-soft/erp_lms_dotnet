using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class InsertAcademicDegreeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO AcademicDegrees (ID, Name) VALUES (1, 'Bachelor');
                INSERT INTO AcademicDegrees (ID, Name) VALUES (2, 'Master');
                INSERT INTO AcademicDegrees (ID, Name) VALUES (3, 'Doctorate');
                INSERT INTO AcademicDegrees (ID, Name) VALUES (4, 'Associate');
                INSERT INTO AcademicDegrees (ID, Name) VALUES (5, 'Diploma');
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM AcademicDegrees  WHERE ID IN (1, 2, 3, 4, 5);
            ");
        }
    }
}
