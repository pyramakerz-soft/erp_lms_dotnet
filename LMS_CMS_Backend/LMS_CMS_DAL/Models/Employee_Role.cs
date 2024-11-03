using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models
{
    public class Employee_Role
    {
        public int Id { get; set; }
        public int Employee_Id { get; set; }

        public int Role_Id { get; set; }

        public Employee Employee { get; set; }

        public Role Role { get; set; }  

    }
}
