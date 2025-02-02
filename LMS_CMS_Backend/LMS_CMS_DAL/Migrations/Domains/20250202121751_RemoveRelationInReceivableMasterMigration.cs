using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class RemoveRelationInReceivableMasterMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceivableMaster_Banks_BankOrSaveID",
                table: "ReceivableMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceivableMaster_Saves_BankOrSaveID",
                table: "ReceivableMaster");

            migrationBuilder.DropIndex(
                name: "IX_ReceivableMaster_BankOrSaveID",
                table: "ReceivableMaster");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ReceivableMaster_BankOrSaveID",
                table: "ReceivableMaster",
                column: "BankOrSaveID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivableMaster_Banks_BankOrSaveID",
                table: "ReceivableMaster",
                column: "BankOrSaveID",
                principalTable: "Banks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivableMaster_Saves_BankOrSaveID",
                table: "ReceivableMaster",
                column: "BankOrSaveID",
                principalTable: "Saves",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
