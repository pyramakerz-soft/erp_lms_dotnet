using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class InsertDaysMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Days (ID, Name) VALUES (1, 'Monday');
                INSERT INTO Days (ID, Name) VALUES (2, 'Tuesday');
                INSERT INTO Days (ID, Name) VALUES (3, 'Wednesday');
                INSERT INTO Days (ID, Name) VALUES (4, 'Thursday');
                INSERT INTO Days (ID, Name) VALUES (5, 'Friday');
                INSERT INTO Days (ID, Name) VALUES (6, 'Saturday');
                INSERT INTO Days (ID, Name) VALUES (7, 'Sunday');
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"
                DELETE FROM Days WHERE ID IN (1, 2, 3, 4, 5, 6, 7);
            ");
        }
    }
}
