using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models
{
    public class Domain_Modules_Permission_View
    {
        public int DomainID { get; set; }
        public string DomainName { get; set; }
        public int ModuleID { get; set; }
        public string ModuleName { get; set; }
        public int DetailedPermissionID { get; set; }
        public string DetailedPermissionName { get; set; }
        public int MasterPermissionID { get; set; }
        public string MasterPermissionName { get; set; }
    }
}
