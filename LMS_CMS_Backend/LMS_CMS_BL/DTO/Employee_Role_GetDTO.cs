using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO
{
    public class Employee_Role_GetDTO
    {
        public int Id { get; set; }
        public int Employee_Id { get; set; }
        public string Employee_Name { get; set; }
        public int Role_Id { get; set; }
        public string Role_Name { get; set; }
    }
}
