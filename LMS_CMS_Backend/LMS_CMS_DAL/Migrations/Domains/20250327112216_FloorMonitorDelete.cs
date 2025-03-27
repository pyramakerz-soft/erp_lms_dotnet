using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class FloorMonitorDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classroom_Employee_FloorMonitorID",
                table: "Classroom");

            migrationBuilder.DropIndex(
                name: "IX_Classroom_FloorMonitorID",
                table: "Classroom");

            migrationBuilder.DropColumn(
                name: "FloorMonitorID",
                table: "Classroom");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FloorMonitorID",
                table: "Classroom",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Classroom_FloorMonitorID",
                table: "Classroom",
                column: "FloorMonitorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Classroom_Employee_FloorMonitorID",
                table: "Classroom",
                column: "FloorMonitorID",
                principalTable: "Employee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
