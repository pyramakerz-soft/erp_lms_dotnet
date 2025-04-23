using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.LMS
{
    public class EvaluationEmployeeStudentBookCorrection : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        public int State { get; set; }
        public string? Note { get; set; }

        [ForeignKey("EvaluationEmployee")]
        public long EvaluationEmployeeID { get; set; }
        public EvaluationEmployee EvaluationEmployee { get; set; }

        [ForeignKey("EvaluationBookCorrection")]
        public long EvaluationBookCorrectionID { get; set; }
        public EvaluationBookCorrection EvaluationBookCorrection { get; set; }

        [ForeignKey("Student")]
        public long StudentID { get; set; }
        public Student Student { get; set; } 
    }
}
