using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO
{
    public class schoolRoleDTO
    {
        public int ID { get; set; }
        public int School_Id { get; set; }
        public int Role_Id { get; set; }

        public string Role_Name { get; set; }
        public string School_Name { get; set; }
    }
}
