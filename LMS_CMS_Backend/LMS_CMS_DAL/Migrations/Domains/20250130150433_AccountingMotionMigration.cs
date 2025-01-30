using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class AccountingMotionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "EmployeeStudent",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByOctaId",
                table: "EmployeeStudent",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "EmployeeStudent",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "EmployeeStudent",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByOctaId",
                table: "EmployeeStudent",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "EmployeeStudent",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeStudent",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "EmployeeStudent",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByOctaId",
                table: "EmployeeStudent",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "EmployeeStudent",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccountingEntriesMaster",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocNumber = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountingEntriesDocTypeID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_AccountingEntriesMaster", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AccountingEntriesMaster_AccountingEntriesDocTypes_AccountingEntriesDocTypeID",
                        column: x => x.AccountingEntriesDocTypeID,
                        principalTable: "AccountingEntriesDocTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountingEntriesMaster_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AccountingEntriesMaster_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AccountingEntriesMaster_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "FeesActivation",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    Net = table.Column<int>(type: "int", nullable: false),
                    FeeTypeID = table.Column<long>(type: "bigint", nullable: false),
                    FeeDiscountTypeID = table.Column<long>(type: "bigint", nullable: false),
                    StudentID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_FeesActivation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FeesActivation_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FeesActivation_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FeesActivation_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FeesActivation_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FeesActivation_TuitionDiscountTypes_FeeDiscountTypeID",
                        column: x => x.FeeDiscountTypeID,
                        principalTable: "TuitionDiscountTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FeesActivation_TuitionFeesTypes_FeeTypeID",
                        column: x => x.FeeTypeID,
                        principalTable: "TuitionFeesTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstallmentDeductionMaster",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocNumber = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeID = table.Column<long>(type: "bigint", nullable: false),
                    StudentID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_InstallmentDeductionMaster", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InstallmentDeductionMaster_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InstallmentDeductionMaster_Employee_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InstallmentDeductionMaster_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InstallmentDeductionMaster_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InstallmentDeductionMaster_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PayableDocType",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("PK_PayableDocType", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PayableDocType_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PayableDocType_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PayableDocType_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ReceivableDocType",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("PK_ReceivableDocType", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReceivableDocType_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ReceivableDocType_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ReceivableDocType_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "AccountingEntriesDetails",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreditAmount = table.Column<int>(type: "int", nullable: false),
                    DebitAmount = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountingTreeChartID = table.Column<long>(type: "bigint", nullable: false),
                    AccountingEntriesMasterID = table.Column<long>(type: "bigint", nullable: false),
                    SubAccountingID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_AccountingEntriesDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AccountingEntriesDetails_AccountingEntriesMaster_AccountingEntriesMasterID",
                        column: x => x.AccountingEntriesMasterID,
                        principalTable: "AccountingEntriesMaster",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountingEntriesDetails_AccountingTreeCharts_AccountingTreeChartID",
                        column: x => x.AccountingTreeChartID,
                        principalTable: "AccountingTreeCharts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountingEntriesDetails_Assets_SubAccountingID",
                        column: x => x.SubAccountingID,
                        principalTable: "Assets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountingEntriesDetails_Banks_SubAccountingID",
                        column: x => x.SubAccountingID,
                        principalTable: "Banks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountingEntriesDetails_Credits_SubAccountingID",
                        column: x => x.SubAccountingID,
                        principalTable: "Credits",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountingEntriesDetails_Debits_SubAccountingID",
                        column: x => x.SubAccountingID,
                        principalTable: "Debits",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountingEntriesDetails_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AccountingEntriesDetails_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AccountingEntriesDetails_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AccountingEntriesDetails_Incomes_SubAccountingID",
                        column: x => x.SubAccountingID,
                        principalTable: "Incomes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountingEntriesDetails_Outcomes_SubAccountingID",
                        column: x => x.SubAccountingID,
                        principalTable: "Outcomes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountingEntriesDetails_Saves_SubAccountingID",
                        column: x => x.SubAccountingID,
                        principalTable: "Saves",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountingEntriesDetails_Suppliers_SubAccountingID",
                        column: x => x.SubAccountingID,
                        principalTable: "Suppliers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountingEntriesDetails_TuitionDiscountTypes_SubAccountingID",
                        column: x => x.SubAccountingID,
                        principalTable: "TuitionDiscountTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountingEntriesDetails_TuitionFeesTypes_SubAccountingID",
                        column: x => x.SubAccountingID,
                        principalTable: "TuitionFeesTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstallmentDeductionDetails",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstallmentDeductionMasterID = table.Column<long>(type: "bigint", nullable: false),
                    FeeTypeID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_InstallmentDeductionDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InstallmentDeductionDetails_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InstallmentDeductionDetails_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InstallmentDeductionDetails_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InstallmentDeductionDetails_InstallmentDeductionMaster_InstallmentDeductionMasterID",
                        column: x => x.InstallmentDeductionMasterID,
                        principalTable: "InstallmentDeductionMaster",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InstallmentDeductionDetails_TuitionFeesTypes_FeeTypeID",
                        column: x => x.FeeTypeID,
                        principalTable: "TuitionFeesTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PayableMaster",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocNumber = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PayableDocTypeID = table.Column<long>(type: "bigint", nullable: false),
                    LinkFileID = table.Column<long>(type: "bigint", nullable: false),
                    BankOrSaveID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_PayableMaster", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PayableMaster_Banks_BankOrSaveID",
                        column: x => x.BankOrSaveID,
                        principalTable: "Banks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayableMaster_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PayableMaster_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PayableMaster_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PayableMaster_LinkFile_LinkFileID",
                        column: x => x.LinkFileID,
                        principalTable: "LinkFile",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayableMaster_PayableDocType_PayableDocTypeID",
                        column: x => x.PayableDocTypeID,
                        principalTable: "PayableDocType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayableMaster_Saves_BankOrSaveID",
                        column: x => x.BankOrSaveID,
                        principalTable: "Saves",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceivableMaster",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocNumber = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceivableDocTypesID = table.Column<long>(type: "bigint", nullable: false),
                    LinkFileID = table.Column<long>(type: "bigint", nullable: false),
                    BankOrSaveID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_ReceivableMaster", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReceivableMaster_Banks_BankOrSaveID",
                        column: x => x.BankOrSaveID,
                        principalTable: "Banks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceivableMaster_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ReceivableMaster_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ReceivableMaster_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ReceivableMaster_LinkFile_LinkFileID",
                        column: x => x.LinkFileID,
                        principalTable: "LinkFile",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceivableMaster_ReceivableDocType_ReceivableDocTypesID",
                        column: x => x.ReceivableDocTypesID,
                        principalTable: "ReceivableDocType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceivableMaster_Saves_BankOrSaveID",
                        column: x => x.BankOrSaveID,
                        principalTable: "Saves",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PayableDetails",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocNumber = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PayableMasterID = table.Column<long>(type: "bigint", nullable: false),
                    LinkFileID = table.Column<long>(type: "bigint", nullable: false),
                    LinkFileTypeID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_PayableDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PayableDetails_Assets_LinkFileTypeID",
                        column: x => x.LinkFileTypeID,
                        principalTable: "Assets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayableDetails_Banks_LinkFileTypeID",
                        column: x => x.LinkFileTypeID,
                        principalTable: "Banks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayableDetails_Credits_LinkFileTypeID",
                        column: x => x.LinkFileTypeID,
                        principalTable: "Credits",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayableDetails_Debits_LinkFileTypeID",
                        column: x => x.LinkFileTypeID,
                        principalTable: "Debits",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayableDetails_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PayableDetails_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PayableDetails_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PayableDetails_Incomes_LinkFileTypeID",
                        column: x => x.LinkFileTypeID,
                        principalTable: "Incomes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayableDetails_LinkFile_LinkFileID",
                        column: x => x.LinkFileID,
                        principalTable: "LinkFile",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayableDetails_Outcomes_LinkFileTypeID",
                        column: x => x.LinkFileTypeID,
                        principalTable: "Outcomes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayableDetails_PayableMaster_PayableMasterID",
                        column: x => x.PayableMasterID,
                        principalTable: "PayableMaster",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayableDetails_Saves_LinkFileTypeID",
                        column: x => x.LinkFileTypeID,
                        principalTable: "Saves",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayableDetails_Suppliers_LinkFileTypeID",
                        column: x => x.LinkFileTypeID,
                        principalTable: "Suppliers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayableDetails_TuitionDiscountTypes_LinkFileTypeID",
                        column: x => x.LinkFileTypeID,
                        principalTable: "TuitionDiscountTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayableDetails_TuitionFeesTypes_LinkFileTypeID",
                        column: x => x.LinkFileTypeID,
                        principalTable: "TuitionFeesTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceivableDetails",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocNumber = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceivableMasterID = table.Column<long>(type: "bigint", nullable: false),
                    LinkFileID = table.Column<long>(type: "bigint", nullable: false),
                    LinkFileTypeID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_ReceivableDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReceivableDetails_Assets_LinkFileTypeID",
                        column: x => x.LinkFileTypeID,
                        principalTable: "Assets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceivableDetails_Banks_LinkFileTypeID",
                        column: x => x.LinkFileTypeID,
                        principalTable: "Banks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceivableDetails_Credits_LinkFileTypeID",
                        column: x => x.LinkFileTypeID,
                        principalTable: "Credits",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceivableDetails_Debits_LinkFileTypeID",
                        column: x => x.LinkFileTypeID,
                        principalTable: "Debits",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceivableDetails_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ReceivableDetails_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ReceivableDetails_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ReceivableDetails_Incomes_LinkFileTypeID",
                        column: x => x.LinkFileTypeID,
                        principalTable: "Incomes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceivableDetails_LinkFile_LinkFileID",
                        column: x => x.LinkFileID,
                        principalTable: "LinkFile",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceivableDetails_Outcomes_LinkFileTypeID",
                        column: x => x.LinkFileTypeID,
                        principalTable: "Outcomes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceivableDetails_ReceivableMaster_ReceivableMasterID",
                        column: x => x.ReceivableMasterID,
                        principalTable: "ReceivableMaster",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceivableDetails_Saves_LinkFileTypeID",
                        column: x => x.LinkFileTypeID,
                        principalTable: "Saves",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceivableDetails_Suppliers_LinkFileTypeID",
                        column: x => x.LinkFileTypeID,
                        principalTable: "Suppliers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceivableDetails_TuitionDiscountTypes_LinkFileTypeID",
                        column: x => x.LinkFileTypeID,
                        principalTable: "TuitionDiscountTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceivableDetails_TuitionFeesTypes_LinkFileTypeID",
                        column: x => x.LinkFileTypeID,
                        principalTable: "TuitionFeesTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeStudent_DeletedByUserId",
                table: "EmployeeStudent",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeStudent_InsertedByUserId",
                table: "EmployeeStudent",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeStudent_UpdatedByUserId",
                table: "EmployeeStudent",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingEntriesDetails_AccountingEntriesMasterID",
                table: "AccountingEntriesDetails",
                column: "AccountingEntriesMasterID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingEntriesDetails_AccountingTreeChartID",
                table: "AccountingEntriesDetails",
                column: "AccountingTreeChartID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingEntriesDetails_DeletedByUserId",
                table: "AccountingEntriesDetails",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingEntriesDetails_InsertedByUserId",
                table: "AccountingEntriesDetails",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingEntriesDetails_SubAccountingID",
                table: "AccountingEntriesDetails",
                column: "SubAccountingID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingEntriesDetails_UpdatedByUserId",
                table: "AccountingEntriesDetails",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingEntriesMaster_AccountingEntriesDocTypeID",
                table: "AccountingEntriesMaster",
                column: "AccountingEntriesDocTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingEntriesMaster_DeletedByUserId",
                table: "AccountingEntriesMaster",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingEntriesMaster_InsertedByUserId",
                table: "AccountingEntriesMaster",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingEntriesMaster_UpdatedByUserId",
                table: "AccountingEntriesMaster",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FeesActivation_DeletedByUserId",
                table: "FeesActivation",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FeesActivation_FeeDiscountTypeID",
                table: "FeesActivation",
                column: "FeeDiscountTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_FeesActivation_FeeTypeID",
                table: "FeesActivation",
                column: "FeeTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_FeesActivation_InsertedByUserId",
                table: "FeesActivation",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FeesActivation_StudentID",
                table: "FeesActivation",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_FeesActivation_UpdatedByUserId",
                table: "FeesActivation",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentDeductionDetails_DeletedByUserId",
                table: "InstallmentDeductionDetails",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentDeductionDetails_FeeTypeID",
                table: "InstallmentDeductionDetails",
                column: "FeeTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentDeductionDetails_InsertedByUserId",
                table: "InstallmentDeductionDetails",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentDeductionDetails_InstallmentDeductionMasterID",
                table: "InstallmentDeductionDetails",
                column: "InstallmentDeductionMasterID");

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentDeductionDetails_UpdatedByUserId",
                table: "InstallmentDeductionDetails",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentDeductionMaster_DeletedByUserId",
                table: "InstallmentDeductionMaster",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentDeductionMaster_EmployeeID",
                table: "InstallmentDeductionMaster",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentDeductionMaster_InsertedByUserId",
                table: "InstallmentDeductionMaster",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentDeductionMaster_StudentID",
                table: "InstallmentDeductionMaster",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentDeductionMaster_UpdatedByUserId",
                table: "InstallmentDeductionMaster",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PayableDetails_DeletedByUserId",
                table: "PayableDetails",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PayableDetails_InsertedByUserId",
                table: "PayableDetails",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PayableDetails_LinkFileID",
                table: "PayableDetails",
                column: "LinkFileID");

            migrationBuilder.CreateIndex(
                name: "IX_PayableDetails_LinkFileTypeID",
                table: "PayableDetails",
                column: "LinkFileTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PayableDetails_PayableMasterID",
                table: "PayableDetails",
                column: "PayableMasterID");

            migrationBuilder.CreateIndex(
                name: "IX_PayableDetails_UpdatedByUserId",
                table: "PayableDetails",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PayableDocType_DeletedByUserId",
                table: "PayableDocType",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PayableDocType_InsertedByUserId",
                table: "PayableDocType",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PayableDocType_UpdatedByUserId",
                table: "PayableDocType",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PayableMaster_BankOrSaveID",
                table: "PayableMaster",
                column: "BankOrSaveID");

            migrationBuilder.CreateIndex(
                name: "IX_PayableMaster_DeletedByUserId",
                table: "PayableMaster",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PayableMaster_InsertedByUserId",
                table: "PayableMaster",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PayableMaster_LinkFileID",
                table: "PayableMaster",
                column: "LinkFileID");

            migrationBuilder.CreateIndex(
                name: "IX_PayableMaster_PayableDocTypeID",
                table: "PayableMaster",
                column: "PayableDocTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PayableMaster_UpdatedByUserId",
                table: "PayableMaster",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivableDetails_DeletedByUserId",
                table: "ReceivableDetails",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivableDetails_InsertedByUserId",
                table: "ReceivableDetails",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivableDetails_LinkFileID",
                table: "ReceivableDetails",
                column: "LinkFileID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivableDetails_LinkFileTypeID",
                table: "ReceivableDetails",
                column: "LinkFileTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivableDetails_ReceivableMasterID",
                table: "ReceivableDetails",
                column: "ReceivableMasterID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivableDetails_UpdatedByUserId",
                table: "ReceivableDetails",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivableDocType_DeletedByUserId",
                table: "ReceivableDocType",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivableDocType_InsertedByUserId",
                table: "ReceivableDocType",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivableDocType_UpdatedByUserId",
                table: "ReceivableDocType",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivableMaster_BankOrSaveID",
                table: "ReceivableMaster",
                column: "BankOrSaveID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivableMaster_DeletedByUserId",
                table: "ReceivableMaster",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivableMaster_InsertedByUserId",
                table: "ReceivableMaster",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivableMaster_LinkFileID",
                table: "ReceivableMaster",
                column: "LinkFileID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivableMaster_ReceivableDocTypesID",
                table: "ReceivableMaster",
                column: "ReceivableDocTypesID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivableMaster_UpdatedByUserId",
                table: "ReceivableMaster",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeStudent_Employee_DeletedByUserId",
                table: "EmployeeStudent",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeStudent_Employee_InsertedByUserId",
                table: "EmployeeStudent",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeStudent_Employee_UpdatedByUserId",
                table: "EmployeeStudent",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeStudent_Employee_DeletedByUserId",
                table: "EmployeeStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeStudent_Employee_InsertedByUserId",
                table: "EmployeeStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeStudent_Employee_UpdatedByUserId",
                table: "EmployeeStudent");

            migrationBuilder.DropTable(
                name: "AccountingEntriesDetails");

            migrationBuilder.DropTable(
                name: "FeesActivation");

            migrationBuilder.DropTable(
                name: "InstallmentDeductionDetails");

            migrationBuilder.DropTable(
                name: "PayableDetails");

            migrationBuilder.DropTable(
                name: "ReceivableDetails");

            migrationBuilder.DropTable(
                name: "AccountingEntriesMaster");

            migrationBuilder.DropTable(
                name: "InstallmentDeductionMaster");

            migrationBuilder.DropTable(
                name: "PayableMaster");

            migrationBuilder.DropTable(
                name: "ReceivableMaster");

            migrationBuilder.DropTable(
                name: "PayableDocType");

            migrationBuilder.DropTable(
                name: "ReceivableDocType");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeStudent_DeletedByUserId",
                table: "EmployeeStudent");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeStudent_InsertedByUserId",
                table: "EmployeeStudent");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeStudent_UpdatedByUserId",
                table: "EmployeeStudent");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "EmployeeStudent");

            migrationBuilder.DropColumn(
                name: "DeletedByOctaId",
                table: "EmployeeStudent");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "EmployeeStudent");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "EmployeeStudent");

            migrationBuilder.DropColumn(
                name: "InsertedByOctaId",
                table: "EmployeeStudent");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "EmployeeStudent");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "EmployeeStudent");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "EmployeeStudent");

            migrationBuilder.DropColumn(
                name: "UpdatedByOctaId",
                table: "EmployeeStudent");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "EmployeeStudent");
        }
    }
}
