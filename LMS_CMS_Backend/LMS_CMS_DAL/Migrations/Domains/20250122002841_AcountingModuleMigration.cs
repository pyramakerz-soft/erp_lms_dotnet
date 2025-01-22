using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class AcountingModuleMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Employee_DeletedByUserId",
                table: "Student");

            migrationBuilder.AddColumn<long>(
                name: "EmployeeID",
                table: "Student",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employee",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AcademicDegreeID",
                table: "Employee",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AccountNumberID",
                table: "Employee",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnnualLeaveBalance",
                table: "Employee",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttendanceTime",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BirthdayDate",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CanReceiveMessage",
                table: "Employee",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CanReceiveRequest",
                table: "Employee",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CasualLeavesBalance",
                table: "Employee",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateOfAppointment",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateOfLeavingWork",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "DelayAllowance",
                table: "Employee",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DepartmentID",
                table: "Employee",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartureTime",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GraduationYear",
                table: "Employee",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasAttendance",
                table: "Employee",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "JobID",
                table: "Employee",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MonthSalary",
                table: "Employee",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MonthlyLeaveRequestBalance",
                table: "Employee",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalID",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Nationality",
                table: "Employee",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportNumber",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ReasonOfLeavingID",
                table: "Employee",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResidenceNumber",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true);

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
                    table.ForeignKey(
                        name: "FK_AccountingEntriesDocTypes_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AccountingEntriesDocTypes_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AccountingEntriesDocTypes_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
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
                    table.ForeignKey(
                        name: "FK_Departments_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Departments_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Departments_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
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
                name: "AccountingTreeCharts",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    SubTypeID = table.Column<long>(type: "bigint", nullable: false),
                    EndTypeID = table.Column<long>(type: "bigint", nullable: false),
                    MainAccountNumberID = table.Column<long>(type: "bigint", nullable: true),
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
                        name: "FK_AccountingTreeCharts_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AccountingTreeCharts_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AccountingTreeCharts_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AccountingTreeCharts_EndTypes_EndTypeID",
                        column: x => x.EndTypeID,
                        principalTable: "EndTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
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
                    table.ForeignKey(
                        name: "FK_Assets_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Assets_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Assets_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
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
                    table.ForeignKey(
                        name: "FK_Banks_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Banks_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Banks_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
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
                    table.ForeignKey(
                        name: "FK_Credits_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Credits_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Credits_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
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
                    table.ForeignKey(
                        name: "FK_Debits_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Debits_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Debits_Employee_UpdatedByUserId",
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

            migrationBuilder.CreateIndex(
                name: "IX_Student_EmployeeID",
                table: "Student",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_AcademicDegreeID",
                table: "Employee",
                column: "AcademicDegreeID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_AccountNumberID",
                table: "Employee",
                column: "AccountNumberID");

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
                name: "IX_Employee_JobID",
                table: "Employee",
                column: "JobID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ReasonOfLeavingID",
                table: "Employee",
                column: "ReasonOfLeavingID");

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
                name: "IX_AccountingTreeCharts_MainAccountNumberID",
                table: "AccountingTreeCharts",
                column: "MainAccountNumberID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_AcademicDegrees_AcademicDegreeID",
                table: "Employee",
                column: "AcademicDegreeID",
                principalTable: "AcademicDegrees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_AccountingTreeCharts_AccountNumberID",
                table: "Employee",
                column: "AccountNumberID",
                principalTable: "AccountingTreeCharts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Departments_DepartmentID",
                table: "Employee",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Student_Employee_DeletedByUserId",
                table: "Student",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Employee_EmployeeID",
                table: "Student",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_AcademicDegrees_AcademicDegreeID",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_AccountingTreeCharts_AccountNumberID",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Departments_DepartmentID",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Jobs_JobID",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_ReasonsForLeavingWork_ReasonOfLeavingID",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Employee_DeletedByUserId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Employee_EmployeeID",
                table: "Student");

            migrationBuilder.DropTable(
                name: "AcademicDegrees");

            migrationBuilder.DropTable(
                name: "AccountingEntriesDocTypes");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Credits");

            migrationBuilder.DropTable(
                name: "Debits");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "EmployeeDays");

            migrationBuilder.DropTable(
                name: "Incomes");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "MotionTypes");

            migrationBuilder.DropTable(
                name: "Outcomes");

            migrationBuilder.DropTable(
                name: "ReasonsForLeavingWork");

            migrationBuilder.DropTable(
                name: "Saves");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "TuitionDiscountTypes");

            migrationBuilder.DropTable(
                name: "TuitionFeesTypes");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropTable(
                name: "JobCategories");

            migrationBuilder.DropTable(
                name: "AccountingTreeCharts");

            migrationBuilder.DropTable(
                name: "EndTypes");

            migrationBuilder.DropTable(
                name: "SubTypes");

            migrationBuilder.DropIndex(
                name: "IX_Student_EmployeeID",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Employee_AcademicDegreeID",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_AccountNumberID",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_DepartmentID",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_Email",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_JobID",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_ReasonOfLeavingID",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "AcademicDegreeID",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "AccountNumberID",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "AnnualLeaveBalance",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "AttendanceTime",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "BirthdayDate",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "CanReceiveMessage",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "CanReceiveRequest",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "CasualLeavesBalance",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "DateOfAppointment",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "DateOfLeavingWork",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "DelayAllowance",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "DepartmentID",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "DepartureTime",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "GraduationYear",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "HasAttendance",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "JobID",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "MonthSalary",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "MonthlyLeaveRequestBalance",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "NationalID",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "PassportNumber",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "ReasonOfLeavingID",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "ResidenceNumber",
                table: "Employee");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Employee_DeletedByUserId",
                table: "Student",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");
        }
    }
}
