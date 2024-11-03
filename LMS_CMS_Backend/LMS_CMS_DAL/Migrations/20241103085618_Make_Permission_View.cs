using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class Make_Permission_View : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW Employee_With_Role_Permission_View AS
                    SELECT 
                        e.ID AS EmployeeID,
                        e.User_Name,
                        e.Email,
                        r.ID AS RoleID,
                        r.Name AS RoleName,
                        dp.ID AS DetailedPermissionID,
                        dp.Name AS DetailedPermissionName,
                        mp.ID AS MasterPermissionID,
                        mp.Name AS MasterPermissionName,
                        m.ID AS ModuleID,
                        m.Name AS ModuleName
                    FROM 
                        Employees e
                    JOIN 
                        Employee_Roles er ON e.ID = er.Employee_ID
                    JOIN 
                        Roles r ON er.Role_ID = r.ID
                    JOIN 
                        Role_Detailed_Permissions rp ON r.ID = rp.Role_ID
                    JOIN 
                        Master_Detailes_Permissions mdp ON rp.Master_Detailed_Permissions_ID = mdp.ID
                    JOIN 
                        Master_Permissions mp ON mdp.Master_Id = mp.ID
                    JOIN 
                        Modules_Master_Permissions mmp ON mp.ID = mmp.Master_Id
                    JOIN 
                        Modules m ON mmp.Module_ID = m.ID
                    JOIN 
                        Detailed_Permissions dp ON mdp.Details_Id = dp.ID;

            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW Employee_With_Role_Permission_View");
        }
    }
}
