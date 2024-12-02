using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeType",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AcademicYear",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SchoolID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicYear", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Semester",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AcademicYearID = table.Column<long>(type: "bigint", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "Bus",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Capacity = table.Column<long>(type: "bigint", nullable: false),
                    DomainID = table.Column<long>(type: "bigint", nullable: false),
                    BusTypeID = table.Column<long>(type: "bigint", nullable: true),
                    BusRestrictID = table.Column<long>(type: "bigint", nullable: true),
                    BusStatusID = table.Column<long>(type: "bigint", nullable: true),
                    DriverID = table.Column<long>(type: "bigint", nullable: true),
                    DriverAssistantID = table.Column<long>(type: "bigint", nullable: true),
                    BusCompanyID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusCategory",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DomainId = table.Column<long>(type: "bigint", nullable: false)
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
                    DomainId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusCompany", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BusRestrict",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DomainId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusRestrict", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BusStatus",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DomainId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusStatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BusStudent",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusID = table.Column<long>(type: "bigint", nullable: false),
                    StudentID = table.Column<long>(type: "bigint", nullable: false),
                    BusCategoryID = table.Column<long>(type: "bigint", nullable: true),
                    SemseterID = table.Column<long>(type: "bigint", nullable: true),
                    IsException = table.Column<bool>(type: "bit", nullable: false),
                    ExceptionFromDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExceptionToDate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusStudent", x => x.Id);
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BusStudent_Semester_SemseterID",
                        column: x => x.SemseterID,
                        principalTable: "Semester",
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
                    DomainId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Domain_Page_Details",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Domain_ID = table.Column<long>(type: "bigint", nullable: false),
                    Page_ID = table.Column<long>(type: "bigint", nullable: false),
                    InsertedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    InsertedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domain_Page_Details", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Domains",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InsertedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    InsertedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domains", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    en_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ar_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Domain_ID = table.Column<long>(type: "bigint", nullable: false),
                    Role_ID = table.Column<long>(type: "bigint", nullable: false),
                    BusCompanyID = table.Column<long>(type: "bigint", nullable: true),
                    EmployeeTypeID = table.Column<long>(type: "bigint", nullable: false),
                    InsertedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    InsertedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employees_BusCompany_BusCompanyID",
                        column: x => x.BusCompanyID,
                        principalTable: "BusCompany",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Employees_Domains_Domain_ID",
                        column: x => x.Domain_ID,
                        principalTable: "Domains",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_EmployeeType_EmployeeTypeID",
                        column: x => x.EmployeeTypeID,
                        principalTable: "EmployeeType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Employees_Employees_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Employees_Employees_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    en_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ar_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Page_ID = table.Column<long>(type: "bigint", nullable: true),
                    InsertedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    InsertedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pages_Employees_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Pages_Employees_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Pages_Employees_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Pages_Pages_Page_ID",
                        column: x => x.Page_ID,
                        principalTable: "Pages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Parents",
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
                    InsertedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parents", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Parents_Employees_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Parents_Employees_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Parents_Employees_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Pyramakerz",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InsertedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    InsertedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pyramakerz", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pyramakerz_Employees_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Pyramakerz_Employees_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Pyramakerz_Employees_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Domain_ID = table.Column<long>(type: "bigint", nullable: false),
                    InsertedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    InsertedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Roles_Domains_Domain_ID",
                        column: x => x.Domain_ID,
                        principalTable: "Domains",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Roles_Employees_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Roles_Employees_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Roles_Employees_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Domain_id = table.Column<long>(type: "bigint", nullable: false),
                    InsertedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    InsertedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schools_Domains_Domain_id",
                        column: x => x.Domain_id,
                        principalTable: "Domains",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schools_Employees_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Schools_Employees_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Schools_Employees_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Students",
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
                    InsertedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Students_Employees_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Students_Employees_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Students_Employees_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Students_Parents_Parent_Id",
                        column: x => x.Parent_Id,
                        principalTable: "Parents",
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
                    InsertedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role_Detailes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Role_Detailes_Employees_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Role_Detailes_Employees_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Role_Detailes_Employees_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Role_Detailes_Pages_Page_ID",
                        column: x => x.Page_ID,
                        principalTable: "Pages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Role_Detailes_Roles_Role_ID",
                        column: x => x.Role_ID,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicYear_SchoolID",
                table: "AcademicYear",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_BusCompanyID",
                table: "Bus",
                column: "BusCompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_BusRestrictID",
                table: "Bus",
                column: "BusRestrictID");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_BusStatusID",
                table: "Bus",
                column: "BusStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_BusTypeID",
                table: "Bus",
                column: "BusTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_DomainID",
                table: "Bus",
                column: "DomainID");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_DriverAssistantID",
                table: "Bus",
                column: "DriverAssistantID");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_DriverID",
                table: "Bus",
                column: "DriverID");

            migrationBuilder.CreateIndex(
                name: "IX_BusCategory_DomainId",
                table: "BusCategory",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_BusCompany_DomainId",
                table: "BusCompany",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_BusRestrict_DomainId",
                table: "BusRestrict",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStatus_DomainId",
                table: "BusStatus",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStudent_BusCategoryID",
                table: "BusStudent",
                column: "BusCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_BusStudent_BusID",
                table: "BusStudent",
                column: "BusID");

            migrationBuilder.CreateIndex(
                name: "IX_BusStudent_SemseterID",
                table: "BusStudent",
                column: "SemseterID");

            migrationBuilder.CreateIndex(
                name: "IX_BusStudent_StudentID",
                table: "BusStudent",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_BusType_DomainId",
                table: "BusType",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_Domain_Page_Details_DeletedByUserId",
                table: "Domain_Page_Details",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Domain_Page_Details_Domain_ID",
                table: "Domain_Page_Details",
                column: "Domain_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Domain_Page_Details_InsertedByUserId",
                table: "Domain_Page_Details",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Domain_Page_Details_Page_ID",
                table: "Domain_Page_Details",
                column: "Page_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Domain_Page_Details_UpdatedByUserId",
                table: "Domain_Page_Details",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Domains_DeletedByUserId",
                table: "Domains",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Domains_InsertedByUserId",
                table: "Domains",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Domains_Name",
                table: "Domains",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Domains_UpdatedByUserId",
                table: "Domains",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BusCompanyID",
                table: "Employees",
                column: "BusCompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DeletedByUserId",
                table: "Employees",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Domain_ID",
                table: "Employees",
                column: "Domain_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeTypeID",
                table: "Employees",
                column: "EmployeeTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_InsertedByUserId",
                table: "Employees",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Role_ID",
                table: "Employees",
                column: "Role_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UpdatedByUserId",
                table: "Employees",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_User_Name",
                table: "Employees",
                column: "User_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pages_ar_name",
                table: "Pages",
                column: "ar_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pages_DeletedByUserId",
                table: "Pages",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_en_name",
                table: "Pages",
                column: "en_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pages_InsertedByUserId",
                table: "Pages",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_Page_ID",
                table: "Pages",
                column: "Page_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_UpdatedByUserId",
                table: "Pages",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_DeletedByUserId",
                table: "Parents",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_Email",
                table: "Parents",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parents_InsertedByUserId",
                table: "Parents",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_UpdatedByUserId",
                table: "Parents",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_User_Name",
                table: "Parents",
                column: "User_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pyramakerz_DeletedByUserId",
                table: "Pyramakerz",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pyramakerz_Email",
                table: "Pyramakerz",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pyramakerz_InsertedByUserId",
                table: "Pyramakerz",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pyramakerz_UpdatedByUserId",
                table: "Pyramakerz",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pyramakerz_User_Name",
                table: "Pyramakerz",
                column: "User_Name",
                unique: true);

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
                name: "IX_Roles_DeletedByUserId",
                table: "Roles",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Domain_ID",
                table: "Roles",
                column: "Domain_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_InsertedByUserId",
                table: "Roles",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UpdatedByUserId",
                table: "Roles",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_DeletedByUserId",
                table: "Schools",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_Domain_id",
                table: "Schools",
                column: "Domain_id");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_InsertedByUserId",
                table: "Schools",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_UpdatedByUserId",
                table: "Schools",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Semester_AcademicYearID",
                table: "Semester",
                column: "AcademicYearID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_DeletedByUserId",
                table: "Students",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Email",
                table: "Students",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_InsertedByUserId",
                table: "Students",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Parent_Id",
                table: "Students",
                column: "Parent_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UpdatedByUserId",
                table: "Students",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_User_Name",
                table: "Students",
                column: "User_Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYear_Schools_SchoolID",
                table: "AcademicYear",
                column: "SchoolID",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_BusCompany_BusCompanyID",
                table: "Bus",
                column: "BusCompanyID",
                principalTable: "BusCompany",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_BusRestrict_BusRestrictID",
                table: "Bus",
                column: "BusRestrictID",
                principalTable: "BusRestrict",
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
                name: "FK_Bus_Domains_DomainID",
                table: "Bus",
                column: "DomainID",
                principalTable: "Domains",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Employees_DriverAssistantID",
                table: "Bus",
                column: "DriverAssistantID",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Employees_DriverID",
                table: "Bus",
                column: "DriverID",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusCategory_Domains_DomainId",
                table: "BusCategory",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Domains_DomainId",
                table: "BusCompany",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusRestrict_Domains_DomainId",
                table: "BusRestrict",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusStatus_Domains_DomainId",
                table: "BusStatus",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusStudent_Students_StudentID",
                table: "BusStudent",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusType_Domains_DomainId",
                table: "BusType",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Domain_Page_Details_Domains_Domain_ID",
                table: "Domain_Page_Details",
                column: "Domain_ID",
                principalTable: "Domains",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Domain_Page_Details_Employees_DeletedByUserId",
                table: "Domain_Page_Details",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Domain_Page_Details_Employees_InsertedByUserId",
                table: "Domain_Page_Details",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Domain_Page_Details_Employees_UpdatedByUserId",
                table: "Domain_Page_Details",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Domain_Page_Details_Pages_Page_ID",
                table: "Domain_Page_Details",
                column: "Page_ID",
                principalTable: "Pages",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Domains_Employees_DeletedByUserId",
                table: "Domains",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Domains_Employees_InsertedByUserId",
                table: "Domains",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Domains_Employees_UpdatedByUserId",
                table: "Domains",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Roles_Role_ID",
                table: "Employees",
                column: "Role_ID",
                principalTable: "Roles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_BusCompany_BusCompanyID",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Domains_Domain_ID",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Domains_Domain_ID",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Employees_DeletedByUserId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Employees_InsertedByUserId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Employees_UpdatedByUserId",
                table: "Roles");

            migrationBuilder.DropTable(
                name: "BusStudent");

            migrationBuilder.DropTable(
                name: "Domain_Page_Details");

            migrationBuilder.DropTable(
                name: "Pyramakerz");

            migrationBuilder.DropTable(
                name: "Role_Detailes");

            migrationBuilder.DropTable(
                name: "BusCategory");

            migrationBuilder.DropTable(
                name: "Bus");

            migrationBuilder.DropTable(
                name: "Semester");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "BusRestrict");

            migrationBuilder.DropTable(
                name: "BusStatus");

            migrationBuilder.DropTable(
                name: "BusType");

            migrationBuilder.DropTable(
                name: "AcademicYear");

            migrationBuilder.DropTable(
                name: "Parents");

            migrationBuilder.DropTable(
                name: "Schools");

            migrationBuilder.DropTable(
                name: "BusCompany");

            migrationBuilder.DropTable(
                name: "Domains");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "EmployeeType");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
