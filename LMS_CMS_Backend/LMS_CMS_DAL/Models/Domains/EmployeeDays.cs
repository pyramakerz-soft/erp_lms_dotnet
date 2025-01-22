using LMS_CMS_DAL.Models.Domains.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains
{
    public class EmployeeDays : AuditableEntity
    {
        [Key]
        public long ID { get; set; }

        [ForeignKey("Employee")]
        public long EmployeeID { get; set; }
        public Employee Employee { get; set; }

        [ForeignKey("Day")]
        public long DayID { get; set; }
        public Days Day { get; set; }
    }
}
