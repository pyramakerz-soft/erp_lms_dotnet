using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class Insert_Employee_Type_Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO EmployeeType (ID, Name) VALUES (1, 'Employee')");
            migrationBuilder.Sql("INSERT INTO EmployeeType (ID, Name) VALUES (2, 'Driver')");
            migrationBuilder.Sql("INSERT INTO EmployeeType (ID, Name) VALUES (3, 'Driver Assistant')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM EmployeeType WHERE ID IN (1, 2, 3)");
        }
    }
}
