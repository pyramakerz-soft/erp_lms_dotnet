using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class ViolationTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Violation",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Violation", x => x.ID);
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
                        name: "FK_EmployeeTypeViolation_Violation_ViolationID",
                        column: x => x.ViolationID,
                        principalTable: "Violation",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTypeViolation_EmployeeTypeID",
                table: "EmployeeTypeViolation",
                column: "EmployeeTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTypeViolation_ViolationID",
                table: "EmployeeTypeViolation",
                column: "ViolationID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeTypeViolation");

            migrationBuilder.DropTable(
                name: "Violation");
        }
    }
}
