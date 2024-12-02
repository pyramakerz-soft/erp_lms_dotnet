using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class AddPyramakerzRelationInAuditable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Students_DeletedByPyramakerzId",
                table: "Students",
                column: "DeletedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_InsertedByPyramakerzId",
                table: "Students",
                column: "InsertedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UpdatedByPyramakerzId",
                table: "Students",
                column: "UpdatedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Semester_DeletedByPyramakerzId",
                table: "Semester",
                column: "DeletedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Semester_InsertedByPyramakerzId",
                table: "Semester",
                column: "InsertedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Semester_UpdatedByPyramakerzId",
                table: "Semester",
                column: "UpdatedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_DeletedByPyramakerzId",
                table: "Schools",
                column: "DeletedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_InsertedByPyramakerzId",
                table: "Schools",
                column: "InsertedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_UpdatedByPyramakerzId",
                table: "Schools",
                column: "UpdatedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_DeletedByPyramakerzId",
                table: "Roles",
                column: "DeletedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_InsertedByPyramakerzId",
                table: "Roles",
                column: "InsertedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UpdatedByPyramakerzId",
                table: "Roles",
                column: "UpdatedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Detailes_DeletedByPyramakerzId",
                table: "Role_Detailes",
                column: "DeletedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Detailes_InsertedByPyramakerzId",
                table: "Role_Detailes",
                column: "InsertedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Detailes_UpdatedByPyramakerzId",
                table: "Role_Detailes",
                column: "UpdatedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_DeletedByPyramakerzId",
                table: "Parents",
                column: "DeletedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_InsertedByPyramakerzId",
                table: "Parents",
                column: "InsertedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_UpdatedByPyramakerzId",
                table: "Parents",
                column: "UpdatedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DeletedByPyramakerzId",
                table: "Employees",
                column: "DeletedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_InsertedByPyramakerzId",
                table: "Employees",
                column: "InsertedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UpdatedByPyramakerzId",
                table: "Employees",
                column: "UpdatedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Domains_DeletedByPyramakerzId",
                table: "Domains",
                column: "DeletedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Domains_InsertedByPyramakerzId",
                table: "Domains",
                column: "InsertedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Domains_UpdatedByPyramakerzId",
                table: "Domains",
                column: "UpdatedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Domain_Page_Details_DeletedByPyramakerzId",
                table: "Domain_Page_Details",
                column: "DeletedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Domain_Page_Details_InsertedByPyramakerzId",
                table: "Domain_Page_Details",
                column: "InsertedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Domain_Page_Details_UpdatedByPyramakerzId",
                table: "Domain_Page_Details",
                column: "UpdatedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_BusType_DeletedByPyramakerzId",
                table: "BusType",
                column: "DeletedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_BusType_InsertedByPyramakerzId",
                table: "BusType",
                column: "InsertedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_BusType_UpdatedByPyramakerzId",
                table: "BusType",
                column: "UpdatedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStudent_DeletedByPyramakerzId",
                table: "BusStudent",
                column: "DeletedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStudent_InsertedByPyramakerzId",
                table: "BusStudent",
                column: "InsertedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStudent_UpdatedByPyramakerzId",
                table: "BusStudent",
                column: "UpdatedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStatus_DeletedByPyramakerzId",
                table: "BusStatus",
                column: "DeletedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStatus_InsertedByPyramakerzId",
                table: "BusStatus",
                column: "InsertedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStatus_UpdatedByPyramakerzId",
                table: "BusStatus",
                column: "UpdatedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_BusRestrict_DeletedByPyramakerzId",
                table: "BusRestrict",
                column: "DeletedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_BusRestrict_InsertedByPyramakerzId",
                table: "BusRestrict",
                column: "InsertedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_BusRestrict_UpdatedByPyramakerzId",
                table: "BusRestrict",
                column: "UpdatedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_BusCompany_DeletedByPyramakerzId",
                table: "BusCompany",
                column: "DeletedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_BusCompany_InsertedByPyramakerzId",
                table: "BusCompany",
                column: "InsertedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_BusCompany_UpdatedByPyramakerzId",
                table: "BusCompany",
                column: "UpdatedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_BusCategory_DeletedByPyramakerzId",
                table: "BusCategory",
                column: "DeletedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_BusCategory_InsertedByPyramakerzId",
                table: "BusCategory",
                column: "InsertedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_BusCategory_UpdatedByPyramakerzId",
                table: "BusCategory",
                column: "UpdatedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_DeletedByPyramakerzId",
                table: "Bus",
                column: "DeletedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_InsertedByPyramakerzId",
                table: "Bus",
                column: "InsertedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_UpdatedByPyramakerzId",
                table: "Bus",
                column: "UpdatedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicYear_DeletedByPyramakerzId",
                table: "AcademicYear",
                column: "DeletedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicYear_InsertedByPyramakerzId",
                table: "AcademicYear",
                column: "InsertedByPyramakerzId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicYear_UpdatedByPyramakerzId",
                table: "AcademicYear",
                column: "UpdatedByPyramakerzId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYear_Pyramakerz_DeletedByPyramakerzId",
                table: "AcademicYear",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYear_Pyramakerz_InsertedByPyramakerzId",
                table: "AcademicYear",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYear_Pyramakerz_UpdatedByPyramakerzId",
                table: "AcademicYear",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Pyramakerz_DeletedByPyramakerzId",
                table: "Bus",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Pyramakerz_InsertedByPyramakerzId",
                table: "Bus",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Pyramakerz_UpdatedByPyramakerzId",
                table: "Bus",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCategory_Pyramakerz_DeletedByPyramakerzId",
                table: "BusCategory",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCategory_Pyramakerz_InsertedByPyramakerzId",
                table: "BusCategory",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCategory_Pyramakerz_UpdatedByPyramakerzId",
                table: "BusCategory",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Pyramakerz_DeletedByPyramakerzId",
                table: "BusCompany",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Pyramakerz_InsertedByPyramakerzId",
                table: "BusCompany",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Pyramakerz_UpdatedByPyramakerzId",
                table: "BusCompany",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusRestrict_Pyramakerz_DeletedByPyramakerzId",
                table: "BusRestrict",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusRestrict_Pyramakerz_InsertedByPyramakerzId",
                table: "BusRestrict",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusRestrict_Pyramakerz_UpdatedByPyramakerzId",
                table: "BusRestrict",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStatus_Pyramakerz_DeletedByPyramakerzId",
                table: "BusStatus",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStatus_Pyramakerz_InsertedByPyramakerzId",
                table: "BusStatus",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStatus_Pyramakerz_UpdatedByPyramakerzId",
                table: "BusStatus",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStudent_Pyramakerz_DeletedByPyramakerzId",
                table: "BusStudent",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStudent_Pyramakerz_InsertedByPyramakerzId",
                table: "BusStudent",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStudent_Pyramakerz_UpdatedByPyramakerzId",
                table: "BusStudent",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusType_Pyramakerz_DeletedByPyramakerzId",
                table: "BusType",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusType_Pyramakerz_InsertedByPyramakerzId",
                table: "BusType",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusType_Pyramakerz_UpdatedByPyramakerzId",
                table: "BusType",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Domain_Page_Details_Pyramakerz_DeletedByPyramakerzId",
                table: "Domain_Page_Details",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Domain_Page_Details_Pyramakerz_InsertedByPyramakerzId",
                table: "Domain_Page_Details",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Domain_Page_Details_Pyramakerz_UpdatedByPyramakerzId",
                table: "Domain_Page_Details",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Domains_Pyramakerz_DeletedByPyramakerzId",
                table: "Domains",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Domains_Pyramakerz_InsertedByPyramakerzId",
                table: "Domains",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Domains_Pyramakerz_UpdatedByPyramakerzId",
                table: "Domains",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Pyramakerz_DeletedByPyramakerzId",
                table: "Employees",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Pyramakerz_InsertedByPyramakerzId",
                table: "Employees",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Pyramakerz_UpdatedByPyramakerzId",
                table: "Employees",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Pyramakerz_DeletedByPyramakerzId",
                table: "Parents",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Pyramakerz_InsertedByPyramakerzId",
                table: "Parents",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Pyramakerz_UpdatedByPyramakerzId",
                table: "Parents",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailes_Pyramakerz_DeletedByPyramakerzId",
                table: "Role_Detailes",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailes_Pyramakerz_InsertedByPyramakerzId",
                table: "Role_Detailes",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailes_Pyramakerz_UpdatedByPyramakerzId",
                table: "Role_Detailes",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Pyramakerz_DeletedByPyramakerzId",
                table: "Roles",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Pyramakerz_InsertedByPyramakerzId",
                table: "Roles",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Pyramakerz_UpdatedByPyramakerzId",
                table: "Roles",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Pyramakerz_DeletedByPyramakerzId",
                table: "Schools",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Pyramakerz_InsertedByPyramakerzId",
                table: "Schools",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Pyramakerz_UpdatedByPyramakerzId",
                table: "Schools",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Semester_Pyramakerz_DeletedByPyramakerzId",
                table: "Semester",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Semester_Pyramakerz_InsertedByPyramakerzId",
                table: "Semester",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Semester_Pyramakerz_UpdatedByPyramakerzId",
                table: "Semester",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Pyramakerz_DeletedByPyramakerzId",
                table: "Students",
                column: "DeletedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Pyramakerz_InsertedByPyramakerzId",
                table: "Students",
                column: "InsertedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Pyramakerz_UpdatedByPyramakerzId",
                table: "Students",
                column: "UpdatedByPyramakerzId",
                principalTable: "Pyramakerz",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYear_Pyramakerz_DeletedByPyramakerzId",
                table: "AcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYear_Pyramakerz_InsertedByPyramakerzId",
                table: "AcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYear_Pyramakerz_UpdatedByPyramakerzId",
                table: "AcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Pyramakerz_DeletedByPyramakerzId",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Pyramakerz_InsertedByPyramakerzId",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Pyramakerz_UpdatedByPyramakerzId",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCategory_Pyramakerz_DeletedByPyramakerzId",
                table: "BusCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCategory_Pyramakerz_InsertedByPyramakerzId",
                table: "BusCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCategory_Pyramakerz_UpdatedByPyramakerzId",
                table: "BusCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Pyramakerz_DeletedByPyramakerzId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Pyramakerz_InsertedByPyramakerzId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Pyramakerz_UpdatedByPyramakerzId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BusRestrict_Pyramakerz_DeletedByPyramakerzId",
                table: "BusRestrict");

            migrationBuilder.DropForeignKey(
                name: "FK_BusRestrict_Pyramakerz_InsertedByPyramakerzId",
                table: "BusRestrict");

            migrationBuilder.DropForeignKey(
                name: "FK_BusRestrict_Pyramakerz_UpdatedByPyramakerzId",
                table: "BusRestrict");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStatus_Pyramakerz_DeletedByPyramakerzId",
                table: "BusStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStatus_Pyramakerz_InsertedByPyramakerzId",
                table: "BusStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStatus_Pyramakerz_UpdatedByPyramakerzId",
                table: "BusStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStudent_Pyramakerz_DeletedByPyramakerzId",
                table: "BusStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStudent_Pyramakerz_InsertedByPyramakerzId",
                table: "BusStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStudent_Pyramakerz_UpdatedByPyramakerzId",
                table: "BusStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusType_Pyramakerz_DeletedByPyramakerzId",
                table: "BusType");

            migrationBuilder.DropForeignKey(
                name: "FK_BusType_Pyramakerz_InsertedByPyramakerzId",
                table: "BusType");

            migrationBuilder.DropForeignKey(
                name: "FK_BusType_Pyramakerz_UpdatedByPyramakerzId",
                table: "BusType");

            migrationBuilder.DropForeignKey(
                name: "FK_Domain_Page_Details_Pyramakerz_DeletedByPyramakerzId",
                table: "Domain_Page_Details");

            migrationBuilder.DropForeignKey(
                name: "FK_Domain_Page_Details_Pyramakerz_InsertedByPyramakerzId",
                table: "Domain_Page_Details");

            migrationBuilder.DropForeignKey(
                name: "FK_Domain_Page_Details_Pyramakerz_UpdatedByPyramakerzId",
                table: "Domain_Page_Details");

            migrationBuilder.DropForeignKey(
                name: "FK_Domains_Pyramakerz_DeletedByPyramakerzId",
                table: "Domains");

            migrationBuilder.DropForeignKey(
                name: "FK_Domains_Pyramakerz_InsertedByPyramakerzId",
                table: "Domains");

            migrationBuilder.DropForeignKey(
                name: "FK_Domains_Pyramakerz_UpdatedByPyramakerzId",
                table: "Domains");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Pyramakerz_DeletedByPyramakerzId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Pyramakerz_InsertedByPyramakerzId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Pyramakerz_UpdatedByPyramakerzId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Pyramakerz_DeletedByPyramakerzId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Pyramakerz_InsertedByPyramakerzId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Pyramakerz_UpdatedByPyramakerzId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailes_Pyramakerz_DeletedByPyramakerzId",
                table: "Role_Detailes");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailes_Pyramakerz_InsertedByPyramakerzId",
                table: "Role_Detailes");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailes_Pyramakerz_UpdatedByPyramakerzId",
                table: "Role_Detailes");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Pyramakerz_DeletedByPyramakerzId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Pyramakerz_InsertedByPyramakerzId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Pyramakerz_UpdatedByPyramakerzId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Pyramakerz_DeletedByPyramakerzId",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Pyramakerz_InsertedByPyramakerzId",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Pyramakerz_UpdatedByPyramakerzId",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Semester_Pyramakerz_DeletedByPyramakerzId",
                table: "Semester");

            migrationBuilder.DropForeignKey(
                name: "FK_Semester_Pyramakerz_InsertedByPyramakerzId",
                table: "Semester");

            migrationBuilder.DropForeignKey(
                name: "FK_Semester_Pyramakerz_UpdatedByPyramakerzId",
                table: "Semester");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Pyramakerz_DeletedByPyramakerzId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Pyramakerz_InsertedByPyramakerzId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Pyramakerz_UpdatedByPyramakerzId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_DeletedByPyramakerzId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_InsertedByPyramakerzId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_UpdatedByPyramakerzId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Semester_DeletedByPyramakerzId",
                table: "Semester");

            migrationBuilder.DropIndex(
                name: "IX_Semester_InsertedByPyramakerzId",
                table: "Semester");

            migrationBuilder.DropIndex(
                name: "IX_Semester_UpdatedByPyramakerzId",
                table: "Semester");

            migrationBuilder.DropIndex(
                name: "IX_Schools_DeletedByPyramakerzId",
                table: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_Schools_InsertedByPyramakerzId",
                table: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_Schools_UpdatedByPyramakerzId",
                table: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_Roles_DeletedByPyramakerzId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_InsertedByPyramakerzId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_UpdatedByPyramakerzId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Role_Detailes_DeletedByPyramakerzId",
                table: "Role_Detailes");

            migrationBuilder.DropIndex(
                name: "IX_Role_Detailes_InsertedByPyramakerzId",
                table: "Role_Detailes");

            migrationBuilder.DropIndex(
                name: "IX_Role_Detailes_UpdatedByPyramakerzId",
                table: "Role_Detailes");

            migrationBuilder.DropIndex(
                name: "IX_Parents_DeletedByPyramakerzId",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_Parents_InsertedByPyramakerzId",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_Parents_UpdatedByPyramakerzId",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DeletedByPyramakerzId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_InsertedByPyramakerzId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_UpdatedByPyramakerzId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Domains_DeletedByPyramakerzId",
                table: "Domains");

            migrationBuilder.DropIndex(
                name: "IX_Domains_InsertedByPyramakerzId",
                table: "Domains");

            migrationBuilder.DropIndex(
                name: "IX_Domains_UpdatedByPyramakerzId",
                table: "Domains");

            migrationBuilder.DropIndex(
                name: "IX_Domain_Page_Details_DeletedByPyramakerzId",
                table: "Domain_Page_Details");

            migrationBuilder.DropIndex(
                name: "IX_Domain_Page_Details_InsertedByPyramakerzId",
                table: "Domain_Page_Details");

            migrationBuilder.DropIndex(
                name: "IX_Domain_Page_Details_UpdatedByPyramakerzId",
                table: "Domain_Page_Details");

            migrationBuilder.DropIndex(
                name: "IX_BusType_DeletedByPyramakerzId",
                table: "BusType");

            migrationBuilder.DropIndex(
                name: "IX_BusType_InsertedByPyramakerzId",
                table: "BusType");

            migrationBuilder.DropIndex(
                name: "IX_BusType_UpdatedByPyramakerzId",
                table: "BusType");

            migrationBuilder.DropIndex(
                name: "IX_BusStudent_DeletedByPyramakerzId",
                table: "BusStudent");

            migrationBuilder.DropIndex(
                name: "IX_BusStudent_InsertedByPyramakerzId",
                table: "BusStudent");

            migrationBuilder.DropIndex(
                name: "IX_BusStudent_UpdatedByPyramakerzId",
                table: "BusStudent");

            migrationBuilder.DropIndex(
                name: "IX_BusStatus_DeletedByPyramakerzId",
                table: "BusStatus");

            migrationBuilder.DropIndex(
                name: "IX_BusStatus_InsertedByPyramakerzId",
                table: "BusStatus");

            migrationBuilder.DropIndex(
                name: "IX_BusStatus_UpdatedByPyramakerzId",
                table: "BusStatus");

            migrationBuilder.DropIndex(
                name: "IX_BusRestrict_DeletedByPyramakerzId",
                table: "BusRestrict");

            migrationBuilder.DropIndex(
                name: "IX_BusRestrict_InsertedByPyramakerzId",
                table: "BusRestrict");

            migrationBuilder.DropIndex(
                name: "IX_BusRestrict_UpdatedByPyramakerzId",
                table: "BusRestrict");

            migrationBuilder.DropIndex(
                name: "IX_BusCompany_DeletedByPyramakerzId",
                table: "BusCompany");

            migrationBuilder.DropIndex(
                name: "IX_BusCompany_InsertedByPyramakerzId",
                table: "BusCompany");

            migrationBuilder.DropIndex(
                name: "IX_BusCompany_UpdatedByPyramakerzId",
                table: "BusCompany");

            migrationBuilder.DropIndex(
                name: "IX_BusCategory_DeletedByPyramakerzId",
                table: "BusCategory");

            migrationBuilder.DropIndex(
                name: "IX_BusCategory_InsertedByPyramakerzId",
                table: "BusCategory");

            migrationBuilder.DropIndex(
                name: "IX_BusCategory_UpdatedByPyramakerzId",
                table: "BusCategory");

            migrationBuilder.DropIndex(
                name: "IX_Bus_DeletedByPyramakerzId",
                table: "Bus");

            migrationBuilder.DropIndex(
                name: "IX_Bus_InsertedByPyramakerzId",
                table: "Bus");

            migrationBuilder.DropIndex(
                name: "IX_Bus_UpdatedByPyramakerzId",
                table: "Bus");

            migrationBuilder.DropIndex(
                name: "IX_AcademicYear_DeletedByPyramakerzId",
                table: "AcademicYear");

            migrationBuilder.DropIndex(
                name: "IX_AcademicYear_InsertedByPyramakerzId",
                table: "AcademicYear");

            migrationBuilder.DropIndex(
                name: "IX_AcademicYear_UpdatedByPyramakerzId",
                table: "AcademicYear");
        }
    }
}
