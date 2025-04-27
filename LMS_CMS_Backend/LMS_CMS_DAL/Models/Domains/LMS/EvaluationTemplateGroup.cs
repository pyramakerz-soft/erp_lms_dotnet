using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.LMS
{
    public class EvaluationTemplateGroup : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "English Title is required")]
        public string EnglishTitle { get; set; }
        [Required(ErrorMessage = "Arabic Title is required")]
        public string ArabicTitle { get; set; }

        [ForeignKey("EvaluationTemplate")]
        public long EvaluationTemplateID { get; set; }
        public EvaluationTemplate EvaluationTemplate { get; set; }

        public ICollection<EvaluationTemplateGroupQuestion> EvaluationTemplateGroupQuestions { get; set; } = new HashSet<EvaluationTemplateGroupQuestion>();
    }
}
