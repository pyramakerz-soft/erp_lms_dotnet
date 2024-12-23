using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Violation
{
    public class EmployeeTypeViolationGetDTO
    {
        public long ID { get; set; }
        public string ViolationsTypes { get; set; }
        public string EmployeeTypes { get; set; }
    }
}
