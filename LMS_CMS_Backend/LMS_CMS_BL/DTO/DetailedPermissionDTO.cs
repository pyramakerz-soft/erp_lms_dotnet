using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO
{
    public class DetailedPermissionDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public MasterPermissionDTO MasterPermission { get; set; }
    }
}
