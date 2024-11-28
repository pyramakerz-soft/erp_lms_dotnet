using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO
{
    public class StudentGetDTO
    {
        public long ID { get; set; }
        public string User_Name { get; set; }
        public string en_name { get; set; }
        public string ar_name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
