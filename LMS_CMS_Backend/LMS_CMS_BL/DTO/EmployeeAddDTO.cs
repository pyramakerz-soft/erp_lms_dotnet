using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO
{
    public class EmployeeAddDTO
    {
        public string User_Name { get; set; }
        public string en_name { get; set; }
        public string? ar_name { get; set; }
        public string Password { get; set; }
        public string? Mobile { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? LicenseNumber { get; set; }
        public string? ExpireDate { get; set; }
        public string? Address { get; set; }
        public long Role_ID { get; set; }
        public long? BusCompanyID { get; set; }
        public long EmployeeTypeID { get; set; }
    }
}
