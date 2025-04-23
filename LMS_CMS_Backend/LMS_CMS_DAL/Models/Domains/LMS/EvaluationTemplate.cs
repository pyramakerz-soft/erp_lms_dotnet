using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.LMS
{
    public class EvaluationTemplate : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "English Title is required")] 
        public string EnglishTitle { get; set; }
        [Required(ErrorMessage = "Arabic Title is required")] 
        public string ArabicTitle { get; set; }
        public int Weight { get; set; }
        public int AfterCount { get; set; }

        public ICollection<EvaluationTemplateGroup> EvaluationTemplateGroups { get; set; } = new HashSet<EvaluationTemplateGroup>();
        public ICollection<EvaluationEmployee> EvaluationEmployees { get; set; } = new HashSet<EvaluationEmployee>();
    }
}
