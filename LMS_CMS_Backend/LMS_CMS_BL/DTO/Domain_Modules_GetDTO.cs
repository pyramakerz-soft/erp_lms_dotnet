using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO
{
    public class Domain_Modules_GetDTO
    {
        public int ID { get; set; }
        public int Domain_Id { get; set; }
        public int Module_Id { get; set; }
        public string Module_Name { get; set; }
        public string Domain_Name { get; set; }
    }
}
