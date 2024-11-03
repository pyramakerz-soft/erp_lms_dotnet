using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models
{
    public class School_Roles
    {
        public int ID { get; set; }
        public int School_Id { get; set; }
        public int Role_Id { get; set; }
        public School School { get; set; }
        public Role Role { get; set; }
    }
}
