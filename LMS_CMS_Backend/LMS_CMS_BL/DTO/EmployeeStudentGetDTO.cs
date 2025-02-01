using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO
{
    public class EmployeeStudentGetDTO
    {
        public long ID { get; set; }
        public long StudentID { get; set; }
        public long EmployeeID { get; set; }
        public string StudentName { get; set; }
        public string EmployeeName { get; set; }
    }
}
