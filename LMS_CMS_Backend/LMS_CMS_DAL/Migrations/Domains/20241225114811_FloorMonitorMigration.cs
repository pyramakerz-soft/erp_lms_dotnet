using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class FloorMonitorMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAttachment_Employee_EmployeeID",
                table: "EmployeeAttachment");

            migrationBuilder.DropForeignKey(
                name: "FK_Floor_Employee_DeletedByUserId",
                table: "Floor");

            migrationBuilder.AddColumn<long>(
                name: "FloorMonitorID",
                table: "Floor",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subject_ar_name",
                table: "Subject",
                column: "ar_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subject_en_name",
                table: "Subject",
                column: "en_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Floor_FloorMonitorID",
                table: "Floor",
                column: "FloorMonitorID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAttachment_Employee_EmployeeID",
                table: "EmployeeAttachment",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Floor_Employee_DeletedByUserId",
                table: "Floor",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Floor_Employee_FloorMonitorID",
                table: "Floor",
                column: "FloorMonitorID",
                principalTable: "Employee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAttachment_Employee_EmployeeID",
                table: "EmployeeAttachment");

            migrationBuilder.DropForeignKey(
                name: "FK_Floor_Employee_DeletedByUserId",
                table: "Floor");

            migrationBuilder.DropForeignKey(
                name: "FK_Floor_Employee_FloorMonitorID",
                table: "Floor");

            migrationBuilder.DropIndex(
                name: "IX_Subject_ar_name",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Subject_en_name",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Floor_FloorMonitorID",
                table: "Floor");

            migrationBuilder.DropColumn(
                name: "FloorMonitorID",
                table: "Floor");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAttachment_Employee_EmployeeID",
                table: "EmployeeAttachment",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Floor_Employee_DeletedByUserId",
                table: "Floor",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");
        }
    }
}
