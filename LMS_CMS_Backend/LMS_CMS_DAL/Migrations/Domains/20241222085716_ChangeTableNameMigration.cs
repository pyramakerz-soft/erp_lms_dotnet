using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class ChangeTableNameMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYear_Employees_DeletedByUserId",
                table: "AcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYear_Employees_InsertedByUserId",
                table: "AcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYear_Employees_UpdatedByUserId",
                table: "AcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYear_Schools_SchoolID",
                table: "AcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_Building_Schools_SchoolID",
                table: "Building");

            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Employees_DeletedByUserId",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Employees_DriverAssistantID",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Employees_DriverID",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Employees_InsertedByUserId",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Employees_UpdatedByUserId",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCategory_Employees_DeletedByUserId",
                table: "BusCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCategory_Employees_InsertedByUserId",
                table: "BusCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCategory_Employees_UpdatedByUserId",
                table: "BusCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Employees_DeletedByUserId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Employees_InsertedByUserId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Employees_UpdatedByUserId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BusDistrict_Employees_DeletedByUserId",
                table: "BusDistrict");

            migrationBuilder.DropForeignKey(
                name: "FK_BusDistrict_Employees_InsertedByUserId",
                table: "BusDistrict");

            migrationBuilder.DropForeignKey(
                name: "FK_BusDistrict_Employees_UpdatedByUserId",
                table: "BusDistrict");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStatus_Employees_DeletedByUserId",
                table: "BusStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStatus_Employees_InsertedByUserId",
                table: "BusStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStatus_Employees_UpdatedByUserId",
                table: "BusStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStudent_Employees_DeletedByUserId",
                table: "BusStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStudent_Employees_InsertedByUserId",
                table: "BusStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStudent_Employees_UpdatedByUserId",
                table: "BusStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStudent_Students_StudentID",
                table: "BusStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusType_Employees_DeletedByUserId",
                table: "BusType");

            migrationBuilder.DropForeignKey(
                name: "FK_BusType_Employees_InsertedByUserId",
                table: "BusType");

            migrationBuilder.DropForeignKey(
                name: "FK_BusType_Employees_UpdatedByUserId",
                table: "BusType");

            migrationBuilder.DropForeignKey(
                name: "FK_Class_Employees_DeletedByUserId",
                table: "Class");

            migrationBuilder.DropForeignKey(
                name: "FK_Class_Employees_InsertedByUserId",
                table: "Class");

            migrationBuilder.DropForeignKey(
                name: "FK_Class_Employees_UpdatedByUserId",
                table: "Class");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAttachment_Employees_EmployeeID",
                table: "EmployeeAttachment");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_BusCompany_BusCompanyID",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeType_EmployeeTypeID",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_DeletedByUserId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_InsertedByUserId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_UpdatedByUserId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Roles_Role_ID",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Employees_DeletedByUserId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Employees_InsertedByUserId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Employees_UpdatedByUserId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Pages_Pages_Page_ID",
                table: "Pages");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Employees_DeletedByUserId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Employees_InsertedByUserId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Employees_UpdatedByUserId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailes_Employees_DeletedByUserId",
                table: "Role_Detailes");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailes_Employees_InsertedByUserId",
                table: "Role_Detailes");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailes_Employees_UpdatedByUserId",
                table: "Role_Detailes");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailes_Pages_Page_ID",
                table: "Role_Detailes");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailes_Roles_Role_ID",
                table: "Role_Detailes");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Employees_DeletedByUserId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Employees_InsertedByUserId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Employees_UpdatedByUserId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Employees_DeletedByUserId",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Employees_InsertedByUserId",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Employees_UpdatedByUserId",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_SchoolType_SchoolTypeID",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Section_Schools_SchoolID",
                table: "Section");

            migrationBuilder.DropForeignKey(
                name: "FK_Semester_Employees_DeletedByUserId",
                table: "Semester");

            migrationBuilder.DropForeignKey(
                name: "FK_Semester_Employees_InsertedByUserId",
                table: "Semester");

            migrationBuilder.DropForeignKey(
                name: "FK_Semester_Employees_UpdatedByUserId",
                table: "Semester");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAcademicYear_Employees_DeletedByUserId",
                table: "StudentAcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAcademicYear_Employees_InsertedByUserId",
                table: "StudentAcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAcademicYear_Employees_UpdatedByUserId",
                table: "StudentAcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAcademicYear_Schools_SchoolID",
                table: "StudentAcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAcademicYear_Students_StudentID",
                table: "StudentAcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Employees_DeletedByUserId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Employees_InsertedByUserId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Employees_UpdatedByUserId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Parents_Parent_Id",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schools",
                table: "Schools");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parents",
                table: "Parents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pages",
                table: "Pages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "Student");

            migrationBuilder.RenameTable(
                name: "Schools",
                newName: "School");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "Parents",
                newName: "Parent");

            migrationBuilder.RenameTable(
                name: "Pages",
                newName: "Page");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Employee");

            migrationBuilder.RenameIndex(
                name: "IX_Students_User_Name",
                table: "Student",
                newName: "IX_Student_User_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Students_UpdatedByUserId",
                table: "Student",
                newName: "IX_Student_UpdatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_Parent_Id",
                table: "Student",
                newName: "IX_Student_Parent_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Students_InsertedByUserId",
                table: "Student",
                newName: "IX_Student_InsertedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_Email",
                table: "Student",
                newName: "IX_Student_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Students_DeletedByUserId",
                table: "Student",
                newName: "IX_Student_DeletedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Schools_UpdatedByUserId",
                table: "School",
                newName: "IX_School_UpdatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Schools_SchoolTypeID",
                table: "School",
                newName: "IX_School_SchoolTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_Schools_InsertedByUserId",
                table: "School",
                newName: "IX_School_InsertedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Schools_DeletedByUserId",
                table: "School",
                newName: "IX_School_DeletedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_UpdatedByUserId",
                table: "Role",
                newName: "IX_Role_UpdatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_InsertedByUserId",
                table: "Role",
                newName: "IX_Role_InsertedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_DeletedByUserId",
                table: "Role",
                newName: "IX_Role_DeletedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Parents_User_Name",
                table: "Parent",
                newName: "IX_Parent_User_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Parents_UpdatedByUserId",
                table: "Parent",
                newName: "IX_Parent_UpdatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Parents_InsertedByUserId",
                table: "Parent",
                newName: "IX_Parent_InsertedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Parents_Email",
                table: "Parent",
                newName: "IX_Parent_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Parents_DeletedByUserId",
                table: "Parent",
                newName: "IX_Parent_DeletedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Pages_Page_ID",
                table: "Page",
                newName: "IX_Page_Page_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Pages_en_name",
                table: "Page",
                newName: "IX_Page_en_name");

            migrationBuilder.RenameIndex(
                name: "IX_Pages_ar_name",
                table: "Page",
                newName: "IX_Page_ar_name");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_User_Name",
                table: "Employee",
                newName: "IX_Employee_User_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_UpdatedByUserId",
                table: "Employee",
                newName: "IX_Employee_UpdatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_Role_ID",
                table: "Employee",
                newName: "IX_Employee_Role_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_InsertedByUserId",
                table: "Employee",
                newName: "IX_Employee_InsertedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_EmployeeTypeID",
                table: "Employee",
                newName: "IX_Employee_EmployeeTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_DeletedByUserId",
                table: "Employee",
                newName: "IX_Employee_DeletedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_BusCompanyID",
                table: "Employee",
                newName: "IX_Employee_BusCompanyID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student",
                table: "Student",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_School",
                table: "School",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parent",
                table: "Parent",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Page",
                table: "Page",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "ID");

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
                name: "FK_Building_School_SchoolID",
                table: "Building",
                column: "SchoolID",
                principalTable: "School",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Employee_DeletedByUserId",
                table: "Bus",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);

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
                name: "FK_Class_Employee_DeletedByUserId",
                table: "Class",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Class_Employee_InsertedByUserId",
                table: "Class",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Class_Employee_UpdatedByUserId",
                table: "Class",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_BusCompany_BusCompanyID",
                table: "Employee",
                column: "BusCompanyID",
                principalTable: "BusCompany",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_EmployeeType_EmployeeTypeID",
                table: "Employee",
                column: "EmployeeTypeID",
                principalTable: "EmployeeType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Employee_DeletedByUserId",
                table: "Employee",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Employee_InsertedByUserId",
                table: "Employee",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Employee_UpdatedByUserId",
                table: "Employee",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Role_Role_ID",
                table: "Employee",
                column: "Role_ID",
                principalTable: "Role",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAttachment_Employee_EmployeeID",
                table: "EmployeeAttachment",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Employee_DeletedByUserId",
                table: "Grade",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Employee_InsertedByUserId",
                table: "Grade",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Employee_UpdatedByUserId",
                table: "Grade",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Page_Page_Page_ID",
                table: "Page",
                column: "Page_ID",
                principalTable: "Page",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parent_Employee_DeletedByUserId",
                table: "Parent",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parent_Employee_InsertedByUserId",
                table: "Parent",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parent_Employee_UpdatedByUserId",
                table: "Parent",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Employee_DeletedByUserId",
                table: "Role",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Employee_InsertedByUserId",
                table: "Role",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Employee_UpdatedByUserId",
                table: "Role",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailes_Employee_DeletedByUserId",
                table: "Role_Detailes",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailes_Employee_InsertedByUserId",
                table: "Role_Detailes",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailes_Employee_UpdatedByUserId",
                table: "Role_Detailes",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailes_Page_Page_ID",
                table: "Role_Detailes",
                column: "Page_ID",
                principalTable: "Page",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailes_Role_Role_ID",
                table: "Role_Detailes",
                column: "Role_ID",
                principalTable: "Role",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_School_Employee_DeletedByUserId",
                table: "School",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_School_Employee_InsertedByUserId",
                table: "School",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_School_Employee_UpdatedByUserId",
                table: "School",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_School_SchoolType_SchoolTypeID",
                table: "School",
                column: "SchoolTypeID",
                principalTable: "SchoolType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Section_School_SchoolID",
                table: "Section",
                column: "SchoolID",
                principalTable: "School",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Semester_Employee_DeletedByUserId",
                table: "Semester",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Semester_Employee_InsertedByUserId",
                table: "Semester",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Semester_Employee_UpdatedByUserId",
                table: "Semester",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Employee_DeletedByUserId",
                table: "Student",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Employee_InsertedByUserId",
                table: "Student",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Employee_UpdatedByUserId",
                table: "Student",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Parent_Parent_Id",
                table: "Student",
                column: "Parent_Id",
                principalTable: "Parent",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAcademicYear_Employee_DeletedByUserId",
                table: "StudentAcademicYear",
                column: "DeletedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAcademicYear_Employee_InsertedByUserId",
                table: "StudentAcademicYear",
                column: "InsertedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAcademicYear_Employee_UpdatedByUserId",
                table: "StudentAcademicYear",
                column: "UpdatedByUserId",
                principalTable: "Employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAcademicYear_School_SchoolID",
                table: "StudentAcademicYear",
                column: "SchoolID",
                principalTable: "School",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAcademicYear_Student_StudentID",
                table: "StudentAcademicYear",
                column: "StudentID",
                principalTable: "Student",
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
                name: "FK_AcademicYear_School_SchoolID",
                table: "AcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_Building_School_SchoolID",
                table: "Building");

            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Employee_DeletedByUserId",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Employee_DriverAssistantID",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Employee_DriverID",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Employee_InsertedByUserId",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Employee_UpdatedByUserId",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCategory_Employee_DeletedByUserId",
                table: "BusCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCategory_Employee_InsertedByUserId",
                table: "BusCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCategory_Employee_UpdatedByUserId",
                table: "BusCategory");

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
                name: "FK_BusDistrict_Employee_DeletedByUserId",
                table: "BusDistrict");

            migrationBuilder.DropForeignKey(
                name: "FK_BusDistrict_Employee_InsertedByUserId",
                table: "BusDistrict");

            migrationBuilder.DropForeignKey(
                name: "FK_BusDistrict_Employee_UpdatedByUserId",
                table: "BusDistrict");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStatus_Employee_DeletedByUserId",
                table: "BusStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStatus_Employee_InsertedByUserId",
                table: "BusStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStatus_Employee_UpdatedByUserId",
                table: "BusStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStudent_Employee_DeletedByUserId",
                table: "BusStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStudent_Employee_InsertedByUserId",
                table: "BusStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStudent_Employee_UpdatedByUserId",
                table: "BusStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStudent_Student_StudentID",
                table: "BusStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusType_Employee_DeletedByUserId",
                table: "BusType");

            migrationBuilder.DropForeignKey(
                name: "FK_BusType_Employee_InsertedByUserId",
                table: "BusType");

            migrationBuilder.DropForeignKey(
                name: "FK_BusType_Employee_UpdatedByUserId",
                table: "BusType");

            migrationBuilder.DropForeignKey(
                name: "FK_Class_Employee_DeletedByUserId",
                table: "Class");

            migrationBuilder.DropForeignKey(
                name: "FK_Class_Employee_InsertedByUserId",
                table: "Class");

            migrationBuilder.DropForeignKey(
                name: "FK_Class_Employee_UpdatedByUserId",
                table: "Class");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_BusCompany_BusCompanyID",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_EmployeeType_EmployeeTypeID",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Employee_DeletedByUserId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Employee_InsertedByUserId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Employee_UpdatedByUserId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Role_Role_ID",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAttachment_Employee_EmployeeID",
                table: "EmployeeAttachment");

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
                name: "FK_Page_Page_Page_ID",
                table: "Page");

            migrationBuilder.DropForeignKey(
                name: "FK_Parent_Employee_DeletedByUserId",
                table: "Parent");

            migrationBuilder.DropForeignKey(
                name: "FK_Parent_Employee_InsertedByUserId",
                table: "Parent");

            migrationBuilder.DropForeignKey(
                name: "FK_Parent_Employee_UpdatedByUserId",
                table: "Parent");

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
                name: "FK_Role_Detailes_Employee_DeletedByUserId",
                table: "Role_Detailes");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailes_Employee_InsertedByUserId",
                table: "Role_Detailes");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailes_Employee_UpdatedByUserId",
                table: "Role_Detailes");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailes_Page_Page_ID",
                table: "Role_Detailes");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailes_Role_Role_ID",
                table: "Role_Detailes");

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
                name: "FK_School_SchoolType_SchoolTypeID",
                table: "School");

            migrationBuilder.DropForeignKey(
                name: "FK_Section_School_SchoolID",
                table: "Section");

            migrationBuilder.DropForeignKey(
                name: "FK_Semester_Employee_DeletedByUserId",
                table: "Semester");

            migrationBuilder.DropForeignKey(
                name: "FK_Semester_Employee_InsertedByUserId",
                table: "Semester");

            migrationBuilder.DropForeignKey(
                name: "FK_Semester_Employee_UpdatedByUserId",
                table: "Semester");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Employee_DeletedByUserId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Employee_InsertedByUserId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Employee_UpdatedByUserId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Parent_Parent_Id",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAcademicYear_Employee_DeletedByUserId",
                table: "StudentAcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAcademicYear_Employee_InsertedByUserId",
                table: "StudentAcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAcademicYear_Employee_UpdatedByUserId",
                table: "StudentAcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAcademicYear_School_SchoolID",
                table: "StudentAcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAcademicYear_Student_StudentID",
                table: "StudentAcademicYear");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Student",
                table: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK_School",
                table: "School");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parent",
                table: "Parent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Page",
                table: "Page");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employee");

            migrationBuilder.RenameTable(
                name: "Student",
                newName: "Students");

            migrationBuilder.RenameTable(
                name: "School",
                newName: "Schools");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "Parent",
                newName: "Parents");

            migrationBuilder.RenameTable(
                name: "Page",
                newName: "Pages");

            migrationBuilder.RenameTable(
                name: "Employee",
                newName: "Employees");

            migrationBuilder.RenameIndex(
                name: "IX_Student_User_Name",
                table: "Students",
                newName: "IX_Students_User_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Student_UpdatedByUserId",
                table: "Students",
                newName: "IX_Students_UpdatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Student_Parent_Id",
                table: "Students",
                newName: "IX_Students_Parent_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Student_InsertedByUserId",
                table: "Students",
                newName: "IX_Students_InsertedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Student_Email",
                table: "Students",
                newName: "IX_Students_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Student_DeletedByUserId",
                table: "Students",
                newName: "IX_Students_DeletedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_School_UpdatedByUserId",
                table: "Schools",
                newName: "IX_Schools_UpdatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_School_SchoolTypeID",
                table: "Schools",
                newName: "IX_Schools_SchoolTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_School_InsertedByUserId",
                table: "Schools",
                newName: "IX_Schools_InsertedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_School_DeletedByUserId",
                table: "Schools",
                newName: "IX_Schools_DeletedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Role_UpdatedByUserId",
                table: "Roles",
                newName: "IX_Roles_UpdatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Role_InsertedByUserId",
                table: "Roles",
                newName: "IX_Roles_InsertedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Role_DeletedByUserId",
                table: "Roles",
                newName: "IX_Roles_DeletedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Parent_User_Name",
                table: "Parents",
                newName: "IX_Parents_User_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Parent_UpdatedByUserId",
                table: "Parents",
                newName: "IX_Parents_UpdatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Parent_InsertedByUserId",
                table: "Parents",
                newName: "IX_Parents_InsertedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Parent_Email",
                table: "Parents",
                newName: "IX_Parents_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Parent_DeletedByUserId",
                table: "Parents",
                newName: "IX_Parents_DeletedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Page_Page_ID",
                table: "Pages",
                newName: "IX_Pages_Page_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Page_en_name",
                table: "Pages",
                newName: "IX_Pages_en_name");

            migrationBuilder.RenameIndex(
                name: "IX_Page_ar_name",
                table: "Pages",
                newName: "IX_Pages_ar_name");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_User_Name",
                table: "Employees",
                newName: "IX_Employees_User_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_UpdatedByUserId",
                table: "Employees",
                newName: "IX_Employees_UpdatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_Role_ID",
                table: "Employees",
                newName: "IX_Employees_Role_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_InsertedByUserId",
                table: "Employees",
                newName: "IX_Employees_InsertedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_EmployeeTypeID",
                table: "Employees",
                newName: "IX_Employees_EmployeeTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_DeletedByUserId",
                table: "Employees",
                newName: "IX_Employees_DeletedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_BusCompanyID",
                table: "Employees",
                newName: "IX_Employees_BusCompanyID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schools",
                table: "Schools",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parents",
                table: "Parents",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pages",
                table: "Pages",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYear_Employees_DeletedByUserId",
                table: "AcademicYear",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYear_Employees_InsertedByUserId",
                table: "AcademicYear",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYear_Employees_UpdatedByUserId",
                table: "AcademicYear",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYear_Schools_SchoolID",
                table: "AcademicYear",
                column: "SchoolID",
                principalTable: "Schools",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Building_Schools_SchoolID",
                table: "Building",
                column: "SchoolID",
                principalTable: "Schools",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Employees_DeletedByUserId",
                table: "Bus",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);

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
                name: "FK_Bus_Employees_InsertedByUserId",
                table: "Bus",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Employees_UpdatedByUserId",
                table: "Bus",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCategory_Employees_DeletedByUserId",
                table: "BusCategory",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCategory_Employees_InsertedByUserId",
                table: "BusCategory",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCategory_Employees_UpdatedByUserId",
                table: "BusCategory",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Employees_DeletedByUserId",
                table: "BusCompany",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Employees_InsertedByUserId",
                table: "BusCompany",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Employees_UpdatedByUserId",
                table: "BusCompany",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusDistrict_Employees_DeletedByUserId",
                table: "BusDistrict",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusDistrict_Employees_InsertedByUserId",
                table: "BusDistrict",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusDistrict_Employees_UpdatedByUserId",
                table: "BusDistrict",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStatus_Employees_DeletedByUserId",
                table: "BusStatus",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStatus_Employees_InsertedByUserId",
                table: "BusStatus",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStatus_Employees_UpdatedByUserId",
                table: "BusStatus",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStudent_Employees_DeletedByUserId",
                table: "BusStudent",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStudent_Employees_InsertedByUserId",
                table: "BusStudent",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStudent_Employees_UpdatedByUserId",
                table: "BusStudent",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStudent_Students_StudentID",
                table: "BusStudent",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusType_Employees_DeletedByUserId",
                table: "BusType",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusType_Employees_InsertedByUserId",
                table: "BusType",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusType_Employees_UpdatedByUserId",
                table: "BusType",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Class_Employees_DeletedByUserId",
                table: "Class",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Class_Employees_InsertedByUserId",
                table: "Class",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Class_Employees_UpdatedByUserId",
                table: "Class",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAttachment_Employees_EmployeeID",
                table: "EmployeeAttachment",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_BusCompany_BusCompanyID",
                table: "Employees",
                column: "BusCompanyID",
                principalTable: "BusCompany",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeType_EmployeeTypeID",
                table: "Employees",
                column: "EmployeeTypeID",
                principalTable: "EmployeeType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_DeletedByUserId",
                table: "Employees",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_InsertedByUserId",
                table: "Employees",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_UpdatedByUserId",
                table: "Employees",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Employees_DeletedByUserId",
                table: "Grade",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Employees_InsertedByUserId",
                table: "Grade",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Employees_UpdatedByUserId",
                table: "Grade",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Pages_Page_ID",
                table: "Pages",
                column: "Page_ID",
                principalTable: "Pages",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Employees_DeletedByUserId",
                table: "Parents",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Employees_InsertedByUserId",
                table: "Parents",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Employees_UpdatedByUserId",
                table: "Parents",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailes_Employees_DeletedByUserId",
                table: "Role_Detailes",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailes_Employees_InsertedByUserId",
                table: "Role_Detailes",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailes_Employees_UpdatedByUserId",
                table: "Role_Detailes",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailes_Pages_Page_ID",
                table: "Role_Detailes",
                column: "Page_ID",
                principalTable: "Pages",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailes_Roles_Role_ID",
                table: "Role_Detailes",
                column: "Role_ID",
                principalTable: "Roles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Employees_DeletedByUserId",
                table: "Roles",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Employees_InsertedByUserId",
                table: "Roles",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Employees_UpdatedByUserId",
                table: "Roles",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Employees_DeletedByUserId",
                table: "Schools",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Employees_InsertedByUserId",
                table: "Schools",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Employees_UpdatedByUserId",
                table: "Schools",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_SchoolType_SchoolTypeID",
                table: "Schools",
                column: "SchoolTypeID",
                principalTable: "SchoolType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Section_Schools_SchoolID",
                table: "Section",
                column: "SchoolID",
                principalTable: "Schools",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Semester_Employees_DeletedByUserId",
                table: "Semester",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Semester_Employees_InsertedByUserId",
                table: "Semester",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Semester_Employees_UpdatedByUserId",
                table: "Semester",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAcademicYear_Employees_DeletedByUserId",
                table: "StudentAcademicYear",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAcademicYear_Employees_InsertedByUserId",
                table: "StudentAcademicYear",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAcademicYear_Employees_UpdatedByUserId",
                table: "StudentAcademicYear",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAcademicYear_Schools_SchoolID",
                table: "StudentAcademicYear",
                column: "SchoolID",
                principalTable: "Schools",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAcademicYear_Students_StudentID",
                table: "StudentAcademicYear",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Employees_DeletedByUserId",
                table: "Students",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Employees_InsertedByUserId",
                table: "Students",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Employees_UpdatedByUserId",
                table: "Students",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Parents_Parent_Id",
                table: "Students",
                column: "Parent_Id",
                principalTable: "Parents",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
