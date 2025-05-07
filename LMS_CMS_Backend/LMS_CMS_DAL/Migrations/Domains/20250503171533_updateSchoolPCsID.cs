using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.LMS_CMS_
{
    /// <inheritdoc />
    public partial class updateSchoolPCsID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SchoolPCs",
                table: "SchoolPCs");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "SchoolPCs");

            migrationBuilder.AddColumn<long>(
                name: "ID",
                table: "SchoolPCs",
                type: "bigint",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SchoolPCs",
                table: "SchoolPCs",
                column: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ID",
                table: "SchoolPCs");
        }
    }
}
