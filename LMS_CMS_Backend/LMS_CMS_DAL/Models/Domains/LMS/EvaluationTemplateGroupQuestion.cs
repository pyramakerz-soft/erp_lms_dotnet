using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.LMS
{
    public class EvaluationTemplateGroupQuestion : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "English Title is required")]
        public string EnglishTitle { get; set; }
        [Required(ErrorMessage = "Arabic Title is required")]
        public string ArabicTitle { get; set; }
        public int Mark { get; set; }

        [ForeignKey("EvaluationTemplateGroup")]
        public long EvaluationTemplateGroupID { get; set; }
        public EvaluationTemplateGroup EvaluationTemplateGroup { get; set; }

        public ICollection<EvaluationEmployeeQuestion> EvaluationEmployeeQuestions { get; set; } = new HashSet<EvaluationEmployeeQuestion>();
    }
}
