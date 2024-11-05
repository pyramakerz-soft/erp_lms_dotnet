using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class Domain_Module_Permission_View : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW Domain_Modules_Permission_View AS
                    SELECT 
                        d.ID AS DomainID,
                        d.Name AS DomainName,
                        m.ID AS ModuleID,
                        m.Name AS ModuleName,
                        dp.ID AS DetailedPermissionID,
                        dp.Name AS DetailedPermissionName,
                        mp.ID AS MasterPermissionID,
                        mp.Name AS MasterPermissionName
                    FROM 
                        Domains d
                    JOIN 
                        Domain_Modules dr ON d.ID = dr.Domain_Id
                    JOIN 
                        Modules m ON dr.Module_Id = m.ID
                    JOIN 
                        Modules_Master_Permissions mmp ON m.ID = mmp.Module_Id
                    JOIN 
                        Master_Permissions mp ON mp.ID = mmp.Master_Id
                    JOIN 
                        Master_Detailes_Permissions mdp ON mp.ID = mdp.Master_Id
                    JOIN 
                        Detailed_Permissions dp ON mdp.Details_Id = dp.ID;
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS Domain_Modules_Permission_View");
        }
    }
}
