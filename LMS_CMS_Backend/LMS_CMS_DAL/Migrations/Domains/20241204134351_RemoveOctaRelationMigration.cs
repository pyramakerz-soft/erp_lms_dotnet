using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class RemoveOctaRelationMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYear_Octa_DeletedByOctaId",
                table: "AcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYear_Octa_InsertedByOctaId",
                table: "AcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYear_Octa_UpdatedByOctaId",
                table: "AcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Octa_DeletedByOctaId",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Octa_InsertedByOctaId",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Octa_UpdatedByOctaId",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCategory_Octa_DeletedByOctaId",
                table: "BusCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCategory_Octa_InsertedByOctaId",
                table: "BusCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCategory_Octa_UpdatedByOctaId",
                table: "BusCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Octa_DeletedByOctaId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Octa_InsertedByOctaId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Octa_UpdatedByOctaId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BusRestrict_Octa_DeletedByOctaId",
                table: "BusRestrict");

            migrationBuilder.DropForeignKey(
                name: "FK_BusRestrict_Octa_InsertedByOctaId",
                table: "BusRestrict");

            migrationBuilder.DropForeignKey(
                name: "FK_BusRestrict_Octa_UpdatedByOctaId",
                table: "BusRestrict");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStatus_Octa_DeletedByOctaId",
                table: "BusStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStatus_Octa_InsertedByOctaId",
                table: "BusStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStatus_Octa_UpdatedByOctaId",
                table: "BusStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStudent_Octa_DeletedByOctaId",
                table: "BusStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStudent_Octa_InsertedByOctaId",
                table: "BusStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStudent_Octa_UpdatedByOctaId",
                table: "BusStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusType_Octa_DeletedByOctaId",
                table: "BusType");

            migrationBuilder.DropForeignKey(
                name: "FK_BusType_Octa_InsertedByOctaId",
                table: "BusType");

            migrationBuilder.DropForeignKey(
                name: "FK_BusType_Octa_UpdatedByOctaId",
                table: "BusType");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Octa_DeletedByOctaId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Octa_InsertedByOctaId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Octa_UpdatedByOctaId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Octa_DeletedByOctaId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Octa_InsertedByOctaId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Octa_UpdatedByOctaId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailes_Octa_DeletedByOctaId",
                table: "Role_Detailes");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailes_Octa_InsertedByOctaId",
                table: "Role_Detailes");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailes_Octa_UpdatedByOctaId",
                table: "Role_Detailes");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Octa_DeletedByOctaId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Octa_InsertedByOctaId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Octa_UpdatedByOctaId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Octa_DeletedByOctaId",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Octa_InsertedByOctaId",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Octa_UpdatedByOctaId",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Semester_Octa_DeletedByOctaId",
                table: "Semester");

            migrationBuilder.DropForeignKey(
                name: "FK_Semester_Octa_InsertedByOctaId",
                table: "Semester");

            migrationBuilder.DropForeignKey(
                name: "FK_Semester_Octa_UpdatedByOctaId",
                table: "Semester");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Octa_DeletedByOctaId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Octa_InsertedByOctaId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Octa_UpdatedByOctaId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Octa");

            migrationBuilder.DropIndex(
                name: "IX_Students_DeletedByOctaId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_InsertedByOctaId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_UpdatedByOctaId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Semester_DeletedByOctaId",
                table: "Semester");

            migrationBuilder.DropIndex(
                name: "IX_Semester_InsertedByOctaId",
                table: "Semester");

            migrationBuilder.DropIndex(
                name: "IX_Semester_UpdatedByOctaId",
                table: "Semester");

            migrationBuilder.DropIndex(
                name: "IX_Schools_DeletedByOctaId",
                table: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_Schools_InsertedByOctaId",
                table: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_Schools_UpdatedByOctaId",
                table: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_Roles_DeletedByOctaId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_InsertedByOctaId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_UpdatedByOctaId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Role_Detailes_DeletedByOctaId",
                table: "Role_Detailes");

            migrationBuilder.DropIndex(
                name: "IX_Role_Detailes_InsertedByOctaId",
                table: "Role_Detailes");

            migrationBuilder.DropIndex(
                name: "IX_Role_Detailes_UpdatedByOctaId",
                table: "Role_Detailes");

            migrationBuilder.DropIndex(
                name: "IX_Parents_DeletedByOctaId",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_Parents_InsertedByOctaId",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_Parents_UpdatedByOctaId",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DeletedByOctaId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_InsertedByOctaId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_UpdatedByOctaId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_BusType_DeletedByOctaId",
                table: "BusType");

            migrationBuilder.DropIndex(
                name: "IX_BusType_InsertedByOctaId",
                table: "BusType");

            migrationBuilder.DropIndex(
                name: "IX_BusType_UpdatedByOctaId",
                table: "BusType");

            migrationBuilder.DropIndex(
                name: "IX_BusStudent_DeletedByOctaId",
                table: "BusStudent");

            migrationBuilder.DropIndex(
                name: "IX_BusStudent_InsertedByOctaId",
                table: "BusStudent");

            migrationBuilder.DropIndex(
                name: "IX_BusStudent_UpdatedByOctaId",
                table: "BusStudent");

            migrationBuilder.DropIndex(
                name: "IX_BusStatus_DeletedByOctaId",
                table: "BusStatus");

            migrationBuilder.DropIndex(
                name: "IX_BusStatus_InsertedByOctaId",
                table: "BusStatus");

            migrationBuilder.DropIndex(
                name: "IX_BusStatus_UpdatedByOctaId",
                table: "BusStatus");

            migrationBuilder.DropIndex(
                name: "IX_BusRestrict_DeletedByOctaId",
                table: "BusRestrict");

            migrationBuilder.DropIndex(
                name: "IX_BusRestrict_InsertedByOctaId",
                table: "BusRestrict");

            migrationBuilder.DropIndex(
                name: "IX_BusRestrict_UpdatedByOctaId",
                table: "BusRestrict");

            migrationBuilder.DropIndex(
                name: "IX_BusCompany_DeletedByOctaId",
                table: "BusCompany");

            migrationBuilder.DropIndex(
                name: "IX_BusCompany_InsertedByOctaId",
                table: "BusCompany");

            migrationBuilder.DropIndex(
                name: "IX_BusCompany_UpdatedByOctaId",
                table: "BusCompany");

            migrationBuilder.DropIndex(
                name: "IX_BusCategory_DeletedByOctaId",
                table: "BusCategory");

            migrationBuilder.DropIndex(
                name: "IX_BusCategory_InsertedByOctaId",
                table: "BusCategory");

            migrationBuilder.DropIndex(
                name: "IX_BusCategory_UpdatedByOctaId",
                table: "BusCategory");

            migrationBuilder.DropIndex(
                name: "IX_Bus_DeletedByOctaId",
                table: "Bus");

            migrationBuilder.DropIndex(
                name: "IX_Bus_InsertedByOctaId",
                table: "Bus");

            migrationBuilder.DropIndex(
                name: "IX_Bus_UpdatedByOctaId",
                table: "Bus");

            migrationBuilder.DropIndex(
                name: "IX_AcademicYear_DeletedByOctaId",
                table: "AcademicYear");

            migrationBuilder.DropIndex(
                name: "IX_AcademicYear_InsertedByOctaId",
                table: "AcademicYear");

            migrationBuilder.DropIndex(
                name: "IX_AcademicYear_UpdatedByOctaId",
                table: "AcademicYear");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Octa",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    User_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Octa", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_DeletedByOctaId",
                table: "Students",
                column: "DeletedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_InsertedByOctaId",
                table: "Students",
                column: "InsertedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UpdatedByOctaId",
                table: "Students",
                column: "UpdatedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_Semester_DeletedByOctaId",
                table: "Semester",
                column: "DeletedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_Semester_InsertedByOctaId",
                table: "Semester",
                column: "InsertedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_Semester_UpdatedByOctaId",
                table: "Semester",
                column: "UpdatedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_DeletedByOctaId",
                table: "Schools",
                column: "DeletedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_InsertedByOctaId",
                table: "Schools",
                column: "InsertedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_UpdatedByOctaId",
                table: "Schools",
                column: "UpdatedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_DeletedByOctaId",
                table: "Roles",
                column: "DeletedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_InsertedByOctaId",
                table: "Roles",
                column: "InsertedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UpdatedByOctaId",
                table: "Roles",
                column: "UpdatedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Detailes_DeletedByOctaId",
                table: "Role_Detailes",
                column: "DeletedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Detailes_InsertedByOctaId",
                table: "Role_Detailes",
                column: "InsertedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Detailes_UpdatedByOctaId",
                table: "Role_Detailes",
                column: "UpdatedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_DeletedByOctaId",
                table: "Parents",
                column: "DeletedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_InsertedByOctaId",
                table: "Parents",
                column: "InsertedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_UpdatedByOctaId",
                table: "Parents",
                column: "UpdatedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DeletedByOctaId",
                table: "Employees",
                column: "DeletedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_InsertedByOctaId",
                table: "Employees",
                column: "InsertedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UpdatedByOctaId",
                table: "Employees",
                column: "UpdatedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_BusType_DeletedByOctaId",
                table: "BusType",
                column: "DeletedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_BusType_InsertedByOctaId",
                table: "BusType",
                column: "InsertedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_BusType_UpdatedByOctaId",
                table: "BusType",
                column: "UpdatedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStudent_DeletedByOctaId",
                table: "BusStudent",
                column: "DeletedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStudent_InsertedByOctaId",
                table: "BusStudent",
                column: "InsertedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStudent_UpdatedByOctaId",
                table: "BusStudent",
                column: "UpdatedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStatus_DeletedByOctaId",
                table: "BusStatus",
                column: "DeletedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStatus_InsertedByOctaId",
                table: "BusStatus",
                column: "InsertedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStatus_UpdatedByOctaId",
                table: "BusStatus",
                column: "UpdatedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_BusRestrict_DeletedByOctaId",
                table: "BusRestrict",
                column: "DeletedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_BusRestrict_InsertedByOctaId",
                table: "BusRestrict",
                column: "InsertedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_BusRestrict_UpdatedByOctaId",
                table: "BusRestrict",
                column: "UpdatedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_BusCompany_DeletedByOctaId",
                table: "BusCompany",
                column: "DeletedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_BusCompany_InsertedByOctaId",
                table: "BusCompany",
                column: "InsertedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_BusCompany_UpdatedByOctaId",
                table: "BusCompany",
                column: "UpdatedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_BusCategory_DeletedByOctaId",
                table: "BusCategory",
                column: "DeletedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_BusCategory_InsertedByOctaId",
                table: "BusCategory",
                column: "InsertedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_BusCategory_UpdatedByOctaId",
                table: "BusCategory",
                column: "UpdatedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_DeletedByOctaId",
                table: "Bus",
                column: "DeletedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_InsertedByOctaId",
                table: "Bus",
                column: "InsertedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_UpdatedByOctaId",
                table: "Bus",
                column: "UpdatedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicYear_DeletedByOctaId",
                table: "AcademicYear",
                column: "DeletedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicYear_InsertedByOctaId",
                table: "AcademicYear",
                column: "InsertedByOctaId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicYear_UpdatedByOctaId",
                table: "AcademicYear",
                column: "UpdatedByOctaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYear_Octa_DeletedByOctaId",
                table: "AcademicYear",
                column: "DeletedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYear_Octa_InsertedByOctaId",
                table: "AcademicYear",
                column: "InsertedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYear_Octa_UpdatedByOctaId",
                table: "AcademicYear",
                column: "UpdatedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Octa_DeletedByOctaId",
                table: "Bus",
                column: "DeletedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Octa_InsertedByOctaId",
                table: "Bus",
                column: "InsertedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Octa_UpdatedByOctaId",
                table: "Bus",
                column: "UpdatedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCategory_Octa_DeletedByOctaId",
                table: "BusCategory",
                column: "DeletedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCategory_Octa_InsertedByOctaId",
                table: "BusCategory",
                column: "InsertedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCategory_Octa_UpdatedByOctaId",
                table: "BusCategory",
                column: "UpdatedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Octa_DeletedByOctaId",
                table: "BusCompany",
                column: "DeletedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Octa_InsertedByOctaId",
                table: "BusCompany",
                column: "InsertedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Octa_UpdatedByOctaId",
                table: "BusCompany",
                column: "UpdatedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusRestrict_Octa_DeletedByOctaId",
                table: "BusRestrict",
                column: "DeletedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusRestrict_Octa_InsertedByOctaId",
                table: "BusRestrict",
                column: "InsertedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusRestrict_Octa_UpdatedByOctaId",
                table: "BusRestrict",
                column: "UpdatedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStatus_Octa_DeletedByOctaId",
                table: "BusStatus",
                column: "DeletedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStatus_Octa_InsertedByOctaId",
                table: "BusStatus",
                column: "InsertedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStatus_Octa_UpdatedByOctaId",
                table: "BusStatus",
                column: "UpdatedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStudent_Octa_DeletedByOctaId",
                table: "BusStudent",
                column: "DeletedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStudent_Octa_InsertedByOctaId",
                table: "BusStudent",
                column: "InsertedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStudent_Octa_UpdatedByOctaId",
                table: "BusStudent",
                column: "UpdatedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusType_Octa_DeletedByOctaId",
                table: "BusType",
                column: "DeletedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusType_Octa_InsertedByOctaId",
                table: "BusType",
                column: "InsertedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusType_Octa_UpdatedByOctaId",
                table: "BusType",
                column: "UpdatedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Octa_DeletedByOctaId",
                table: "Employees",
                column: "DeletedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Octa_InsertedByOctaId",
                table: "Employees",
                column: "InsertedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Octa_UpdatedByOctaId",
                table: "Employees",
                column: "UpdatedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Octa_DeletedByOctaId",
                table: "Parents",
                column: "DeletedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Octa_InsertedByOctaId",
                table: "Parents",
                column: "InsertedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Octa_UpdatedByOctaId",
                table: "Parents",
                column: "UpdatedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailes_Octa_DeletedByOctaId",
                table: "Role_Detailes",
                column: "DeletedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailes_Octa_InsertedByOctaId",
                table: "Role_Detailes",
                column: "InsertedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailes_Octa_UpdatedByOctaId",
                table: "Role_Detailes",
                column: "UpdatedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Octa_DeletedByOctaId",
                table: "Roles",
                column: "DeletedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Octa_InsertedByOctaId",
                table: "Roles",
                column: "InsertedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Octa_UpdatedByOctaId",
                table: "Roles",
                column: "UpdatedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Octa_DeletedByOctaId",
                table: "Schools",
                column: "DeletedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Octa_InsertedByOctaId",
                table: "Schools",
                column: "InsertedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Octa_UpdatedByOctaId",
                table: "Schools",
                column: "UpdatedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Semester_Octa_DeletedByOctaId",
                table: "Semester",
                column: "DeletedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Semester_Octa_InsertedByOctaId",
                table: "Semester",
                column: "InsertedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Semester_Octa_UpdatedByOctaId",
                table: "Semester",
                column: "UpdatedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Octa_DeletedByOctaId",
                table: "Students",
                column: "DeletedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Octa_InsertedByOctaId",
                table: "Students",
                column: "InsertedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Octa_UpdatedByOctaId",
                table: "Students",
                column: "UpdatedByOctaId",
                principalTable: "Octa",
                principalColumn: "ID");
        }
    }
}
