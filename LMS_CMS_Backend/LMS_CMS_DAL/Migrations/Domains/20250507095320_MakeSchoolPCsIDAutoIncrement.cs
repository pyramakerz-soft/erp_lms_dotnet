using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.LMS_CMS_
{
    /// <inheritdoc />
    public partial class MakeSchoolPCsIDAutoIncrement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryMaster_SchoolPCs_SchoolPCId",
                table: "InventoryMaster");

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
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SchoolPCs",
                table: "SchoolPCs",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryMaster_SchoolPCs_SchoolPCId",
                table: "InventoryMaster",
                column: "SchoolPCId",
                principalTable: "SchoolPCs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ID",
                table: "SchoolPCs",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
