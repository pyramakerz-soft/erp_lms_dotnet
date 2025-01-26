using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS_CMS_DAL.Models.Domains.LMS;

namespace LMS_CMS_DAL.Models.Domains.AccountingModule
{
    public class EmployeeStudent
    {
        [Key]
        public long ID { get; set; }
        [ForeignKey("Student")]
        public long StudentID { get; set; }
        [ForeignKey("employee")]
        public long EmployeeID { get; set; }
        public Student Student { get; set; }
        public Employee employee { get; set; }
    }
}
