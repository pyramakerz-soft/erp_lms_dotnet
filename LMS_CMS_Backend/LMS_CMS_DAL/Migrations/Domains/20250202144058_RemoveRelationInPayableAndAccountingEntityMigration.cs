using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class RemoveRelationInPayableAndAccountingEntityMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountingEntriesDetails_Assets_SubAccountingID",
                table: "AccountingEntriesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountingEntriesDetails_Banks_SubAccountingID",
                table: "AccountingEntriesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountingEntriesDetails_Credits_SubAccountingID",
                table: "AccountingEntriesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountingEntriesDetails_Debits_SubAccountingID",
                table: "AccountingEntriesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountingEntriesDetails_Incomes_SubAccountingID",
                table: "AccountingEntriesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountingEntriesDetails_Outcomes_SubAccountingID",
                table: "AccountingEntriesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountingEntriesDetails_Saves_SubAccountingID",
                table: "AccountingEntriesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountingEntriesDetails_Suppliers_SubAccountingID",
                table: "AccountingEntriesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountingEntriesDetails_TuitionDiscountTypes_SubAccountingID",
                table: "AccountingEntriesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountingEntriesDetails_TuitionFeesTypes_SubAccountingID",
                table: "AccountingEntriesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PayableDetails_Assets_LinkFileTypeID",
                table: "PayableDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PayableDetails_Banks_LinkFileTypeID",
                table: "PayableDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PayableDetails_Credits_LinkFileTypeID",
                table: "PayableDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PayableDetails_Debits_LinkFileTypeID",
                table: "PayableDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PayableDetails_Incomes_LinkFileTypeID",
                table: "PayableDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PayableDetails_Outcomes_LinkFileTypeID",
                table: "PayableDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PayableDetails_Saves_LinkFileTypeID",
                table: "PayableDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PayableDetails_Suppliers_LinkFileTypeID",
                table: "PayableDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PayableDetails_TuitionDiscountTypes_LinkFileTypeID",
                table: "PayableDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PayableDetails_TuitionFeesTypes_LinkFileTypeID",
                table: "PayableDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PayableMaster_Banks_BankOrSaveID",
                table: "PayableMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_PayableMaster_Saves_BankOrSaveID",
                table: "PayableMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceivableDetails_Assets_LinkFileTypeID",
                table: "ReceivableDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceivableDetails_Banks_LinkFileTypeID",
                table: "ReceivableDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceivableDetails_Credits_LinkFileTypeID",
                table: "ReceivableDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceivableDetails_Debits_LinkFileTypeID",
                table: "ReceivableDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceivableDetails_Incomes_LinkFileTypeID",
                table: "ReceivableDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceivableDetails_Outcomes_LinkFileTypeID",
                table: "ReceivableDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceivableDetails_Saves_LinkFileTypeID",
                table: "ReceivableDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceivableDetails_Suppliers_LinkFileTypeID",
                table: "ReceivableDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceivableDetails_TuitionDiscountTypes_LinkFileTypeID",
                table: "ReceivableDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceivableDetails_TuitionFeesTypes_LinkFileTypeID",
                table: "ReceivableDetails");

            migrationBuilder.DropIndex(
                name: "IX_ReceivableDetails_LinkFileTypeID",
                table: "ReceivableDetails");

            migrationBuilder.DropIndex(
                name: "IX_PayableMaster_BankOrSaveID",
                table: "PayableMaster");

            migrationBuilder.DropIndex(
                name: "IX_PayableDetails_LinkFileTypeID",
                table: "PayableDetails");

            migrationBuilder.DropIndex(
                name: "IX_AccountingEntriesDetails_SubAccountingID",
                table: "AccountingEntriesDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ReceivableDetails_LinkFileTypeID",
                table: "ReceivableDetails",
                column: "LinkFileTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PayableMaster_BankOrSaveID",
                table: "PayableMaster",
                column: "BankOrSaveID");

            migrationBuilder.CreateIndex(
                name: "IX_PayableDetails_LinkFileTypeID",
                table: "PayableDetails",
                column: "LinkFileTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingEntriesDetails_SubAccountingID",
                table: "AccountingEntriesDetails",
                column: "SubAccountingID");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingEntriesDetails_Assets_SubAccountingID",
                table: "AccountingEntriesDetails",
                column: "SubAccountingID",
                principalTable: "Assets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingEntriesDetails_Banks_SubAccountingID",
                table: "AccountingEntriesDetails",
                column: "SubAccountingID",
                principalTable: "Banks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingEntriesDetails_Credits_SubAccountingID",
                table: "AccountingEntriesDetails",
                column: "SubAccountingID",
                principalTable: "Credits",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingEntriesDetails_Debits_SubAccountingID",
                table: "AccountingEntriesDetails",
                column: "SubAccountingID",
                principalTable: "Debits",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingEntriesDetails_Incomes_SubAccountingID",
                table: "AccountingEntriesDetails",
                column: "SubAccountingID",
                principalTable: "Incomes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingEntriesDetails_Outcomes_SubAccountingID",
                table: "AccountingEntriesDetails",
                column: "SubAccountingID",
                principalTable: "Outcomes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingEntriesDetails_Saves_SubAccountingID",
                table: "AccountingEntriesDetails",
                column: "SubAccountingID",
                principalTable: "Saves",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingEntriesDetails_Suppliers_SubAccountingID",
                table: "AccountingEntriesDetails",
                column: "SubAccountingID",
                principalTable: "Suppliers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingEntriesDetails_TuitionDiscountTypes_SubAccountingID",
                table: "AccountingEntriesDetails",
                column: "SubAccountingID",
                principalTable: "TuitionDiscountTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingEntriesDetails_TuitionFeesTypes_SubAccountingID",
                table: "AccountingEntriesDetails",
                column: "SubAccountingID",
                principalTable: "TuitionFeesTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PayableDetails_Assets_LinkFileTypeID",
                table: "PayableDetails",
                column: "LinkFileTypeID",
                principalTable: "Assets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PayableDetails_Banks_LinkFileTypeID",
                table: "PayableDetails",
                column: "LinkFileTypeID",
                principalTable: "Banks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PayableDetails_Credits_LinkFileTypeID",
                table: "PayableDetails",
                column: "LinkFileTypeID",
                principalTable: "Credits",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PayableDetails_Debits_LinkFileTypeID",
                table: "PayableDetails",
                column: "LinkFileTypeID",
                principalTable: "Debits",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PayableDetails_Incomes_LinkFileTypeID",
                table: "PayableDetails",
                column: "LinkFileTypeID",
                principalTable: "Incomes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PayableDetails_Outcomes_LinkFileTypeID",
                table: "PayableDetails",
                column: "LinkFileTypeID",
                principalTable: "Outcomes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PayableDetails_Saves_LinkFileTypeID",
                table: "PayableDetails",
                column: "LinkFileTypeID",
                principalTable: "Saves",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PayableDetails_Suppliers_LinkFileTypeID",
                table: "PayableDetails",
                column: "LinkFileTypeID",
                principalTable: "Suppliers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PayableDetails_TuitionDiscountTypes_LinkFileTypeID",
                table: "PayableDetails",
                column: "LinkFileTypeID",
                principalTable: "TuitionDiscountTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PayableDetails_TuitionFeesTypes_LinkFileTypeID",
                table: "PayableDetails",
                column: "LinkFileTypeID",
                principalTable: "TuitionFeesTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PayableMaster_Banks_BankOrSaveID",
                table: "PayableMaster",
                column: "BankOrSaveID",
                principalTable: "Banks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PayableMaster_Saves_BankOrSaveID",
                table: "PayableMaster",
                column: "BankOrSaveID",
                principalTable: "Saves",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivableDetails_Assets_LinkFileTypeID",
                table: "ReceivableDetails",
                column: "LinkFileTypeID",
                principalTable: "Assets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivableDetails_Banks_LinkFileTypeID",
                table: "ReceivableDetails",
                column: "LinkFileTypeID",
                principalTable: "Banks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivableDetails_Credits_LinkFileTypeID",
                table: "ReceivableDetails",
                column: "LinkFileTypeID",
                principalTable: "Credits",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivableDetails_Debits_LinkFileTypeID",
                table: "ReceivableDetails",
                column: "LinkFileTypeID",
                principalTable: "Debits",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivableDetails_Incomes_LinkFileTypeID",
                table: "ReceivableDetails",
                column: "LinkFileTypeID",
                principalTable: "Incomes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivableDetails_Outcomes_LinkFileTypeID",
                table: "ReceivableDetails",
                column: "LinkFileTypeID",
                principalTable: "Outcomes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivableDetails_Saves_LinkFileTypeID",
                table: "ReceivableDetails",
                column: "LinkFileTypeID",
                principalTable: "Saves",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivableDetails_Suppliers_LinkFileTypeID",
                table: "ReceivableDetails",
                column: "LinkFileTypeID",
                principalTable: "Suppliers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivableDetails_TuitionDiscountTypes_LinkFileTypeID",
                table: "ReceivableDetails",
                column: "LinkFileTypeID",
                principalTable: "TuitionDiscountTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivableDetails_TuitionFeesTypes_LinkFileTypeID",
                table: "ReceivableDetails",
                column: "LinkFileTypeID",
                principalTable: "TuitionFeesTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
