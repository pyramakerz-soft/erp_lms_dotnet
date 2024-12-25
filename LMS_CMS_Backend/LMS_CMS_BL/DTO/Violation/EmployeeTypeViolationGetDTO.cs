using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Violation
{
    public class EmployeeTypeViolationGetDTO
    {
        public long ID { get; set; }
        public long? EmployeeTypeID { get; set; }
        public long ViolationID { get; set; }
        public string ViolationsTypeName { get; set; }
        public string EmployeeTypeName { get; set; }
    }
}
