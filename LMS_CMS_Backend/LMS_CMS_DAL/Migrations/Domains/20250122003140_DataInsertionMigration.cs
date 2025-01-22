using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class DataInsertionMigration : Migration
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

            migrationBuilder.Sql(@"
                INSERT INTO AcademicDegrees (ID, Name) VALUES (1, 'Bachelor');
                INSERT INTO AcademicDegrees (ID, Name) VALUES (2, 'Master');
                INSERT INTO AcademicDegrees (ID, Name) VALUES (3, 'Doctorate');
                INSERT INTO AcademicDegrees (ID, Name) VALUES (4, 'Associate');
                INSERT INTO AcademicDegrees (ID, Name) VALUES (5, 'Diploma');
            ");

            migrationBuilder.Sql(@"
                INSERT INTO MotionTypes (ID, Name) VALUES (1, 'Debit');
                INSERT INTO MotionTypes (ID, Name) VALUES (2, 'Credit');
            ");

            migrationBuilder.Sql(@"
                INSERT INTO SubTypes (ID, Name) VALUES (1, 'Main');
                INSERT INTO SubTypes (ID, Name) VALUES (2, 'Sub');
            ");

            migrationBuilder.Sql(@"
                INSERT INTO EndTypes (ID, Name) VALUES (1, 'تشغيل');
                INSERT INTO EndTypes (ID, Name) VALUES (2, 'متاجرة');
                INSERT INTO EndTypes (ID, Name) VALUES (3, 'ارباح و خسائر');
                INSERT INTO EndTypes (ID, Name) VALUES (4, 'ميزانية عمومية');
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM EndTypes WHERE ID IN (1, 2, 3, 4);
            ");

            migrationBuilder.Sql(@"
                DELETE FROM SubTypes WHERE ID IN (1, 2);
            ");

            migrationBuilder.Sql(@"
                DELETE FROM MotionTypes  WHERE ID IN (1, 2);
            ");

            migrationBuilder.Sql(@"
                DELETE FROM AcademicDegrees  WHERE ID IN (1, 2, 3, 4, 5);
            ");

            migrationBuilder.Sql(@"
                DELETE FROM Days WHERE ID IN (1, 2, 3, 4, 5, 6, 7);
            ");
        }
    }
}
