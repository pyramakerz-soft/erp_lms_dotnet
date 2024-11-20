using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class ID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Schools",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BusStudent",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Bus",
                newName: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Schools",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "BusStudent",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Bus",
                newName: "Id");
        }
    }
}
