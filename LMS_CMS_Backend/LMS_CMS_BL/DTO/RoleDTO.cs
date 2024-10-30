using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO
{
    public class RoleDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<DetailedPermissionDTO> DetailedPermissions { get; set; }
    }
}
