using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class CreateDomainDBMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicDegrees",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicDegrees", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeType",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EndTypes",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FieldType",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "InterViewState",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterViewState", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "InventoryFlags",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    enName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    arName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    en_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ar_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemInOut = table.Column<int>(type: "int", nullable: false),
                    FlagValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryFlags", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MotionTypes",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotionTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OrderState",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderState", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Page",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    en_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ar_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDisplay = table.Column<bool>(type: "bit", nullable: false),
                    Page_ID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Page_Page_Page_ID",
                        column: x => x.Page_ID,
                        principalTable: "Page",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionType",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RegisterationFormState",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterationFormState", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SchoolType",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SubTypes",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TestState",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestState", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LinkFile",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ArName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotionTypeID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkFile", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LinkFile_MotionTypes_MotionTypeID",
                        column: x => x.MotionTypeID,
                        principalTable: "MotionTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AcademicYear",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateFrom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SchoolID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_AcademicYear", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AccountingEntriesDetails",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreditAmount = table.Column<int>(type: "int", nullable: false),
                    DebitAmount = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountingTreeChartID = table.Column<long>(type: "bigint", nullable: false),
                    AccountingEntriesMasterID = table.Column<long>(type: "bigint", nullable: false),
                    SubAccountingID = table.Column<long>(type: "bigint", nullable: true),
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
                });

            migrationBuilder.CreateTable(
                name: "AccountingEntriesDocTypes",
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
                    table.PrimaryKey("PK_AccountingEntriesDocTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AccountingEntriesMaster",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocNumber = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                });

            migrationBuilder.CreateTable(
                name: "AccountingTreeCharts",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    SubTypeID = table.Column<long>(type: "bigint", nullable: false),
                    EndTypeID = table.Column<long>(type: "bigint", nullable: false),
                    MainAccountNumberID = table.Column<long>(type: "bigint", nullable: true),
                    LinkFileID = table.Column<long>(type: "bigint", nullable: true),
                    MotionTypeID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_AccountingTreeCharts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AccountingTreeCharts_AccountingTreeCharts_MainAccountNumberID",
                        column: x => x.MainAccountNumberID,
                        principalTable: "AccountingTreeCharts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountingTreeCharts_EndTypes_EndTypeID",
                        column: x => x.EndTypeID,
                        principalTable: "EndTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountingTreeCharts_LinkFile_LinkFileID",
                        column: x => x.LinkFileID,
                        principalTable: "LinkFile",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountingTreeCharts_MotionTypes_MotionTypeID",
                        column: x => x.MotionTypeID,
                        principalTable: "MotionTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountingTreeCharts_SubTypes_SubTypeID",
                        column: x => x.SubTypeID,
                        principalTable: "SubTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AccountNumberID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Assets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Assets_AccountingTreeCharts_AccountNumberID",
                        column: x => x.AccountNumberID,
                        principalTable: "AccountingTreeCharts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BankAccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IBAN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankAccountNumber = table.Column<int>(type: "int", nullable: false),
                    AccountOpeningDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountClosingDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumberID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Banks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Banks_AccountingTreeCharts_AccountNumberID",
                        column: x => x.AccountNumberID,
                        principalTable: "AccountingTreeCharts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SchoolID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Building", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Bus",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Capacity = table.Column<long>(type: "bigint", nullable: false),
                    IsCapacityRestricted = table.Column<bool>(type: "bit", nullable: false),
                    MorningPrice = table.Column<int>(type: "int", nullable: false),
                    BackPrice = table.Column<int>(type: "int", nullable: false),
                    TwoWaysPrice = table.Column<int>(type: "int", nullable: false),
                    BusTypeID = table.Column<long>(type: "bigint", nullable: true),
                    BusDistrictID = table.Column<long>(type: "bigint", nullable: true),
                    BusStatusID = table.Column<long>(type: "bigint", nullable: true),
                    DriverID = table.Column<long>(type: "bigint", nullable: true),
                    DriverAssistantID = table.Column<long>(type: "bigint", nullable: true),
                    BusCompanyID = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_Bus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BusCategory",
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
                    table.PrimaryKey("PK_BusCategory", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BusCompany",
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
                    table.PrimaryKey("PK_BusCompany", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BusDistrict",
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
                    table.PrimaryKey("PK_BusDistrict", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BusStatus",
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
                    table.PrimaryKey("PK_BusStatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BusStudent",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusID = table.Column<long>(type: "bigint", nullable: false),
                    StudentID = table.Column<long>(type: "bigint", nullable: false),
                    BusCategoryID = table.Column<long>(type: "bigint", nullable: false),
                    SemseterID = table.Column<long>(type: "bigint", nullable: false),
                    IsException = table.Column<bool>(type: "bit", nullable: false),
                    ExceptionFromDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExceptionToDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_BusStudent", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BusStudent_BusCategory_BusCategoryID",
                        column: x => x.BusCategoryID,
                        principalTable: "BusCategory",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BusStudent_Bus_BusID",
                        column: x => x.BusID,
                        principalTable: "Bus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BusType",
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
                    table.PrimaryKey("PK_BusType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalPrice = table.Column<float>(type: "real", nullable: false),
                    StudentID = table.Column<long>(type: "bigint", nullable: false),
                    OrderStateID = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_Cart", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cart_OrderState_OrderStateID",
                        column: x => x.OrderStateID,
                        principalTable: "OrderState",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Cart_ShopItem",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ShopItemID = table.Column<long>(type: "bigint", nullable: false),
                    CartID = table.Column<long>(type: "bigint", nullable: false),
                    ShopItemSizeID = table.Column<long>(type: "bigint", nullable: true),
                    ShopItemColorID = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_Cart_ShopItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cart_ShopItem_Cart_CartID",
                        column: x => x.CartID,
                        principalTable: "Cart",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryField",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ArName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OrderInForm = table.Column<int>(type: "int", nullable: false),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    RegistrationCategoryID = table.Column<long>(type: "bigint", nullable: false),
                    FieldTypeID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_CategoryField", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CategoryField_FieldType_FieldTypeID",
                        column: x => x.FieldTypeID,
                        principalTable: "FieldType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Classroom",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    FloorID = table.Column<long>(type: "bigint", nullable: false),
                    HomeroomTeacherID = table.Column<long>(type: "bigint", nullable: true),
                    GradeID = table.Column<long>(type: "bigint", nullable: false),
                    AcademicYearID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Classroom", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Classroom_AcademicYear_AcademicYearID",
                        column: x => x.AcademicYearID,
                        principalTable: "AcademicYear",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Credits",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AccountNumberID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Credits", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Credits_AccountingTreeCharts_AccountNumberID",
                        column: x => x.AccountNumberID,
                        principalTable: "AccountingTreeCharts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Debits",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AccountNumberID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Debits", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Debits_AccountingTreeCharts_AccountNumberID",
                        column: x => x.AccountNumberID,
                        principalTable: "AccountingTreeCharts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
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
                    table.PrimaryKey("PK_Departments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Diagnoses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Diagnoses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dose",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoseTimes = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Dose", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drugs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Drugs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    en_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ar_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LicenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpireDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResidenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthdayDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<long>(type: "bigint", nullable: true),
                    DateOfAppointment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfLeavingWork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthSalary = table.Column<int>(type: "int", nullable: true),
                    HasAttendance = table.Column<bool>(type: "bit", nullable: true),
                    AttendanceTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartureTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DelayAllowance = table.Column<float>(type: "real", nullable: true),
                    AnnualLeaveBalance = table.Column<int>(type: "int", nullable: true),
                    CasualLeavesBalance = table.Column<int>(type: "int", nullable: true),
                    MonthlyLeaveRequestBalance = table.Column<int>(type: "int", nullable: true),
                    GraduationYear = table.Column<int>(type: "int", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CanReceiveRequest = table.Column<bool>(type: "bit", nullable: true),
                    CanReceiveMessage = table.Column<bool>(type: "bit", nullable: true),
                    Role_ID = table.Column<long>(type: "bigint", nullable: false),
                    BusCompanyID = table.Column<long>(type: "bigint", nullable: true),
                    EmployeeTypeID = table.Column<long>(type: "bigint", nullable: false),
                    ReasonOfLeavingID = table.Column<long>(type: "bigint", nullable: true),
                    AccountNumberID = table.Column<long>(type: "bigint", nullable: true),
                    DepartmentID = table.Column<long>(type: "bigint", nullable: true),
                    JobID = table.Column<long>(type: "bigint", nullable: true),
                    AcademicDegreeID = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_Employee", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employee_AcademicDegrees_AcademicDegreeID",
                        column: x => x.AcademicDegreeID,
                        principalTable: "AcademicDegrees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employee_AccountingTreeCharts_AccountNumberID",
                        column: x => x.AccountNumberID,
                        principalTable: "AccountingTreeCharts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employee_BusCompany_BusCompanyID",
                        column: x => x.BusCompanyID,
                        principalTable: "BusCompany",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employee_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employee_EmployeeType_EmployeeTypeID",
                        column: x => x.EmployeeTypeID,
                        principalTable: "EmployeeType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employee_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Employee_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Employee_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAttachment",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_EmployeeAttachment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EmployeeAttachment_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EmployeeAttachment_Employee_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeAttachment_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EmployeeAttachment_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDays",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<long>(type: "bigint", nullable: false),
                    DayID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_EmployeeDays", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EmployeeDays_Days_DayID",
                        column: x => x.DayID,
                        principalTable: "Days",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeDays_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeDays_Employee_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeDays_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EmployeeDays_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "FieldOption",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CategoryFieldID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_FieldOption", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FieldOption_CategoryField_CategoryFieldID",
                        column: x => x.CategoryFieldID,
                        principalTable: "CategoryField",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FieldOption_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FieldOption_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FieldOption_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Floor",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    buildingID = table.Column<long>(type: "bigint", nullable: false),
                    FloorMonitorID = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_Floor", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Floor_Building_buildingID",
                        column: x => x.buildingID,
                        principalTable: "Building",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Floor_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Floor_Employee_FloorMonitorID",
                        column: x => x.FloorMonitorID,
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Floor_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Floor_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "HygieneTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_HygieneTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HygieneTypes_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_HygieneTypes_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_HygieneTypes_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Incomes",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AccountNumberID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Incomes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Incomes_AccountingTreeCharts_AccountNumberID",
                        column: x => x.AccountNumberID,
                        principalTable: "AccountingTreeCharts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Incomes_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Incomes_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Incomes_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "InterviewTime",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Reserved = table.Column<int>(type: "int", nullable: false),
                    AcademicYearID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_InterviewTime", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InterviewTime_AcademicYear_AcademicYearID",
                        column: x => x.AcademicYearID,
                        principalTable: "AcademicYear",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InterviewTime_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InterviewTime_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InterviewTime_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "InventoryCategories",
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
                    table.PrimaryKey("PK_InventoryCategories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InventoryCategories_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InventoryCategories_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InventoryCategories_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "JobCategories",
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
                    table.PrimaryKey("PK_JobCategories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_JobCategories_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_JobCategories_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_JobCategories_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Outcomes",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AccountNumberID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Outcomes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Outcomes_AccountingTreeCharts_AccountNumberID",
                        column: x => x.AccountNumberID,
                        principalTable: "AccountingTreeCharts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Outcomes_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Outcomes_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Outcomes_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Parent",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    en_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ar_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConfirmationCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportNoExpiredDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalIDExpiredDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qualification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Profession = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Parent", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Parent_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Parent_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Parent_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
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
                name: "ReasonsForLeavingWork",
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
                    table.PrimaryKey("PK_ReasonsForLeavingWork", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReasonsForLeavingWork_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ReasonsForLeavingWork_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ReasonsForLeavingWork_Employee_UpdatedByUserId",
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
                name: "RegistrationCategory",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ArName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OrderInForm = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_RegistrationCategory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RegistrationCategory_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegistrationCategory_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegistrationCategory_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "RegistrationForm",
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
                    table.PrimaryKey("PK_RegistrationForm", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RegistrationForm_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegistrationForm_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegistrationForm_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Role",
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
                    table.PrimaryKey("PK_Role", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Role_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Role_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Role_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Saves",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AccountNumberID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Saves", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Saves_AccountingTreeCharts_AccountNumberID",
                        column: x => x.AccountNumberID,
                        principalTable: "AccountingTreeCharts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Saves_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Saves_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Saves_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "School",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SchoolTypeID = table.Column<long>(type: "bigint", nullable: false),
                    ReportHeaderOneEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportHeaderOneAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportHeaderTwoEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportHeaderTwoAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VatNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_School", x => x.ID);
                    table.ForeignKey(
                        name: "FK_School_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_School_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_School_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_School_SchoolType_SchoolTypeID",
                        column: x => x.SchoolTypeID,
                        principalTable: "SchoolType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Semester",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateFrom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: true),
                    AcademicYearID = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_Semester", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Semester_AcademicYear_AcademicYearID",
                        column: x => x.AcademicYearID,
                        principalTable: "AcademicYear",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Semester_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Semester_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Semester_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Store",
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
                    table.PrimaryKey("PK_Store", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Store_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Store_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Store_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "SubjectCategory",
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
                    table.PrimaryKey("PK_SubjectCategory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubjectCategory_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SubjectCategory_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SubjectCategory_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryID = table.Column<long>(type: "bigint", nullable: false),
                    CommercialRegister = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxCard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumberID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Suppliers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Suppliers_AccountingTreeCharts_AccountNumberID",
                        column: x => x.AccountNumberID,
                        principalTable: "AccountingTreeCharts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Suppliers_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Suppliers_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Suppliers_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TuitionDiscountTypes",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AccountNumberID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_TuitionDiscountTypes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TuitionDiscountTypes_AccountingTreeCharts_AccountNumberID",
                        column: x => x.AccountNumberID,
                        principalTable: "AccountingTreeCharts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TuitionDiscountTypes_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TuitionDiscountTypes_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TuitionDiscountTypes_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TuitionFeesTypes",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AccountNumberID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_TuitionFeesTypes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TuitionFeesTypes_AccountingTreeCharts_AccountNumberID",
                        column: x => x.AccountNumberID,
                        principalTable: "AccountingTreeCharts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TuitionFeesTypes_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TuitionFeesTypes_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TuitionFeesTypes_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Violation",
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
                    table.PrimaryKey("PK_Violation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Violation_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Violation_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Violation_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "InventorySubCategories",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InventoryCategoriesID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_InventorySubCategories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InventorySubCategories_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InventorySubCategories_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InventorySubCategories_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InventorySubCategories_InventoryCategories_InventoryCategoriesID",
                        column: x => x.InventoryCategoriesID,
                        principalTable: "InventoryCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    JobCategoryID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Jobs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Jobs_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Jobs_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Jobs_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Jobs_JobCategories_JobCategoryID",
                        column: x => x.JobCategoryID,
                        principalTable: "JobCategories",
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
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                });

            migrationBuilder.CreateTable(
                name: "ReceivableMaster",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocNumber = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                });

            migrationBuilder.CreateTable(
                name: "RegisterationFormParent",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GradeID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcademicYearID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterationFormStateID = table.Column<long>(type: "bigint", nullable: true),
                    ParentID = table.Column<long>(type: "bigint", nullable: true),
                    RegistrationFormID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_RegisterationFormParent", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RegisterationFormParent_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegisterationFormParent_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegisterationFormParent_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegisterationFormParent_Parent_ParentID",
                        column: x => x.ParentID,
                        principalTable: "Parent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegisterationFormParent_RegisterationFormState_RegisterationFormStateID",
                        column: x => x.RegisterationFormStateID,
                        principalTable: "RegisterationFormState",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegisterationFormParent_RegistrationForm_RegistrationFormID",
                        column: x => x.RegistrationFormID,
                        principalTable: "RegistrationForm",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationFormCategory",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationFormID = table.Column<long>(type: "bigint", nullable: false),
                    RegistrationCategoryID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_RegistrationFormCategory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RegistrationFormCategory_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegistrationFormCategory_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegistrationFormCategory_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegistrationFormCategory_RegistrationCategory_RegistrationCategoryID",
                        column: x => x.RegistrationCategoryID,
                        principalTable: "RegistrationCategory",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegistrationFormCategory_RegistrationForm_RegistrationFormID",
                        column: x => x.RegistrationFormID,
                        principalTable: "RegistrationForm",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Role_Detailes",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Allow_Edit = table.Column<bool>(type: "bit", nullable: false),
                    Allow_Delete = table.Column<bool>(type: "bit", nullable: false),
                    Allow_Edit_For_Others = table.Column<bool>(type: "bit", nullable: false),
                    Allow_Delete_For_Others = table.Column<bool>(type: "bit", nullable: false),
                    Role_ID = table.Column<long>(type: "bigint", nullable: false),
                    Page_ID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Role_Detailes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Role_Detailes_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Role_Detailes_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Role_Detailes_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Role_Detailes_Page_Page_ID",
                        column: x => x.Page_ID,
                        principalTable: "Page",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Role_Detailes_Role_Role_ID",
                        column: x => x.Role_ID,
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Section",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SchoolID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Section", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Section_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Section_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Section_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Section_School_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "School",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stocking",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdditionId = table.Column<long>(type: "bigint", nullable: true),
                    DisbursementId = table.Column<long>(type: "bigint", nullable: true),
                    StoreID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Stocking", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Stocking_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Stocking_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Stocking_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Stocking_Store_StoreID",
                        column: x => x.StoreID,
                        principalTable: "Store",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreCategories",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventoryCategoriesID = table.Column<long>(type: "bigint", nullable: false),
                    StoreID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_StoreCategories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StoreCategories_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_StoreCategories_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_StoreCategories_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_StoreCategories_InventoryCategories_InventoryCategoriesID",
                        column: x => x.InventoryCategoriesID,
                        principalTable: "InventoryCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreCategories_Store_StoreID",
                        column: x => x.StoreID,
                        principalTable: "Store",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTypeViolation",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeTypeID = table.Column<long>(type: "bigint", nullable: true),
                    ViolationID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_EmployeeTypeViolation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EmployeeTypeViolation_EmployeeType_EmployeeTypeID",
                        column: x => x.EmployeeTypeID,
                        principalTable: "EmployeeType",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EmployeeTypeViolation_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EmployeeTypeViolation_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EmployeeTypeViolation_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EmployeeTypeViolation_Violation_ViolationID",
                        column: x => x.ViolationID,
                        principalTable: "Violation",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PayableDetails",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                        name: "FK_PayableDetails_LinkFile_LinkFileID",
                        column: x => x.LinkFileID,
                        principalTable: "LinkFile",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayableDetails_PayableMaster_PayableMasterID",
                        column: x => x.PayableMasterID,
                        principalTable: "PayableMaster",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceivableDetails",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                        name: "FK_ReceivableDetails_LinkFile_LinkFileID",
                        column: x => x.LinkFileID,
                        principalTable: "LinkFile",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceivableDetails_ReceivableMaster_ReceivableMasterID",
                        column: x => x.ReceivableMasterID,
                        principalTable: "ReceivableMaster",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegisterationFormInterview",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterviewStateID = table.Column<long>(type: "bigint", nullable: true),
                    RegisterationFormParentID = table.Column<long>(type: "bigint", nullable: false),
                    InterviewTimeID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_RegisterationFormInterview", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RegisterationFormInterview_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegisterationFormInterview_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegisterationFormInterview_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegisterationFormInterview_InterViewState_InterviewStateID",
                        column: x => x.InterviewStateID,
                        principalTable: "InterViewState",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegisterationFormInterview_InterviewTime_InterviewTimeID",
                        column: x => x.InterviewTimeID,
                        principalTable: "InterviewTime",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegisterationFormInterview_RegisterationFormParent_RegisterationFormParentID",
                        column: x => x.RegisterationFormParentID,
                        principalTable: "RegisterationFormParent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegisterationFormSubmittion",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TextAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegisterationFormParentID = table.Column<long>(type: "bigint", nullable: false),
                    CategoryFieldID = table.Column<long>(type: "bigint", nullable: false),
                    SelectedFieldOptionID = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_RegisterationFormSubmittion", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RegisterationFormSubmittion_CategoryField_CategoryFieldID",
                        column: x => x.CategoryFieldID,
                        principalTable: "CategoryField",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegisterationFormSubmittion_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegisterationFormSubmittion_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegisterationFormSubmittion_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegisterationFormSubmittion_FieldOption_SelectedFieldOptionID",
                        column: x => x.SelectedFieldOptionID,
                        principalTable: "FieldOption",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegisterationFormSubmittion_RegisterationFormParent_RegisterationFormParentID",
                        column: x => x.RegisterationFormParentID,
                        principalTable: "RegisterationFormParent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    en_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ar_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalIDExpiredDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<long>(type: "bigint", nullable: true),
                    Parent_Id = table.Column<long>(type: "bigint", nullable: true),
                    AccountNumberID = table.Column<long>(type: "bigint", nullable: true),
                    GenderId = table.Column<long>(type: "bigint", nullable: false),
                    RegistrationFormParentID = table.Column<long>(type: "bigint", nullable: true),
                    DateOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Religion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportExpiredDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousSchool = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmergencyContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmergencyContactRelation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmergencyContactMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickUpContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickUpContactRelation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickUpContactMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRegisteredInNoor = table.Column<bool>(type: "bit", nullable: true),
                    MotherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherPassportNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherPassportExpireDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherNationalID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherNationalIDExpiredDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherQualification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherWorkPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherExperiences = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherProfession = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuardianRelation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdmissionDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Student", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Student_AccountingTreeCharts_AccountNumberID",
                        column: x => x.AccountNumberID,
                        principalTable: "AccountingTreeCharts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Student_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Student_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Student_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Student_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Student_Parent_Parent_Id",
                        column: x => x.Parent_Id,
                        principalTable: "Parent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Student_RegisterationFormParent_RegistrationFormParentID",
                        column: x => x.RegistrationFormParentID,
                        principalTable: "RegisterationFormParent",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateFrom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SectionID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Grade", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Grade_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Grade_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Grade_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Grade_Section_SectionID",
                        column: x => x.SectionID,
                        principalTable: "Section",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeStudent",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<long>(type: "bigint", nullable: false),
                    EmployeeID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_EmployeeStudent", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EmployeeStudent_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeStudent_Employee_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeStudent_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EmployeeStudent_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EmployeeStudent_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FeesActivation",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    Discount = table.Column<float>(type: "real", nullable: false),
                    Net = table.Column<float>(type: "real", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeeTypeID = table.Column<long>(type: "bigint", nullable: false),
                    FeeDiscountTypeID = table.Column<long>(type: "bigint", nullable: true),
                    StudentID = table.Column<long>(type: "bigint", nullable: false),
                    AcademicYearId = table.Column<long>(type: "bigint", nullable: true),
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
                        name: "FK_FeesActivation_AcademicYear_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYear",
                        principalColumn: "ID");
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
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "InventoryMaster",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceHead = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceNumber = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCash = table.Column<bool>(type: "bit", nullable: false),
                    IsVisa = table.Column<bool>(type: "bit", nullable: false),
                    CashAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VisaAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Remaining = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Vat = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalWithVat = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DigestValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignatureValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublicKeyCertificate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StampCertificate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    XmlInvoiceFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QRCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attachments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreID = table.Column<long>(type: "bigint", nullable: false),
                    StudentID = table.Column<long>(type: "bigint", nullable: true),
                    SaveID = table.Column<long>(type: "bigint", nullable: true),
                    BankID = table.Column<long>(type: "bigint", nullable: true),
                    FlagId = table.Column<long>(type: "bigint", nullable: false),
                    SupplierId = table.Column<long>(type: "bigint", nullable: true),
                    StoreToTransformId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_InventoryMaster", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InventoryMaster_Banks_BankID",
                        column: x => x.BankID,
                        principalTable: "Banks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryMaster_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InventoryMaster_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InventoryMaster_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InventoryMaster_InventoryFlags_FlagId",
                        column: x => x.FlagId,
                        principalTable: "InventoryFlags",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryMaster_Saves_SaveID",
                        column: x => x.SaveID,
                        principalTable: "Saves",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryMaster_Store_StoreID",
                        column: x => x.StoreID,
                        principalTable: "Store",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryMaster_Store_StoreToTransformId",
                        column: x => x.StoreToTransformId,
                        principalTable: "Store",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryMaster_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryMaster_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalPrice = table.Column<float>(type: "real", nullable: false),
                    OrderStateID = table.Column<long>(type: "bigint", nullable: false),
                    StudentID = table.Column<long>(type: "bigint", nullable: false),
                    CartID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Order", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Order_Cart_CartID",
                        column: x => x.CartID,
                        principalTable: "Cart",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Order_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Order_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Order_OrderState_OrderStateID",
                        column: x => x.OrderStateID,
                        principalTable: "OrderState",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FollowUps",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolId = table.Column<long>(type: "bigint", nullable: false),
                    GradeId = table.Column<long>(type: "bigint", nullable: false),
                    ClassroomId = table.Column<long>(type: "bigint", nullable: false),
                    StudentId = table.Column<long>(type: "bigint", nullable: false),
                    Complains = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosisId = table.Column<long>(type: "bigint", nullable: false),
                    Recommendation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendSMS = table.Column<bool>(type: "bit", nullable: true),
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
                    table.PrimaryKey("PK_FollowUps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FollowUps_Classroom_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classroom",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FollowUps_Diagnoses_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "Diagnoses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FollowUps_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FollowUps_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FollowUps_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FollowUps_Grade_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grade",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FollowUps_School_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "School",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FollowUps_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HygieneForms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolId = table.Column<long>(type: "bigint", nullable: false),
                    GradeId = table.Column<long>(type: "bigint", nullable: false),
                    ClassRoomID = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_HygieneForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HygieneForms_Classroom_ClassRoomID",
                        column: x => x.ClassRoomID,
                        principalTable: "Classroom",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HygieneForms_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_HygieneForms_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_HygieneForms_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_HygieneForms_Grade_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grade",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HygieneForms_School_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "School",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalHistories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolId = table.Column<long>(type: "bigint", nullable: true),
                    GradeId = table.Column<long>(type: "bigint", nullable: true),
                    ClassRoomID = table.Column<long>(type: "bigint", nullable: true),
                    StudentId = table.Column<long>(type: "bigint", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstReport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecReport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attached = table.Column<int>(type: "int", nullable: true),
                    PermanentDrug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    table.PrimaryKey("PK_MedicalHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalHistories_Classroom_ClassRoomID",
                        column: x => x.ClassRoomID,
                        principalTable: "Classroom",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_MedicalHistories_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_MedicalHistories_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_MedicalHistories_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_MedicalHistories_Grade_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grade",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_MedicalHistories_School_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "School",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_MedicalHistories_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ShopItem",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ArName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EnDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchasePrice = table.Column<float>(type: "real", nullable: false),
                    SalesPrice = table.Column<float>(type: "real", nullable: false),
                    VATForForeign = table.Column<float>(type: "real", nullable: true),
                    Limit = table.Column<int>(type: "int", nullable: false),
                    AvailableInShop = table.Column<bool>(type: "bit", nullable: false),
                    MainImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BarCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GenderID = table.Column<long>(type: "bigint", nullable: true),
                    InventorySubCategoriesID = table.Column<long>(type: "bigint", nullable: false),
                    SchoolID = table.Column<long>(type: "bigint", nullable: false),
                    GradeID = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_ShopItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ShopItem_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ShopItem_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ShopItem_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ShopItem_Gender_GenderID",
                        column: x => x.GenderID,
                        principalTable: "Gender",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShopItem_Grade_GradeID",
                        column: x => x.GradeID,
                        principalTable: "Grade",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShopItem_InventorySubCategories_InventorySubCategoriesID",
                        column: x => x.InventorySubCategoriesID,
                        principalTable: "InventorySubCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShopItem_School_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "School",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentAcademicYear",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<long>(type: "bigint", nullable: false),
                    SchoolID = table.Column<long>(type: "bigint", nullable: false),
                    ClassID = table.Column<long>(type: "bigint", nullable: false),
                    GradeID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_StudentAcademicYear", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StudentAcademicYear_Classroom_ClassID",
                        column: x => x.ClassID,
                        principalTable: "Classroom",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentAcademicYear_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_StudentAcademicYear_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_StudentAcademicYear_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_StudentAcademicYear_Grade_GradeID",
                        column: x => x.GradeID,
                        principalTable: "Grade",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentAcademicYear_School_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "School",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentAcademicYear_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    en_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ar_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OrderInCertificate = table.Column<int>(type: "int", nullable: false),
                    CreditHours = table.Column<double>(type: "float", nullable: false),
                    SubjectCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassByDegree = table.Column<int>(type: "int", nullable: false),
                    TotalMark = table.Column<int>(type: "int", nullable: false),
                    HideFromGradeReport = table.Column<bool>(type: "bit", nullable: false),
                    IconLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfSessionPerWeek = table.Column<int>(type: "int", nullable: false),
                    GradeID = table.Column<long>(type: "bigint", nullable: false),
                    SubjectCategoryID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Subject", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Subject_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Subject_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Subject_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Subject_Grade_GradeID",
                        column: x => x.GradeID,
                        principalTable: "Grade",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subject_SubjectCategory_SubjectCategoryID",
                        column: x => x.SubjectCategoryID,
                        principalTable: "SubjectCategory",
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
                name: "FollowUpDrugs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FollowUpId = table.Column<long>(type: "bigint", nullable: false),
                    DrugId = table.Column<long>(type: "bigint", nullable: false),
                    DoseId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_FollowUpDrugs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FollowUpDrugs_Dose_DoseId",
                        column: x => x.DoseId,
                        principalTable: "Dose",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FollowUpDrugs_Drugs_DrugId",
                        column: x => x.DrugId,
                        principalTable: "Drugs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FollowUpDrugs_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FollowUpDrugs_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FollowUpDrugs_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FollowUpDrugs_FollowUps_FollowUpId",
                        column: x => x.FollowUpId,
                        principalTable: "FollowUps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentHygieneTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HygieneFormId = table.Column<long>(type: "bigint", nullable: false),
                    StudentId = table.Column<long>(type: "bigint", nullable: false),
                    Attendance = table.Column<bool>(type: "bit", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionTaken = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_StudentHygieneTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentHygieneTypes_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_StudentHygieneTypes_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_StudentHygieneTypes_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_StudentHygieneTypes_HygieneForms_HygieneFormId",
                        column: x => x.HygieneFormId,
                        principalTable: "HygieneForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentHygieneTypes_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryDetails",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    TotalPrice = table.Column<float>(type: "real", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShopItemID = table.Column<long>(type: "bigint", nullable: false),
                    InventoryMasterId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_InventoryDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InventoryDetails_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InventoryDetails_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InventoryDetails_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InventoryDetails_InventoryMaster_InventoryMasterId",
                        column: x => x.InventoryMasterId,
                        principalTable: "InventoryMaster",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryDetails_ShopItem_ShopItemID",
                        column: x => x.ShopItemID,
                        principalTable: "ShopItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShopItemColor",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShopItemID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_ShopItemColor", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ShopItemColor_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ShopItemColor_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ShopItemColor_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ShopItemColor_ShopItem_ShopItemID",
                        column: x => x.ShopItemID,
                        principalTable: "ShopItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShopItemSize",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShopItemID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_ShopItemSize", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ShopItemSize_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ShopItemSize_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ShopItemSize_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ShopItemSize_ShopItem_ShopItemID",
                        column: x => x.ShopItemID,
                        principalTable: "ShopItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockingDetails",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentStock = table.Column<long>(type: "bigint", nullable: false),
                    ActualStock = table.Column<long>(type: "bigint", nullable: false),
                    TheDifference = table.Column<long>(type: "bigint", nullable: false),
                    ShopItemID = table.Column<long>(type: "bigint", nullable: false),
                    StockingId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_StockingDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StockingDetails_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_StockingDetails_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_StockingDetails_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_StockingDetails_ShopItem_ShopItemID",
                        column: x => x.ShopItemID,
                        principalTable: "ShopItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockingDetails_Stocking_StockingId",
                        column: x => x.StockingId,
                        principalTable: "Stocking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalMark = table.Column<double>(type: "float", nullable: false),
                    AcademicYearID = table.Column<long>(type: "bigint", nullable: false),
                    SubjectID = table.Column<long>(type: "bigint", nullable: false),
                    GradeID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Test", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Test_AcademicYear_AcademicYearID",
                        column: x => x.AcademicYearID,
                        principalTable: "AcademicYear",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Test_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Test_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Test_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Test_Grade_GradeID",
                        column: x => x.GradeID,
                        principalTable: "Grade",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Test_Subject_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subject",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HygieneTypeStudentHygieneTypes",
                columns: table => new
                {
                    HygieneTypesId = table.Column<long>(type: "bigint", nullable: false),
                    StudentHygieneTypesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HygieneTypeStudentHygieneTypes", x => new { x.HygieneTypesId, x.StudentHygieneTypesId });
                    table.ForeignKey(
                        name: "FK_HygieneTypeStudentHygieneTypes_HygieneTypes_HygieneTypesId",
                        column: x => x.HygieneTypesId,
                        principalTable: "HygieneTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HygieneTypeStudentHygieneTypes_StudentHygieneTypes_StudentHygieneTypesId",
                        column: x => x.StudentHygieneTypesId,
                        principalTable: "StudentHygieneTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesItemAttachment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InventoryDetailsID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_SalesItemAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesItemAttachment_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SalesItemAttachment_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SalesItemAttachment_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SalesItemAttachment_InventoryDetails_InventoryDetailsID",
                        column: x => x.InventoryDetailsID,
                        principalTable: "InventoryDetails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegisterationFormTest",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mark = table.Column<double>(type: "float", nullable: false),
                    VisibleToParent = table.Column<bool>(type: "bit", nullable: false),
                    TestID = table.Column<long>(type: "bigint", nullable: false),
                    StateID = table.Column<long>(type: "bigint", nullable: false),
                    RegisterationFormParentID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_RegisterationFormTest", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RegisterationFormTest_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegisterationFormTest_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegisterationFormTest_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegisterationFormTest_RegisterationFormParent_RegisterationFormParentID",
                        column: x => x.RegisterationFormParentID,
                        principalTable: "RegisterationFormParent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegisterationFormTest_TestState_StateID",
                        column: x => x.StateID,
                        principalTable: "TestState",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegisterationFormTest_Test_TestID",
                        column: x => x.TestID,
                        principalTable: "Test",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MCQQuestionOption",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Question_ID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_MCQQuestionOption", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MCQQuestionOption_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_MCQQuestionOption_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_MCQQuestionOption_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Video = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorrectAnswerID = table.Column<long>(type: "bigint", nullable: true),
                    QuestionTypeID = table.Column<long>(type: "bigint", nullable: false),
                    TestID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Question", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Question_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Question_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Question_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Question_MCQQuestionOption_CorrectAnswerID",
                        column: x => x.CorrectAnswerID,
                        principalTable: "MCQQuestionOption",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Question_QuestionType_QuestionTypeID",
                        column: x => x.QuestionTypeID,
                        principalTable: "QuestionType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Question_Test_TestID",
                        column: x => x.TestID,
                        principalTable: "Test",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegisterationFormTestAnswer",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EssayAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionID = table.Column<long>(type: "bigint", nullable: false),
                    AnswerID = table.Column<long>(type: "bigint", nullable: true),
                    RegisterationFormParentID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_RegisterationFormTestAnswer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RegisterationFormTestAnswer_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegisterationFormTestAnswer_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegisterationFormTestAnswer_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegisterationFormTestAnswer_MCQQuestionOption_AnswerID",
                        column: x => x.AnswerID,
                        principalTable: "MCQQuestionOption",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegisterationFormTestAnswer_Question_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Question",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegisterationFormTestAnswer_RegisterationFormParent_RegisterationFormParentID",
                        column: x => x.RegisterationFormParentID,
                        principalTable: "RegisterationFormParent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicYear_DeletedByUserId",
                table: "AcademicYear",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicYear_InsertedByUserId",
                table: "AcademicYear",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicYear_SchoolID",
                table: "AcademicYear",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicYear_UpdatedByUserId",
                table: "AcademicYear",
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
                name: "IX_AccountingEntriesDetails_UpdatedByUserId",
                table: "AccountingEntriesDetails",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingEntriesDocTypes_DeletedByUserId",
                table: "AccountingEntriesDocTypes",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingEntriesDocTypes_InsertedByUserId",
                table: "AccountingEntriesDocTypes",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingEntriesDocTypes_UpdatedByUserId",
                table: "AccountingEntriesDocTypes",
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
                name: "IX_AccountingTreeCharts_DeletedByUserId",
                table: "AccountingTreeCharts",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingTreeCharts_EndTypeID",
                table: "AccountingTreeCharts",
                column: "EndTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingTreeCharts_InsertedByUserId",
                table: "AccountingTreeCharts",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingTreeCharts_LinkFileID",
                table: "AccountingTreeCharts",
                column: "LinkFileID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingTreeCharts_MainAccountNumberID",
                table: "AccountingTreeCharts",
                column: "MainAccountNumberID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingTreeCharts_MotionTypeID",
                table: "AccountingTreeCharts",
                column: "MotionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingTreeCharts_SubTypeID",
                table: "AccountingTreeCharts",
                column: "SubTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingTreeCharts_UpdatedByUserId",
                table: "AccountingTreeCharts",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_AccountNumberID",
                table: "Assets",
                column: "AccountNumberID");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_DeletedByUserId",
                table: "Assets",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_InsertedByUserId",
                table: "Assets",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_UpdatedByUserId",
                table: "Assets",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_AccountNumberID",
                table: "Banks",
                column: "AccountNumberID");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_DeletedByUserId",
                table: "Banks",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_InsertedByUserId",
                table: "Banks",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_UpdatedByUserId",
                table: "Banks",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Building_DeletedByUserId",
                table: "Building",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Building_InsertedByUserId",
                table: "Building",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Building_SchoolID",
                table: "Building",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_Building_UpdatedByUserId",
                table: "Building",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_BusCompanyID",
                table: "Bus",
                column: "BusCompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_BusDistrictID",
                table: "Bus",
                column: "BusDistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_BusStatusID",
                table: "Bus",
                column: "BusStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_BusTypeID",
                table: "Bus",
                column: "BusTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_DeletedByUserId",
                table: "Bus",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_DriverAssistantID",
                table: "Bus",
                column: "DriverAssistantID");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_DriverID",
                table: "Bus",
                column: "DriverID");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_InsertedByUserId",
                table: "Bus",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_UpdatedByUserId",
                table: "Bus",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusCategory_DeletedByUserId",
                table: "BusCategory",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusCategory_InsertedByUserId",
                table: "BusCategory",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusCategory_UpdatedByUserId",
                table: "BusCategory",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusCompany_DeletedByUserId",
                table: "BusCompany",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusCompany_InsertedByUserId",
                table: "BusCompany",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusCompany_UpdatedByUserId",
                table: "BusCompany",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusDistrict_DeletedByUserId",
                table: "BusDistrict",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusDistrict_InsertedByUserId",
                table: "BusDistrict",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusDistrict_UpdatedByUserId",
                table: "BusDistrict",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStatus_DeletedByUserId",
                table: "BusStatus",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStatus_InsertedByUserId",
                table: "BusStatus",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStatus_UpdatedByUserId",
                table: "BusStatus",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStudent_BusCategoryID",
                table: "BusStudent",
                column: "BusCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_BusStudent_BusID",
                table: "BusStudent",
                column: "BusID");

            migrationBuilder.CreateIndex(
                name: "IX_BusStudent_DeletedByUserId",
                table: "BusStudent",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStudent_InsertedByUserId",
                table: "BusStudent",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStudent_SemseterID",
                table: "BusStudent",
                column: "SemseterID");

            migrationBuilder.CreateIndex(
                name: "IX_BusStudent_StudentID",
                table: "BusStudent",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_BusStudent_UpdatedByUserId",
                table: "BusStudent",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusType_DeletedByUserId",
                table: "BusType",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusType_InsertedByUserId",
                table: "BusType",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusType_UpdatedByUserId",
                table: "BusType",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_DeletedByUserId",
                table: "Cart",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_InsertedByUserId",
                table: "Cart",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_OrderStateID",
                table: "Cart",
                column: "OrderStateID");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_StudentID",
                table: "Cart",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UpdatedByUserId",
                table: "Cart",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_ShopItem_CartID",
                table: "Cart_ShopItem",
                column: "CartID");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_ShopItem_DeletedByUserId",
                table: "Cart_ShopItem",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_ShopItem_InsertedByUserId",
                table: "Cart_ShopItem",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_ShopItem_ShopItemColorID",
                table: "Cart_ShopItem",
                column: "ShopItemColorID");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_ShopItem_ShopItemID",
                table: "Cart_ShopItem",
                column: "ShopItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_ShopItem_ShopItemSizeID",
                table: "Cart_ShopItem",
                column: "ShopItemSizeID");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_ShopItem_UpdatedByUserId",
                table: "Cart_ShopItem",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryField_DeletedByUserId",
                table: "CategoryField",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryField_FieldTypeID",
                table: "CategoryField",
                column: "FieldTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryField_InsertedByUserId",
                table: "CategoryField",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryField_RegistrationCategoryID",
                table: "CategoryField",
                column: "RegistrationCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryField_UpdatedByUserId",
                table: "CategoryField",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Classroom_AcademicYearID",
                table: "Classroom",
                column: "AcademicYearID");

            migrationBuilder.CreateIndex(
                name: "IX_Classroom_DeletedByUserId",
                table: "Classroom",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Classroom_FloorID",
                table: "Classroom",
                column: "FloorID");

            migrationBuilder.CreateIndex(
                name: "IX_Classroom_GradeID",
                table: "Classroom",
                column: "GradeID");

            migrationBuilder.CreateIndex(
                name: "IX_Classroom_HomeroomTeacherID",
                table: "Classroom",
                column: "HomeroomTeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_Classroom_InsertedByUserId",
                table: "Classroom",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Classroom_UpdatedByUserId",
                table: "Classroom",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Credits_AccountNumberID",
                table: "Credits",
                column: "AccountNumberID");

            migrationBuilder.CreateIndex(
                name: "IX_Credits_DeletedByUserId",
                table: "Credits",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Credits_InsertedByUserId",
                table: "Credits",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Credits_UpdatedByUserId",
                table: "Credits",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Debits_AccountNumberID",
                table: "Debits",
                column: "AccountNumberID");

            migrationBuilder.CreateIndex(
                name: "IX_Debits_DeletedByUserId",
                table: "Debits",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Debits_InsertedByUserId",
                table: "Debits",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Debits_UpdatedByUserId",
                table: "Debits",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_DeletedByUserId",
                table: "Departments",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_InsertedByUserId",
                table: "Departments",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_UpdatedByUserId",
                table: "Departments",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnoses_DeletedByUserId",
                table: "Diagnoses",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnoses_InsertedByUserId",
                table: "Diagnoses",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnoses_UpdatedByUserId",
                table: "Diagnoses",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Dose_DeletedByUserId",
                table: "Dose",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Dose_InsertedByUserId",
                table: "Dose",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Dose_UpdatedByUserId",
                table: "Dose",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Drugs_DeletedByUserId",
                table: "Drugs",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Drugs_InsertedByUserId",
                table: "Drugs",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Drugs_UpdatedByUserId",
                table: "Drugs",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_AcademicDegreeID",
                table: "Employee",
                column: "AcademicDegreeID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_AccountNumberID",
                table: "Employee",
                column: "AccountNumberID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_BusCompanyID",
                table: "Employee",
                column: "BusCompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DeletedByUserId",
                table: "Employee",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DepartmentID",
                table: "Employee",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Email",
                table: "Employee",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_EmployeeTypeID",
                table: "Employee",
                column: "EmployeeTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_InsertedByUserId",
                table: "Employee",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_JobID",
                table: "Employee",
                column: "JobID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ReasonOfLeavingID",
                table: "Employee",
                column: "ReasonOfLeavingID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Role_ID",
                table: "Employee",
                column: "Role_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_UpdatedByUserId",
                table: "Employee",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_User_Name",
                table: "Employee",
                column: "User_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttachment_DeletedByUserId",
                table: "EmployeeAttachment",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttachment_EmployeeID",
                table: "EmployeeAttachment",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttachment_InsertedByUserId",
                table: "EmployeeAttachment",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttachment_UpdatedByUserId",
                table: "EmployeeAttachment",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDays_DayID",
                table: "EmployeeDays",
                column: "DayID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDays_DeletedByUserId",
                table: "EmployeeDays",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDays_EmployeeID",
                table: "EmployeeDays",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDays_InsertedByUserId",
                table: "EmployeeDays",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDays_UpdatedByUserId",
                table: "EmployeeDays",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeStudent_DeletedByUserId",
                table: "EmployeeStudent",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeStudent_EmployeeID",
                table: "EmployeeStudent",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeStudent_InsertedByUserId",
                table: "EmployeeStudent",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeStudent_StudentID",
                table: "EmployeeStudent",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeStudent_UpdatedByUserId",
                table: "EmployeeStudent",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeType_Name",
                table: "EmployeeType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTypeViolation_DeletedByUserId",
                table: "EmployeeTypeViolation",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTypeViolation_EmployeeTypeID",
                table: "EmployeeTypeViolation",
                column: "EmployeeTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTypeViolation_InsertedByUserId",
                table: "EmployeeTypeViolation",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTypeViolation_UpdatedByUserId",
                table: "EmployeeTypeViolation",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTypeViolation_ViolationID",
                table: "EmployeeTypeViolation",
                column: "ViolationID");

            migrationBuilder.CreateIndex(
                name: "IX_FeesActivation_AcademicYearId",
                table: "FeesActivation",
                column: "AcademicYearId");

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
                name: "IX_FieldOption_CategoryFieldID",
                table: "FieldOption",
                column: "CategoryFieldID");

            migrationBuilder.CreateIndex(
                name: "IX_FieldOption_DeletedByUserId",
                table: "FieldOption",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldOption_InsertedByUserId",
                table: "FieldOption",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldOption_UpdatedByUserId",
                table: "FieldOption",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Floor_buildingID",
                table: "Floor",
                column: "buildingID");

            migrationBuilder.CreateIndex(
                name: "IX_Floor_DeletedByUserId",
                table: "Floor",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Floor_FloorMonitorID",
                table: "Floor",
                column: "FloorMonitorID");

            migrationBuilder.CreateIndex(
                name: "IX_Floor_InsertedByUserId",
                table: "Floor",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Floor_UpdatedByUserId",
                table: "Floor",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUpDrugs_DeletedByUserId",
                table: "FollowUpDrugs",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUpDrugs_DoseId",
                table: "FollowUpDrugs",
                column: "DoseId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUpDrugs_DrugId",
                table: "FollowUpDrugs",
                column: "DrugId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUpDrugs_FollowUpId",
                table: "FollowUpDrugs",
                column: "FollowUpId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUpDrugs_InsertedByUserId",
                table: "FollowUpDrugs",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUpDrugs_UpdatedByUserId",
                table: "FollowUpDrugs",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUps_ClassroomId",
                table: "FollowUps",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUps_DeletedByUserId",
                table: "FollowUps",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUps_DiagnosisId",
                table: "FollowUps",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUps_GradeId",
                table: "FollowUps",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUps_InsertedByUserId",
                table: "FollowUps",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUps_SchoolId",
                table: "FollowUps",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUps_StudentId",
                table: "FollowUps",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUps_UpdatedByUserId",
                table: "FollowUps",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_DeletedByUserId",
                table: "Grade",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_InsertedByUserId",
                table: "Grade",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_SectionID",
                table: "Grade",
                column: "SectionID");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_UpdatedByUserId",
                table: "Grade",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HygieneForms_ClassRoomID",
                table: "HygieneForms",
                column: "ClassRoomID");

            migrationBuilder.CreateIndex(
                name: "IX_HygieneForms_DeletedByUserId",
                table: "HygieneForms",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HygieneForms_GradeId",
                table: "HygieneForms",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_HygieneForms_InsertedByUserId",
                table: "HygieneForms",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HygieneForms_SchoolId",
                table: "HygieneForms",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_HygieneForms_UpdatedByUserId",
                table: "HygieneForms",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HygieneTypes_DeletedByUserId",
                table: "HygieneTypes",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HygieneTypes_InsertedByUserId",
                table: "HygieneTypes",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HygieneTypes_UpdatedByUserId",
                table: "HygieneTypes",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HygieneTypeStudentHygieneTypes_StudentHygieneTypesId",
                table: "HygieneTypeStudentHygieneTypes",
                column: "StudentHygieneTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_AccountNumberID",
                table: "Incomes",
                column: "AccountNumberID");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_DeletedByUserId",
                table: "Incomes",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_InsertedByUserId",
                table: "Incomes",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_UpdatedByUserId",
                table: "Incomes",
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
                name: "IX_InterViewState_Name",
                table: "InterViewState",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTime_AcademicYearID",
                table: "InterviewTime",
                column: "AcademicYearID");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTime_DeletedByUserId",
                table: "InterviewTime",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTime_InsertedByUserId",
                table: "InterviewTime",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTime_UpdatedByUserId",
                table: "InterviewTime",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryCategories_DeletedByUserId",
                table: "InventoryCategories",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryCategories_InsertedByUserId",
                table: "InventoryCategories",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryCategories_UpdatedByUserId",
                table: "InventoryCategories",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDetails_DeletedByUserId",
                table: "InventoryDetails",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDetails_InsertedByUserId",
                table: "InventoryDetails",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDetails_InventoryMasterId",
                table: "InventoryDetails",
                column: "InventoryMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDetails_ShopItemID",
                table: "InventoryDetails",
                column: "ShopItemID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDetails_UpdatedByUserId",
                table: "InventoryDetails",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMaster_BankID",
                table: "InventoryMaster",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMaster_DeletedByUserId",
                table: "InventoryMaster",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMaster_FlagId",
                table: "InventoryMaster",
                column: "FlagId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMaster_InsertedByUserId",
                table: "InventoryMaster",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMaster_SaveID",
                table: "InventoryMaster",
                column: "SaveID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMaster_StoreID",
                table: "InventoryMaster",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMaster_StoreToTransformId",
                table: "InventoryMaster",
                column: "StoreToTransformId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMaster_StudentID",
                table: "InventoryMaster",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMaster_SupplierId",
                table: "InventoryMaster",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMaster_UpdatedByUserId",
                table: "InventoryMaster",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventorySubCategories_DeletedByUserId",
                table: "InventorySubCategories",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventorySubCategories_InsertedByUserId",
                table: "InventorySubCategories",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventorySubCategories_InventoryCategoriesID",
                table: "InventorySubCategories",
                column: "InventoryCategoriesID");

            migrationBuilder.CreateIndex(
                name: "IX_InventorySubCategories_UpdatedByUserId",
                table: "InventorySubCategories",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JobCategories_DeletedByUserId",
                table: "JobCategories",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JobCategories_InsertedByUserId",
                table: "JobCategories",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JobCategories_UpdatedByUserId",
                table: "JobCategories",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_DeletedByUserId",
                table: "Jobs",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_InsertedByUserId",
                table: "Jobs",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobCategoryID",
                table: "Jobs",
                column: "JobCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_UpdatedByUserId",
                table: "Jobs",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkFile_MotionTypeID",
                table: "LinkFile",
                column: "MotionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_MCQQuestionOption_DeletedByUserId",
                table: "MCQQuestionOption",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MCQQuestionOption_InsertedByUserId",
                table: "MCQQuestionOption",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MCQQuestionOption_Question_ID",
                table: "MCQQuestionOption",
                column: "Question_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MCQQuestionOption_UpdatedByUserId",
                table: "MCQQuestionOption",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistories_ClassRoomID",
                table: "MedicalHistories",
                column: "ClassRoomID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistories_DeletedByUserId",
                table: "MedicalHistories",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistories_GradeId",
                table: "MedicalHistories",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistories_InsertedByUserId",
                table: "MedicalHistories",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistories_SchoolId",
                table: "MedicalHistories",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistories_StudentId",
                table: "MedicalHistories",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistories_UpdatedByUserId",
                table: "MedicalHistories",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CartID",
                table: "Order",
                column: "CartID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_DeletedByUserId",
                table: "Order",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_InsertedByUserId",
                table: "Order",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderStateID",
                table: "Order",
                column: "OrderStateID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_StudentID",
                table: "Order",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UpdatedByUserId",
                table: "Order",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderState_Name",
                table: "OrderState",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Outcomes_AccountNumberID",
                table: "Outcomes",
                column: "AccountNumberID");

            migrationBuilder.CreateIndex(
                name: "IX_Outcomes_DeletedByUserId",
                table: "Outcomes",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Outcomes_InsertedByUserId",
                table: "Outcomes",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Outcomes_UpdatedByUserId",
                table: "Outcomes",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Page_Page_ID",
                table: "Page",
                column: "Page_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Parent_DeletedByUserId",
                table: "Parent",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Parent_Email",
                table: "Parent",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parent_InsertedByUserId",
                table: "Parent",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Parent_UpdatedByUserId",
                table: "Parent",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Parent_User_Name",
                table: "Parent",
                column: "User_Name",
                unique: true);

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
                name: "IX_Question_CorrectAnswerID",
                table: "Question",
                column: "CorrectAnswerID");

            migrationBuilder.CreateIndex(
                name: "IX_Question_DeletedByUserId",
                table: "Question",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_InsertedByUserId",
                table: "Question",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_QuestionTypeID",
                table: "Question",
                column: "QuestionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Question_TestID",
                table: "Question",
                column: "TestID");

            migrationBuilder.CreateIndex(
                name: "IX_Question_UpdatedByUserId",
                table: "Question",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReasonsForLeavingWork_DeletedByUserId",
                table: "ReasonsForLeavingWork",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReasonsForLeavingWork_InsertedByUserId",
                table: "ReasonsForLeavingWork",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReasonsForLeavingWork_UpdatedByUserId",
                table: "ReasonsForLeavingWork",
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

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormInterview_DeletedByUserId",
                table: "RegisterationFormInterview",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormInterview_InsertedByUserId",
                table: "RegisterationFormInterview",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormInterview_InterviewStateID",
                table: "RegisterationFormInterview",
                column: "InterviewStateID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormInterview_InterviewTimeID",
                table: "RegisterationFormInterview",
                column: "InterviewTimeID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormInterview_RegisterationFormParentID",
                table: "RegisterationFormInterview",
                column: "RegisterationFormParentID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormInterview_UpdatedByUserId",
                table: "RegisterationFormInterview",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormParent_DeletedByUserId",
                table: "RegisterationFormParent",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormParent_InsertedByUserId",
                table: "RegisterationFormParent",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormParent_ParentID",
                table: "RegisterationFormParent",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormParent_RegisterationFormStateID",
                table: "RegisterationFormParent",
                column: "RegisterationFormStateID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormParent_RegistrationFormID",
                table: "RegisterationFormParent",
                column: "RegistrationFormID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormParent_UpdatedByUserId",
                table: "RegisterationFormParent",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormState_Name",
                table: "RegisterationFormState",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormSubmittion_CategoryFieldID",
                table: "RegisterationFormSubmittion",
                column: "CategoryFieldID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormSubmittion_DeletedByUserId",
                table: "RegisterationFormSubmittion",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormSubmittion_InsertedByUserId",
                table: "RegisterationFormSubmittion",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormSubmittion_RegisterationFormParentID",
                table: "RegisterationFormSubmittion",
                column: "RegisterationFormParentID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormSubmittion_SelectedFieldOptionID",
                table: "RegisterationFormSubmittion",
                column: "SelectedFieldOptionID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormSubmittion_UpdatedByUserId",
                table: "RegisterationFormSubmittion",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormTest_DeletedByUserId",
                table: "RegisterationFormTest",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormTest_InsertedByUserId",
                table: "RegisterationFormTest",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormTest_RegisterationFormParentID",
                table: "RegisterationFormTest",
                column: "RegisterationFormParentID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormTest_StateID",
                table: "RegisterationFormTest",
                column: "StateID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormTest_TestID",
                table: "RegisterationFormTest",
                column: "TestID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormTest_UpdatedByUserId",
                table: "RegisterationFormTest",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormTestAnswer_AnswerID",
                table: "RegisterationFormTestAnswer",
                column: "AnswerID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormTestAnswer_DeletedByUserId",
                table: "RegisterationFormTestAnswer",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormTestAnswer_InsertedByUserId",
                table: "RegisterationFormTestAnswer",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormTestAnswer_QuestionID",
                table: "RegisterationFormTestAnswer",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormTestAnswer_RegisterationFormParentID",
                table: "RegisterationFormTestAnswer",
                column: "RegisterationFormParentID");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterationFormTestAnswer_UpdatedByUserId",
                table: "RegisterationFormTestAnswer",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationCategory_DeletedByUserId",
                table: "RegistrationCategory",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationCategory_InsertedByUserId",
                table: "RegistrationCategory",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationCategory_UpdatedByUserId",
                table: "RegistrationCategory",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationForm_DeletedByUserId",
                table: "RegistrationForm",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationForm_InsertedByUserId",
                table: "RegistrationForm",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationForm_UpdatedByUserId",
                table: "RegistrationForm",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationFormCategory_DeletedByUserId",
                table: "RegistrationFormCategory",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationFormCategory_InsertedByUserId",
                table: "RegistrationFormCategory",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationFormCategory_RegistrationCategoryID",
                table: "RegistrationFormCategory",
                column: "RegistrationCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationFormCategory_RegistrationFormID",
                table: "RegistrationFormCategory",
                column: "RegistrationFormID");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationFormCategory_UpdatedByUserId",
                table: "RegistrationFormCategory",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_DeletedByUserId",
                table: "Role",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_InsertedByUserId",
                table: "Role",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Name",
                table: "Role",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_UpdatedByUserId",
                table: "Role",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Detailes_DeletedByUserId",
                table: "Role_Detailes",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Detailes_InsertedByUserId",
                table: "Role_Detailes",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Detailes_Page_ID",
                table: "Role_Detailes",
                column: "Page_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Detailes_Role_ID",
                table: "Role_Detailes",
                column: "Role_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Detailes_UpdatedByUserId",
                table: "Role_Detailes",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesItemAttachment_DeletedByUserId",
                table: "SalesItemAttachment",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesItemAttachment_InsertedByUserId",
                table: "SalesItemAttachment",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesItemAttachment_InventoryDetailsID",
                table: "SalesItemAttachment",
                column: "InventoryDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesItemAttachment_UpdatedByUserId",
                table: "SalesItemAttachment",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Saves_AccountNumberID",
                table: "Saves",
                column: "AccountNumberID");

            migrationBuilder.CreateIndex(
                name: "IX_Saves_DeletedByUserId",
                table: "Saves",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Saves_InsertedByUserId",
                table: "Saves",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Saves_UpdatedByUserId",
                table: "Saves",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_School_DeletedByUserId",
                table: "School",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_School_InsertedByUserId",
                table: "School",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_School_SchoolTypeID",
                table: "School",
                column: "SchoolTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_School_UpdatedByUserId",
                table: "School",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Section_DeletedByUserId",
                table: "Section",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Section_InsertedByUserId",
                table: "Section",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Section_SchoolID",
                table: "Section",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_Section_UpdatedByUserId",
                table: "Section",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Semester_AcademicYearID",
                table: "Semester",
                column: "AcademicYearID");

            migrationBuilder.CreateIndex(
                name: "IX_Semester_DeletedByUserId",
                table: "Semester",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Semester_InsertedByUserId",
                table: "Semester",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Semester_UpdatedByUserId",
                table: "Semester",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItem_BarCode",
                table: "ShopItem",
                column: "BarCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShopItem_DeletedByUserId",
                table: "ShopItem",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItem_GenderID",
                table: "ShopItem",
                column: "GenderID");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItem_GradeID",
                table: "ShopItem",
                column: "GradeID");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItem_InsertedByUserId",
                table: "ShopItem",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItem_InventorySubCategoriesID",
                table: "ShopItem",
                column: "InventorySubCategoriesID");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItem_SchoolID",
                table: "ShopItem",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItem_UpdatedByUserId",
                table: "ShopItem",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItemColor_DeletedByUserId",
                table: "ShopItemColor",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItemColor_InsertedByUserId",
                table: "ShopItemColor",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItemColor_ShopItemID",
                table: "ShopItemColor",
                column: "ShopItemID");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItemColor_UpdatedByUserId",
                table: "ShopItemColor",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItemSize_DeletedByUserId",
                table: "ShopItemSize",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItemSize_InsertedByUserId",
                table: "ShopItemSize",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItemSize_ShopItemID",
                table: "ShopItemSize",
                column: "ShopItemID");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItemSize_UpdatedByUserId",
                table: "ShopItemSize",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocking_DeletedByUserId",
                table: "Stocking",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocking_InsertedByUserId",
                table: "Stocking",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocking_StoreID",
                table: "Stocking",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_Stocking_UpdatedByUserId",
                table: "Stocking",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StockingDetails_DeletedByUserId",
                table: "StockingDetails",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StockingDetails_InsertedByUserId",
                table: "StockingDetails",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StockingDetails_ShopItemID",
                table: "StockingDetails",
                column: "ShopItemID");

            migrationBuilder.CreateIndex(
                name: "IX_StockingDetails_StockingId",
                table: "StockingDetails",
                column: "StockingId");

            migrationBuilder.CreateIndex(
                name: "IX_StockingDetails_UpdatedByUserId",
                table: "StockingDetails",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Store_DeletedByUserId",
                table: "Store",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Store_InsertedByUserId",
                table: "Store",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Store_UpdatedByUserId",
                table: "Store",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreCategories_DeletedByUserId",
                table: "StoreCategories",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreCategories_InsertedByUserId",
                table: "StoreCategories",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreCategories_InventoryCategoriesID",
                table: "StoreCategories",
                column: "InventoryCategoriesID");

            migrationBuilder.CreateIndex(
                name: "IX_StoreCategories_StoreID",
                table: "StoreCategories",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_StoreCategories_UpdatedByUserId",
                table: "StoreCategories",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_AccountNumberID",
                table: "Student",
                column: "AccountNumberID");

            migrationBuilder.CreateIndex(
                name: "IX_Student_DeletedByUserId",
                table: "Student",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Email",
                table: "Student",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Student_GenderId",
                table: "Student",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_InsertedByUserId",
                table: "Student",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Parent_Id",
                table: "Student",
                column: "Parent_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_RegistrationFormParentID",
                table: "Student",
                column: "RegistrationFormParentID");

            migrationBuilder.CreateIndex(
                name: "IX_Student_UpdatedByUserId",
                table: "Student",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_User_Name",
                table: "Student",
                column: "User_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentAcademicYear_ClassID",
                table: "StudentAcademicYear",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAcademicYear_DeletedByUserId",
                table: "StudentAcademicYear",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAcademicYear_GradeID",
                table: "StudentAcademicYear",
                column: "GradeID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAcademicYear_InsertedByUserId",
                table: "StudentAcademicYear",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAcademicYear_SchoolID",
                table: "StudentAcademicYear",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAcademicYear_StudentID",
                table: "StudentAcademicYear",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAcademicYear_UpdatedByUserId",
                table: "StudentAcademicYear",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentHygieneTypes_DeletedByUserId",
                table: "StudentHygieneTypes",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentHygieneTypes_HygieneFormId",
                table: "StudentHygieneTypes",
                column: "HygieneFormId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentHygieneTypes_InsertedByUserId",
                table: "StudentHygieneTypes",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentHygieneTypes_StudentId",
                table: "StudentHygieneTypes",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentHygieneTypes_UpdatedByUserId",
                table: "StudentHygieneTypes",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_DeletedByUserId",
                table: "Subject",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_GradeID",
                table: "Subject",
                column: "GradeID");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_InsertedByUserId",
                table: "Subject",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_SubjectCategoryID",
                table: "Subject",
                column: "SubjectCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_UpdatedByUserId",
                table: "Subject",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectCategory_DeletedByUserId",
                table: "SubjectCategory",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectCategory_InsertedByUserId",
                table: "SubjectCategory",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectCategory_UpdatedByUserId",
                table: "SubjectCategory",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_AccountNumberID",
                table: "Suppliers",
                column: "AccountNumberID");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_DeletedByUserId",
                table: "Suppliers",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_InsertedByUserId",
                table: "Suppliers",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_UpdatedByUserId",
                table: "Suppliers",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Test_AcademicYearID",
                table: "Test",
                column: "AcademicYearID");

            migrationBuilder.CreateIndex(
                name: "IX_Test_DeletedByUserId",
                table: "Test",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Test_GradeID",
                table: "Test",
                column: "GradeID");

            migrationBuilder.CreateIndex(
                name: "IX_Test_InsertedByUserId",
                table: "Test",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Test_SubjectID",
                table: "Test",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Test_UpdatedByUserId",
                table: "Test",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TestState_Name",
                table: "TestState",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TuitionDiscountTypes_AccountNumberID",
                table: "TuitionDiscountTypes",
                column: "AccountNumberID");

            migrationBuilder.CreateIndex(
                name: "IX_TuitionDiscountTypes_DeletedByUserId",
                table: "TuitionDiscountTypes",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TuitionDiscountTypes_InsertedByUserId",
                table: "TuitionDiscountTypes",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TuitionDiscountTypes_UpdatedByUserId",
                table: "TuitionDiscountTypes",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TuitionFeesTypes_AccountNumberID",
                table: "TuitionFeesTypes",
                column: "AccountNumberID");

            migrationBuilder.CreateIndex(
                name: "IX_TuitionFeesTypes_DeletedByUserId",
                table: "TuitionFeesTypes",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TuitionFeesTypes_InsertedByUserId",
                table: "TuitionFeesTypes",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TuitionFeesTypes_UpdatedByUserId",
                table: "TuitionFeesTypes",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Violation_DeletedByUserId",
                table: "Violation",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Violation_InsertedByUserId",
                table: "Violation",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Violation_Name",
                table: "Violation",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Violation_UpdatedByUserId",
                table: "Violation",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYear_Employee_DeletedByUserId",
                table: "AcademicYear",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYear_Employee_InsertedByUserId",
                table: "AcademicYear",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYear_Employee_UpdatedByUserId",
                table: "AcademicYear",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYear_School_SchoolID",
                table: "AcademicYear",
                column: "SchoolID",
                principalTable: "School",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingEntriesDetails_AccountingEntriesMaster_AccountingEntriesMasterID",
                table: "AccountingEntriesDetails",
                column: "AccountingEntriesMasterID",
                principalTable: "AccountingEntriesMaster",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingEntriesDetails_AccountingTreeCharts_AccountingTreeChartID",
                table: "AccountingEntriesDetails",
                column: "AccountingTreeChartID",
                principalTable: "AccountingTreeCharts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingEntriesDetails_Employee_DeletedByUserId",
                table: "AccountingEntriesDetails",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingEntriesDetails_Employee_InsertedByUserId",
                table: "AccountingEntriesDetails",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingEntriesDetails_Employee_UpdatedByUserId",
                table: "AccountingEntriesDetails",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingEntriesDocTypes_Employee_DeletedByUserId",
                table: "AccountingEntriesDocTypes",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingEntriesDocTypes_Employee_InsertedByUserId",
                table: "AccountingEntriesDocTypes",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingEntriesDocTypes_Employee_UpdatedByUserId",
                table: "AccountingEntriesDocTypes",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingEntriesMaster_Employee_DeletedByUserId",
                table: "AccountingEntriesMaster",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingEntriesMaster_Employee_InsertedByUserId",
                table: "AccountingEntriesMaster",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingEntriesMaster_Employee_UpdatedByUserId",
                table: "AccountingEntriesMaster",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingTreeCharts_Employee_DeletedByUserId",
                table: "AccountingTreeCharts",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingTreeCharts_Employee_InsertedByUserId",
                table: "AccountingTreeCharts",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingTreeCharts_Employee_UpdatedByUserId",
                table: "AccountingTreeCharts",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Employee_DeletedByUserId",
                table: "Assets",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Employee_InsertedByUserId",
                table: "Assets",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Employee_UpdatedByUserId",
                table: "Assets",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Banks_Employee_DeletedByUserId",
                table: "Banks",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Banks_Employee_InsertedByUserId",
                table: "Banks",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Banks_Employee_UpdatedByUserId",
                table: "Banks",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Building_Employee_DeletedByUserId",
                table: "Building",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Building_Employee_InsertedByUserId",
                table: "Building",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Building_Employee_UpdatedByUserId",
                table: "Building",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Building_School_SchoolID",
                table: "Building",
                column: "SchoolID",
                principalTable: "School",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_BusCompany_BusCompanyID",
                table: "Bus",
                column: "BusCompanyID",
                principalTable: "BusCompany",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_BusDistrict_BusDistrictID",
                table: "Bus",
                column: "BusDistrictID",
                principalTable: "BusDistrict",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_BusStatus_BusStatusID",
                table: "Bus",
                column: "BusStatusID",
                principalTable: "BusStatus",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_BusType_BusTypeID",
                table: "Bus",
                column: "BusTypeID",
                principalTable: "BusType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Employee_DeletedByUserId",
                table: "Bus",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Employee_DriverAssistantID",
                table: "Bus",
                column: "DriverAssistantID",
                principalTable: "Employee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Employee_DriverID",
                table: "Bus",
                column: "DriverID",
                principalTable: "Employee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Employee_InsertedByUserId",
                table: "Bus",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Employee_UpdatedByUserId",
                table: "Bus",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCategory_Employee_DeletedByUserId",
                table: "BusCategory",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCategory_Employee_InsertedByUserId",
                table: "BusCategory",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCategory_Employee_UpdatedByUserId",
                table: "BusCategory",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Employee_DeletedByUserId",
                table: "BusCompany",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Employee_InsertedByUserId",
                table: "BusCompany",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Employee_UpdatedByUserId",
                table: "BusCompany",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusDistrict_Employee_DeletedByUserId",
                table: "BusDistrict",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusDistrict_Employee_InsertedByUserId",
                table: "BusDistrict",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusDistrict_Employee_UpdatedByUserId",
                table: "BusDistrict",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStatus_Employee_DeletedByUserId",
                table: "BusStatus",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStatus_Employee_InsertedByUserId",
                table: "BusStatus",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStatus_Employee_UpdatedByUserId",
                table: "BusStatus",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStudent_Employee_DeletedByUserId",
                table: "BusStudent",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStudent_Employee_InsertedByUserId",
                table: "BusStudent",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStudent_Employee_UpdatedByUserId",
                table: "BusStudent",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStudent_Semester_SemseterID",
                table: "BusStudent",
                column: "SemseterID",
                principalTable: "Semester",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusStudent_Student_StudentID",
                table: "BusStudent",
                column: "StudentID",
                principalTable: "Student",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusType_Employee_DeletedByUserId",
                table: "BusType",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusType_Employee_InsertedByUserId",
                table: "BusType",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusType_Employee_UpdatedByUserId",
                table: "BusType",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Employee_DeletedByUserId",
                table: "Cart",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Employee_InsertedByUserId",
                table: "Cart",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Employee_UpdatedByUserId",
                table: "Cart",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Student_StudentID",
                table: "Cart",
                column: "StudentID",
                principalTable: "Student",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_ShopItem_Employee_DeletedByUserId",
                table: "Cart_ShopItem",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_ShopItem_Employee_InsertedByUserId",
                table: "Cart_ShopItem",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_ShopItem_Employee_UpdatedByUserId",
                table: "Cart_ShopItem",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_ShopItem_ShopItemColor_ShopItemColorID",
                table: "Cart_ShopItem",
                column: "ShopItemColorID",
                principalTable: "ShopItemColor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_ShopItem_ShopItemSize_ShopItemSizeID",
                table: "Cart_ShopItem",
                column: "ShopItemSizeID",
                principalTable: "ShopItemSize",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_ShopItem_ShopItem_ShopItemID",
                table: "Cart_ShopItem",
                column: "ShopItemID",
                principalTable: "ShopItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryField_Employee_DeletedByUserId",
                table: "CategoryField",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryField_Employee_InsertedByUserId",
                table: "CategoryField",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryField_Employee_UpdatedByUserId",
                table: "CategoryField",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryField_RegistrationCategory_RegistrationCategoryID",
                table: "CategoryField",
                column: "RegistrationCategoryID",
                principalTable: "RegistrationCategory",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Classroom_Employee_DeletedByUserId",
                table: "Classroom",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Classroom_Employee_HomeroomTeacherID",
                table: "Classroom",
                column: "HomeroomTeacherID",
                principalTable: "Employee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Classroom_Employee_InsertedByUserId",
                table: "Classroom",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Classroom_Employee_UpdatedByUserId",
                table: "Classroom",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Classroom_Floor_FloorID",
                table: "Classroom",
                column: "FloorID",
                principalTable: "Floor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Classroom_Grade_GradeID",
                table: "Classroom",
                column: "GradeID",
                principalTable: "Grade",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Credits_Employee_DeletedByUserId",
                table: "Credits",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Credits_Employee_InsertedByUserId",
                table: "Credits",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Credits_Employee_UpdatedByUserId",
                table: "Credits",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Debits_Employee_DeletedByUserId",
                table: "Debits",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Debits_Employee_InsertedByUserId",
                table: "Debits",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Debits_Employee_UpdatedByUserId",
                table: "Debits",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Employee_DeletedByUserId",
                table: "Departments",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Employee_InsertedByUserId",
                table: "Departments",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Employee_UpdatedByUserId",
                table: "Departments",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnoses_Employee_DeletedByUserId",
                table: "Diagnoses",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnoses_Employee_InsertedByUserId",
                table: "Diagnoses",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnoses_Employee_UpdatedByUserId",
                table: "Diagnoses",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Dose_Employee_DeletedByUserId",
                table: "Dose",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Dose_Employee_InsertedByUserId",
                table: "Dose",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Dose_Employee_UpdatedByUserId",
                table: "Dose",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Drugs_Employee_DeletedByUserId",
                table: "Drugs",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Drugs_Employee_InsertedByUserId",
                table: "Drugs",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Drugs_Employee_UpdatedByUserId",
                table: "Drugs",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Jobs_JobID",
                table: "Employee",
                column: "JobID",
                principalTable: "Jobs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_ReasonsForLeavingWork_ReasonOfLeavingID",
                table: "Employee",
                column: "ReasonOfLeavingID",
                principalTable: "ReasonsForLeavingWork",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Role_Role_ID",
                table: "Employee",
                column: "Role_ID",
                principalTable: "Role",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MCQQuestionOption_Question_Question_ID",
                table: "MCQQuestionOption",
                column: "Question_ID",
                principalTable: "Question",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYear_Employee_DeletedByUserId",
                table: "AcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYear_Employee_InsertedByUserId",
                table: "AcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYear_Employee_UpdatedByUserId",
                table: "AcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountingTreeCharts_Employee_DeletedByUserId",
                table: "AccountingTreeCharts");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountingTreeCharts_Employee_InsertedByUserId",
                table: "AccountingTreeCharts");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountingTreeCharts_Employee_UpdatedByUserId",
                table: "AccountingTreeCharts");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Employee_DeletedByUserId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Employee_InsertedByUserId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Employee_UpdatedByUserId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Employee_DeletedByUserId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Employee_InsertedByUserId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Employee_UpdatedByUserId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Employee_DeletedByUserId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Employee_InsertedByUserId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Employee_UpdatedByUserId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_JobCategories_Employee_DeletedByUserId",
                table: "JobCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_JobCategories_Employee_InsertedByUserId",
                table: "JobCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_JobCategories_Employee_UpdatedByUserId",
                table: "JobCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Employee_DeletedByUserId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Employee_InsertedByUserId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Employee_UpdatedByUserId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_MCQQuestionOption_Employee_DeletedByUserId",
                table: "MCQQuestionOption");

            migrationBuilder.DropForeignKey(
                name: "FK_MCQQuestionOption_Employee_InsertedByUserId",
                table: "MCQQuestionOption");

            migrationBuilder.DropForeignKey(
                name: "FK_MCQQuestionOption_Employee_UpdatedByUserId",
                table: "MCQQuestionOption");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Employee_DeletedByUserId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Employee_InsertedByUserId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Employee_UpdatedByUserId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_ReasonsForLeavingWork_Employee_DeletedByUserId",
                table: "ReasonsForLeavingWork");

            migrationBuilder.DropForeignKey(
                name: "FK_ReasonsForLeavingWork_Employee_InsertedByUserId",
                table: "ReasonsForLeavingWork");

            migrationBuilder.DropForeignKey(
                name: "FK_ReasonsForLeavingWork_Employee_UpdatedByUserId",
                table: "ReasonsForLeavingWork");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Employee_DeletedByUserId",
                table: "Role");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Employee_InsertedByUserId",
                table: "Role");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Employee_UpdatedByUserId",
                table: "Role");

            migrationBuilder.DropForeignKey(
                name: "FK_School_Employee_DeletedByUserId",
                table: "School");

            migrationBuilder.DropForeignKey(
                name: "FK_School_Employee_InsertedByUserId",
                table: "School");

            migrationBuilder.DropForeignKey(
                name: "FK_School_Employee_UpdatedByUserId",
                table: "School");

            migrationBuilder.DropForeignKey(
                name: "FK_Section_Employee_DeletedByUserId",
                table: "Section");

            migrationBuilder.DropForeignKey(
                name: "FK_Section_Employee_InsertedByUserId",
                table: "Section");

            migrationBuilder.DropForeignKey(
                name: "FK_Section_Employee_UpdatedByUserId",
                table: "Section");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Employee_DeletedByUserId",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Employee_InsertedByUserId",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Employee_UpdatedByUserId",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectCategory_Employee_DeletedByUserId",
                table: "SubjectCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectCategory_Employee_InsertedByUserId",
                table: "SubjectCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectCategory_Employee_UpdatedByUserId",
                table: "SubjectCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Test_Employee_DeletedByUserId",
                table: "Test");

            migrationBuilder.DropForeignKey(
                name: "FK_Test_Employee_InsertedByUserId",
                table: "Test");

            migrationBuilder.DropForeignKey(
                name: "FK_Test_Employee_UpdatedByUserId",
                table: "Test");

            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYear_School_SchoolID",
                table: "AcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_Section_School_SchoolID",
                table: "Section");

            migrationBuilder.DropForeignKey(
                name: "FK_Test_AcademicYear_AcademicYearID",
                table: "Test");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Grade_GradeID",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_Test_Grade_GradeID",
                table: "Test");

            migrationBuilder.DropForeignKey(
                name: "FK_MCQQuestionOption_Question_Question_ID",
                table: "MCQQuestionOption");

            migrationBuilder.DropTable(
                name: "AccountingEntriesDetails");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "BusStudent");

            migrationBuilder.DropTable(
                name: "Cart_ShopItem");

            migrationBuilder.DropTable(
                name: "Credits");

            migrationBuilder.DropTable(
                name: "Debits");

            migrationBuilder.DropTable(
                name: "EmployeeAttachment");

            migrationBuilder.DropTable(
                name: "EmployeeDays");

            migrationBuilder.DropTable(
                name: "EmployeeStudent");

            migrationBuilder.DropTable(
                name: "EmployeeTypeViolation");

            migrationBuilder.DropTable(
                name: "FeesActivation");

            migrationBuilder.DropTable(
                name: "FollowUpDrugs");

            migrationBuilder.DropTable(
                name: "HygieneTypeStudentHygieneTypes");

            migrationBuilder.DropTable(
                name: "Incomes");

            migrationBuilder.DropTable(
                name: "InstallmentDeductionDetails");

            migrationBuilder.DropTable(
                name: "MedicalHistories");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Outcomes");

            migrationBuilder.DropTable(
                name: "PayableDetails");

            migrationBuilder.DropTable(
                name: "ReceivableDetails");

            migrationBuilder.DropTable(
                name: "RegisterationFormInterview");

            migrationBuilder.DropTable(
                name: "RegisterationFormSubmittion");

            migrationBuilder.DropTable(
                name: "RegisterationFormTest");

            migrationBuilder.DropTable(
                name: "RegisterationFormTestAnswer");

            migrationBuilder.DropTable(
                name: "RegistrationFormCategory");

            migrationBuilder.DropTable(
                name: "Role_Detailes");

            migrationBuilder.DropTable(
                name: "SalesItemAttachment");

            migrationBuilder.DropTable(
                name: "StockingDetails");

            migrationBuilder.DropTable(
                name: "StoreCategories");

            migrationBuilder.DropTable(
                name: "StudentAcademicYear");

            migrationBuilder.DropTable(
                name: "AccountingEntriesMaster");

            migrationBuilder.DropTable(
                name: "BusCategory");

            migrationBuilder.DropTable(
                name: "Bus");

            migrationBuilder.DropTable(
                name: "Semester");

            migrationBuilder.DropTable(
                name: "ShopItemColor");

            migrationBuilder.DropTable(
                name: "ShopItemSize");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropTable(
                name: "Violation");

            migrationBuilder.DropTable(
                name: "TuitionDiscountTypes");

            migrationBuilder.DropTable(
                name: "Dose");

            migrationBuilder.DropTable(
                name: "Drugs");

            migrationBuilder.DropTable(
                name: "FollowUps");

            migrationBuilder.DropTable(
                name: "HygieneTypes");

            migrationBuilder.DropTable(
                name: "StudentHygieneTypes");

            migrationBuilder.DropTable(
                name: "InstallmentDeductionMaster");

            migrationBuilder.DropTable(
                name: "TuitionFeesTypes");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "PayableMaster");

            migrationBuilder.DropTable(
                name: "ReceivableMaster");

            migrationBuilder.DropTable(
                name: "InterViewState");

            migrationBuilder.DropTable(
                name: "InterviewTime");

            migrationBuilder.DropTable(
                name: "FieldOption");

            migrationBuilder.DropTable(
                name: "TestState");

            migrationBuilder.DropTable(
                name: "Page");

            migrationBuilder.DropTable(
                name: "InventoryDetails");

            migrationBuilder.DropTable(
                name: "Stocking");

            migrationBuilder.DropTable(
                name: "AccountingEntriesDocTypes");

            migrationBuilder.DropTable(
                name: "BusDistrict");

            migrationBuilder.DropTable(
                name: "BusStatus");

            migrationBuilder.DropTable(
                name: "BusType");

            migrationBuilder.DropTable(
                name: "Diagnoses");

            migrationBuilder.DropTable(
                name: "HygieneForms");

            migrationBuilder.DropTable(
                name: "OrderState");

            migrationBuilder.DropTable(
                name: "PayableDocType");

            migrationBuilder.DropTable(
                name: "ReceivableDocType");

            migrationBuilder.DropTable(
                name: "CategoryField");

            migrationBuilder.DropTable(
                name: "InventoryMaster");

            migrationBuilder.DropTable(
                name: "ShopItem");

            migrationBuilder.DropTable(
                name: "Classroom");

            migrationBuilder.DropTable(
                name: "FieldType");

            migrationBuilder.DropTable(
                name: "RegistrationCategory");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "InventoryFlags");

            migrationBuilder.DropTable(
                name: "Saves");

            migrationBuilder.DropTable(
                name: "Store");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "InventorySubCategories");

            migrationBuilder.DropTable(
                name: "Floor");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "RegisterationFormParent");

            migrationBuilder.DropTable(
                name: "InventoryCategories");

            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "Parent");

            migrationBuilder.DropTable(
                name: "RegisterationFormState");

            migrationBuilder.DropTable(
                name: "RegistrationForm");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "AcademicDegrees");

            migrationBuilder.DropTable(
                name: "AccountingTreeCharts");

            migrationBuilder.DropTable(
                name: "BusCompany");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "EmployeeType");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "ReasonsForLeavingWork");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "EndTypes");

            migrationBuilder.DropTable(
                name: "LinkFile");

            migrationBuilder.DropTable(
                name: "SubTypes");

            migrationBuilder.DropTable(
                name: "JobCategories");

            migrationBuilder.DropTable(
                name: "MotionTypes");

            migrationBuilder.DropTable(
                name: "School");

            migrationBuilder.DropTable(
                name: "SchoolType");

            migrationBuilder.DropTable(
                name: "AcademicYear");

            migrationBuilder.DropTable(
                name: "Grade");

            migrationBuilder.DropTable(
                name: "Section");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "MCQQuestionOption");

            migrationBuilder.DropTable(
                name: "QuestionType");

            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "SubjectCategory");
        }
    }
}
