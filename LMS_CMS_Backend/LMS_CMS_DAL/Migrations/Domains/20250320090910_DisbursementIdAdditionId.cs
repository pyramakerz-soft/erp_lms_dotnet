using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class DisbursementIdAdditionId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AdditionId",
                table: "Stocking",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DisbursementId",
                table: "Stocking",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionId",
                table: "Stocking");

            migrationBuilder.DropColumn(
                name: "DisbursementId",
                table: "Stocking");
        }
    }
}
