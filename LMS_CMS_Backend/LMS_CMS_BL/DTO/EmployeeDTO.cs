using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO
{
    public class EmployeeDTO
    {
        public int id { get; set; }
        public string user_Name { get; set; }
        public string email { get; set; }
        public List<RoleDTO> Roles { get; set; }
    }
}
