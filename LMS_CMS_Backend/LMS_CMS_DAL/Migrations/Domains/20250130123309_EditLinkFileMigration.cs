using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class EditLinkFileMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MotionTypeID",
                table: "LinkFile",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TableName",
                table: "LinkFile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LinkFile_MotionTypeID",
                table: "LinkFile",
                column: "MotionTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_LinkFile_MotionTypes_MotionTypeID",
                table: "LinkFile",
                column: "MotionTypeID",
                principalTable: "MotionTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LinkFile_MotionTypes_MotionTypeID",
                table: "LinkFile");

            migrationBuilder.DropIndex(
                name: "IX_LinkFile_MotionTypeID",
                table: "LinkFile");

            migrationBuilder.DropColumn(
                name: "MotionTypeID",
                table: "LinkFile");

            migrationBuilder.DropColumn(
                name: "TableName",
                table: "LinkFile");
        }
    }
}
