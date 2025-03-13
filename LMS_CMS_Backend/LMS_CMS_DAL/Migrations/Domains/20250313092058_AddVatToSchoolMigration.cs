using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class AddVatToSchoolMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VatNumber",
                table: "School",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceHead",
                table: "InventoryMaster",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "QRCode",
                table: "InventoryMaster",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalWithVat",
                table: "InventoryMaster",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Vat",
                table: "InventoryMaster",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VatNumber",
                table: "School");

            migrationBuilder.DropColumn(
                name: "InvoiceHead",
                table: "InventoryMaster");

            migrationBuilder.DropColumn(
                name: "QRCode",
                table: "InventoryMaster");

            migrationBuilder.DropColumn(
                name: "TotalWithVat",
                table: "InventoryMaster");

            migrationBuilder.DropColumn(
                name: "Vat",
                table: "InventoryMaster");
        }
    }
}
