using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class UpdateCatrgoryFieldMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE CategoryField
                SET FieldTypeID = 7
                WHERE ID IN (3, 5, 6, 7, 8, 9, 14);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE CategoryField
                SET FieldTypeID = 4
                WHERE ID IN (3, 5, 6, 7, 8, 9, 14);
            ");
        }
    }
}
