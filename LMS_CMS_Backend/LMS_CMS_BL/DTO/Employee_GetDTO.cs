using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO
{
    public class Employee_GetDTO
    {
        public long ID { get; set; }
        public string User_Name { get; set; }
        public string en_name { get; set; }
        public string ar_name { get; set; }
        public string Password { get; set; }
        public long Role_ID { get; set; }
        public long? BusCompanyID { get; set; }
        public long EmployeeTypeID { get; set; }
    }
}
