using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class empTypeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
        name: "TempID",
        table: "EmployeeType",
        type: "bigint",
        nullable: false,
        defaultValue: 0);

            // Copy data from 'ID' to 'TempID'
            migrationBuilder.Sql("UPDATE EmployeeType SET TempID = ID");

            // Drop foreign key constraints
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeType_EmployeeTypeID",
                table: "Employees");

            // Drop primary key on 'ID'
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeType",
                table: "EmployeeType");

            // Drop the 'ID' column
            migrationBuilder.DropColumn(
                name: "ID",
                table: "EmployeeType");

            // Rename 'TempID' to 'ID'
            migrationBuilder.RenameColumn(
                name: "TempID",
                table: "EmployeeType",
                newName: "ID");

            // Recreate the primary key on the new 'ID' column
            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeType",
                table: "EmployeeType",
                column: "ID");

            // Recreate foreign key constraints
            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeType_EmployeeTypeID",
                table: "Employees",
                column: "EmployeeTypeID",
                principalTable: "EmployeeType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TempID",
                table: "EmployeeType",
                type: "bigint",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            // Copy data back from the current column
            migrationBuilder.Sql("UPDATE EmployeeType SET TempID = ID");

            // Drop the current column
            migrationBuilder.DropColumn(
                name: "ID",
                table: "EmployeeType");

            // Rename the temporary column back to the original name
            migrationBuilder.RenameColumn(
                name: "TempID",
                table: "EmployeeType",
                newName: "ID");
        }
    }
}
