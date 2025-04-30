using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.LMS
{
    public class EvaluationEmployeeQuestion : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        public int Mark { get; set; }
        public string? Note { get; set; }
        
        [ForeignKey("EvaluationEmployee")]
        public long EvaluationEmployeeID { get; set; }
        public EvaluationEmployee EvaluationEmployee { get; set; }
        
        [ForeignKey("EvaluationTemplateGroupQuestion")]
        public long EvaluationTemplateGroupQuestionID { get; set; }
        public EvaluationTemplateGroupQuestion EvaluationTemplateGroupQuestion { get; set; }  
    }
}
