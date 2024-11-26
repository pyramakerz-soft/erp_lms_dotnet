using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class Create_Pyramakerz_Account : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Pyramakerz(User_Name, Password, Email) VALUES('Pyramakerz', 'Pyramakerz2019@pass', 'Pyramakerz@pyramakerz.com')
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Pyramakerz WHERE User_Name = 'Pyramakerz'");
        }
    }
}
