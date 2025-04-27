using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.LMS
{
    public class EvaluationBookCorrection : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "English Name is required")]
        public string EnglishName { get; set; }
        [Required(ErrorMessage = "Arabic Name is required")]
        public string ArabicName { get; set; }
        public ICollection<EvaluationEmployeeStudentBookCorrection> EvaluationEmployeeStudentBookCorrections { get; set; } = new HashSet<EvaluationEmployeeStudentBookCorrection>();
    }
}
