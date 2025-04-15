using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class OctaAccountMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword("Octa2019@pass");

            migrationBuilder.Sql($@"
                INSERT INTO Octa(User_Name, Arabic_Name, Password) 
                VALUES('Octa', N'أوكتا', '{hashedPassword}')
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Octa WHERE User_Name = 'Octa'");
        }
    }
}
