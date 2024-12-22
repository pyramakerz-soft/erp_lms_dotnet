using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.Violations
{
    public class EmployeeTypeViolation : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [ForeignKey("EmployeeType")]
        public long? EmployeeTypeID { get; set; }
        [ForeignKey("Violation")]
        public long ViolationID { get; set; }

        public EmployeeType EmployeeType { get; set; }
        public Violations Violation { get; set; }
    }
}
