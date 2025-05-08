using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.LMS
{
    public class Tag : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        public ICollection<LessonTag> LessonTags { get; set; } = new HashSet<LessonTag>();
        public ICollection<QuestionBankTags> QuestionBankTags { get; set; } = new HashSet<QuestionBankTags>();

    }
}
