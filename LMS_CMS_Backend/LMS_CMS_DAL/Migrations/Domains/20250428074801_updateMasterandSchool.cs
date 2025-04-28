using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.LMS_CMS_
{
    /// <inheritdoc />
    public partial class updateMasterandSchool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DigestValue",
                table: "InventoryMaster");

            migrationBuilder.DropColumn(
                name: "InvoiceHead",
                table: "InventoryMaster");

            migrationBuilder.DropColumn(
                name: "PublicKeyCertificate",
                table: "InventoryMaster");

            migrationBuilder.RenameColumn(
                name: "Vat",
                table: "InventoryMaster",
                newName: "VatPercent");

            migrationBuilder.RenameColumn(
                name: "StampCertificate",
                table: "InventoryMaster",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "SignatureValue",
                table: "InventoryMaster",
                newName: "InvoiceHash");

            migrationBuilder.AddColumn<string>(
                name: "CRN",
                table: "School",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SchoolPCs",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchoolId = table.Column<long>(type: "bigint", nullable: false),
                    InsertedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    InsertedByOctaId = table.Column<long>(type: "bigint", nullable: true),
                    InsertedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByOctaId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletedByOctaId = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolPCs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SchoolPCs_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SchoolPCs_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SchoolPCs_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SchoolPCs_School_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "School",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchoolPCs_DeletedByUserId",
                table: "SchoolPCs",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolPCs_InsertedByUserId",
                table: "SchoolPCs",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolPCs_SchoolId",
                table: "SchoolPCs",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolPCs_UpdatedByUserId",
                table: "SchoolPCs",
                column: "UpdatedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchoolPCs");

            migrationBuilder.DropColumn(
                name: "CRN",
                table: "School");

            migrationBuilder.RenameColumn(
                name: "VatPercent",
                table: "InventoryMaster",
                newName: "Vat");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "InventoryMaster",
                newName: "StampCertificate");

            migrationBuilder.RenameColumn(
                name: "InvoiceHash",
                table: "InventoryMaster",
                newName: "SignatureValue");

            migrationBuilder.AddColumn<string>(
                name: "DigestValue",
                table: "InventoryMaster",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceHead",
                table: "InventoryMaster",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicKeyCertificate",
                table: "InventoryMaster",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
