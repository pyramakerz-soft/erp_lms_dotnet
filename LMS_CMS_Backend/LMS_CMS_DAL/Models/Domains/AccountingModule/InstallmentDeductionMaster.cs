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
    public class InstallmentDeductionMaster : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        public int? DocNumber { get; set; }
        public string Date { get; set; }
        public string Notes { get; set; }
        [ForeignKey("Employee")]
        public long EmployeeID { get; set; }
        [ForeignKey("Student")]
        public long StudentID { get; set; }

        public Employee Employee { get; set; }
        public Student Student { get; set; }
        public ICollection<InstallmentDeductionDetails> InstallmentDeductionDetails { get; set; } = new HashSet<InstallmentDeductionDetails>();

    }
}
