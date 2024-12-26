using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Classroom",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    FloorID = table.Column<long>(type: "bigint", nullable: false),
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
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpireDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role_ID = table.Column<long>(type: "bigint", nullable: false),
                    BusCompanyID = table.Column<long>(type: "bigint", nullable: true),
                    EmployeeTypeID = table.Column<long>(type: "bigint", nullable: false),
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
                        name: "FK_Employee_BusCompany_BusCompanyID",
                        column: x => x.BusCompanyID,
                        principalTable: "BusCompany",
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
                    Link = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                name: "School",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SchoolTypeID = table.Column<long>(type: "bigint", nullable: false),
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
                name: "Student",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    en_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ar_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Parent_Id = table.Column<long>(type: "bigint", nullable: false),
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
                        name: "FK_Student_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
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
                        name: "FK_Student_Parent_Parent_Id",
                        column: x => x.Parent_Id,
                        principalTable: "Parent",
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Role_Detailes_Role_Role_ID",
                        column: x => x.Role_ID,
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
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
                name: "StudentAcademicYear",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<long>(type: "bigint", nullable: false),
                    SchoolID = table.Column<long>(type: "bigint", nullable: false),
                    ClassID = table.Column<long>(type: "bigint", nullable: false),
                    GradeID = table.Column<long>(type: "bigint", nullable: false),
                    SemesterID = table.Column<long>(type: "bigint", nullable: false),
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
                        name: "FK_StudentAcademicYear_Semester_SemesterID",
                        column: x => x.SemesterID,
                        principalTable: "Semester",
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
                name: "IX_Classroom_InsertedByUserId",
                table: "Classroom",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Classroom_UpdatedByUserId",
                table: "Classroom",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_BusCompanyID",
                table: "Employee",
                column: "BusCompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DeletedByUserId",
                table: "Employee",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_EmployeeTypeID",
                table: "Employee",
                column: "EmployeeTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_InsertedByUserId",
                table: "Employee",
                column: "InsertedByUserId");

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
                name: "IX_EmployeeAttachment_Link",
                table: "EmployeeAttachment",
                column: "Link",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttachment_UpdatedByUserId",
                table: "EmployeeAttachment",
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
                name: "IX_Grade_DeletedByUserId",
                table: "Grade",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_InsertedByUserId",
                table: "Grade",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_Name",
                table: "Grade",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grade_SectionID",
                table: "Grade",
                column: "SectionID");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_UpdatedByUserId",
                table: "Grade",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Page_ar_name",
                table: "Page",
                column: "ar_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Page_en_name",
                table: "Page",
                column: "en_name",
                unique: true);

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
                name: "IX_Section_Name",
                table: "Section",
                column: "Name",
                unique: true);

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
                name: "IX_Student_DeletedByUserId",
                table: "Student",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Email",
                table: "Student",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_InsertedByUserId",
                table: "Student",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Parent_Id",
                table: "Student",
                column: "Parent_Id");

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
                name: "IX_StudentAcademicYear_SemesterID",
                table: "StudentAcademicYear",
                column: "SemesterID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAcademicYear_StudentID",
                table: "StudentAcademicYear",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAcademicYear_UpdatedByUserId",
                table: "StudentAcademicYear",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_ar_name",
                table: "Subject",
                column: "ar_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subject_DeletedByUserId",
                table: "Subject",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_en_name",
                table: "Subject",
                column: "en_name",
                unique: true);

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
                name: "FK_Classroom_Employee_DeletedByUserId",
                table: "Classroom",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

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
                name: "FK_Employee_Role_Role_ID",
                table: "Employee",
                column: "Role_ID",
                principalTable: "Role",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_Role_Employee_DeletedByUserId",
                table: "Role");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Employee_InsertedByUserId",
                table: "Role");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Employee_UpdatedByUserId",
                table: "Role");

            migrationBuilder.DropTable(
                name: "BusStudent");

            migrationBuilder.DropTable(
                name: "EmployeeAttachment");

            migrationBuilder.DropTable(
                name: "Role_Detailes");

            migrationBuilder.DropTable(
                name: "StudentAcademicYear");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "BusCategory");

            migrationBuilder.DropTable(
                name: "Bus");

            migrationBuilder.DropTable(
                name: "Page");

            migrationBuilder.DropTable(
                name: "Classroom");

            migrationBuilder.DropTable(
                name: "Semester");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "SubjectCategory");

            migrationBuilder.DropTable(
                name: "BusDistrict");

            migrationBuilder.DropTable(
                name: "BusStatus");

            migrationBuilder.DropTable(
                name: "BusType");

            migrationBuilder.DropTable(
                name: "Floor");

            migrationBuilder.DropTable(
                name: "Grade");

            migrationBuilder.DropTable(
                name: "AcademicYear");

            migrationBuilder.DropTable(
                name: "Parent");

            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "Section");

            migrationBuilder.DropTable(
                name: "School");

            migrationBuilder.DropTable(
                name: "SchoolType");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "BusCompany");

            migrationBuilder.DropTable(
                name: "EmployeeType");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
