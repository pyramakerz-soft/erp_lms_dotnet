using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class AdministratorMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Schools_SchoolID",
                table: "Grade");

            migrationBuilder.RenameColumn(
                name: "SchoolID",
                table: "Grade",
                newName: "SectionID");

            migrationBuilder.RenameIndex(
                name: "IX_Grade_SchoolID",
                table: "Grade",
                newName: "IX_Grade_SectionID");

            migrationBuilder.AddColumn<string>(
                name: "DateFrom",
                table: "Semester",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DateTo",
                table: "Semester",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Schools",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "SchoolTypeID",
                table: "Schools",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExpireDate",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LicenseNumber",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DateFrom",
                table: "AcademicYear",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DateTo",
                table: "AcademicYear",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AcademicYear",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SchoolID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Building_Schools_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "Schools",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAttachment",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Link = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAttachment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EmployeeAttachment_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchoolType",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Section",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SchoolID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Section_Schools_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "Schools",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubjectCategory",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectCategory", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Violations",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Violations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Floor",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    buildingID = table.Column<long>(type: "bigint", nullable: false)
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
                    SubjectCategoryID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.ID);
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
                name: "EmployeeTypeViolation",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeTypeID = table.Column<long>(type: "bigint", nullable: true),
                    ViolationID = table.Column<long>(type: "bigint", nullable: false)
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
                        name: "FK_EmployeeTypeViolation_Violations_ViolationID",
                        column: x => x.ViolationID,
                        principalTable: "Violations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
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
                    GradeID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classroom", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Classroom_Floor_FloorID",
                        column: x => x.FloorID,
                        principalTable: "Floor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Classroom_Grade_GradeID",
                        column: x => x.GradeID,
                        principalTable: "Grade",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schools_SchoolTypeID",
                table: "Schools",
                column: "SchoolTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Building_ID",
                table: "Building",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Building_SchoolID",
                table: "Building",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_Classroom_FloorID",
                table: "Classroom",
                column: "FloorID");

            migrationBuilder.CreateIndex(
                name: "IX_Classroom_GradeID",
                table: "Classroom",
                column: "GradeID");

            migrationBuilder.CreateIndex(
                name: "IX_Classroom_ID",
                table: "Classroom",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttachment_EmployeeID",
                table: "EmployeeAttachment",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttachment_Link",
                table: "EmployeeAttachment",
                column: "Link",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTypeViolation_EmployeeTypeID",
                table: "EmployeeTypeViolation",
                column: "EmployeeTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTypeViolation_ViolationID",
                table: "EmployeeTypeViolation",
                column: "ViolationID");

            migrationBuilder.CreateIndex(
                name: "IX_Floor_buildingID",
                table: "Floor",
                column: "buildingID");

            migrationBuilder.CreateIndex(
                name: "IX_Floor_ID",
                table: "Floor",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SchoolType_ID",
                table: "SchoolType",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Section_ID",
                table: "Section",
                column: "ID",
                unique: true);

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
                name: "IX_Subject_GradeID",
                table: "Subject",
                column: "GradeID");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_ID",
                table: "Subject",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subject_SubjectCategoryID",
                table: "Subject",
                column: "SubjectCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectCategory_ID",
                table: "SubjectCategory",
                column: "ID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Section_SectionID",
                table: "Grade",
                column: "SectionID",
                principalTable: "Section",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_SchoolType_SchoolTypeID",
                table: "Schools",
                column: "SchoolTypeID",
                principalTable: "SchoolType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Section_SectionID",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_SchoolType_SchoolTypeID",
                table: "Schools");

            migrationBuilder.DropTable(
                name: "Classroom");

            migrationBuilder.DropTable(
                name: "EmployeeAttachment");

            migrationBuilder.DropTable(
                name: "EmployeeTypeViolation");

            migrationBuilder.DropTable(
                name: "SchoolType");

            migrationBuilder.DropTable(
                name: "Section");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Floor");

            migrationBuilder.DropTable(
                name: "Violations");

            migrationBuilder.DropTable(
                name: "SubjectCategory");

            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropIndex(
                name: "IX_Schools_SchoolTypeID",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "DateFrom",
                table: "Semester");

            migrationBuilder.DropColumn(
                name: "DateTo",
                table: "Semester");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "SchoolTypeID",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ExpireDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "LicenseNumber",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DateFrom",
                table: "AcademicYear");

            migrationBuilder.DropColumn(
                name: "DateTo",
                table: "AcademicYear");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AcademicYear");

            migrationBuilder.RenameColumn(
                name: "SectionID",
                table: "Grade",
                newName: "SchoolID");

            migrationBuilder.RenameIndex(
                name: "IX_Grade_SectionID",
                table: "Grade",
                newName: "IX_Grade_SchoolID");

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Schools_SchoolID",
                table: "Grade",
                column: "SchoolID",
                principalTable: "Schools",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
