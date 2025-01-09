using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class MakeRegistrationFormMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO RegistrationForm(Name) VALUES
                  ('Registration Form');
            ");
            migrationBuilder.Sql(@"
                INSERT INTO RegistrationFormCategory(RegistrationFormID, RegistrationCategoryID) VALUES
                  (1,1),
                  (1,2),
                  (1,3);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM RegistrationForm WHERE ID IN (1)");
            migrationBuilder.Sql("DELETE FROM RegistrationFormCategory WHERE ID IN (1,2,3)");
        }
    }
}
