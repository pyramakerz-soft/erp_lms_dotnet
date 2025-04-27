using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.LMS
{
    public class EvaluationEmployee : AuditableEntity
    {
        [Key]
        public long ID { get; set; } 
        public DateOnly Date { get; set; }
        public int Period { get; set; }
        public string? Narration { get; set; }
        public string? Feedback { get; set; }
        
        [ForeignKey("Evaluator")]
        public long EvaluatorID { get; set; }
        public Employee Evaluator { get; set; }
        
        [ForeignKey("Evaluated")]
        public long EvaluatedID { get; set; }
        public Employee Evaluated { get; set; }

        [ForeignKey("EvaluationTemplate")]
        public long EvaluationTemplateID { get; set; }
        public EvaluationTemplate EvaluationTemplate { get; set; }
        
        [ForeignKey("Classroom")]
        public long ClassroomID { get; set; }
        public Classroom Classroom { get; set; }

        public ICollection<EvaluationEmployeeQuestion> EvaluationEmployeeQuestions { get; set; } = new HashSet<EvaluationEmployeeQuestion>();
        public ICollection<EvaluationEmployeeStudentBookCorrection> EvaluationEmployeeStudentBookCorrections { get; set; } = new HashSet<EvaluationEmployeeStudentBookCorrection>();
    }
}
