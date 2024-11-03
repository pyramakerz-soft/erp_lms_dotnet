using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models
{
    public class Employee_With_Role_Permission_View
    {
        public int EmployeeID { get; set; }
        public string User_Name { get; set; }
        public string Email { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public int DetailedPermissionID { get; set; }
        public string DetailedPermissionName { get; set; }
        public int MasterPermissionID { get; set; }
        public string MasterPermissionName { get; set; }
        public int ModuleID { get; set; }
        public string ModuleName { get; set; }
    }
}
