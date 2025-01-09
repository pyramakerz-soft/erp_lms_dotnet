using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class RegisrationCategoryInsertionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO RegistrationCategory(EnName, ArName, OrderInForm) VALUES
                  ('Personal Data Of Student', N'البيانات الشخصية للطالب', 1),
                  ('Guardian’s Information', N'معلومات الوصي', 2),
                  ('Mother’s Information', N'معلومات عن الأم', 3);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM RegistrationCategory WHERE ID IN (1,2,3)");
        }
    }
}
