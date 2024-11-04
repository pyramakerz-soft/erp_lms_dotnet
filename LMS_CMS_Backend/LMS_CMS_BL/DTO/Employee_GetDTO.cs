using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO
{
    public class Employee_GetDTO
    {
        public int ID { get; set; }
        public string User_Name { get; set; }
        public string Email { get; set; }
        public string School_Name { get; set; }
        public int School_Id { get; set; }
        public string Domain_Name { get; set; }
        public int Domain_id { get; set; }
    }
}
