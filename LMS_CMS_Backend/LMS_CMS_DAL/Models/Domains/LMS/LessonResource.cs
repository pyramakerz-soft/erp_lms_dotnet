using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.LMS
{
    public class LessonResource : AuditableEntity
    {
        [Key]
        public long ID { get; set; } 
        [Required(ErrorMessage = "English Title is required")]
        [StringLength(100, ErrorMessage = "English Title cannot be longer than 100 characters.")]
        public string EnglishTitle { get; set; }
        [Required(ErrorMessage = "Arabic Title is required")]
        [StringLength(100, ErrorMessage = "Arabic Title cannot be longer than 100 characters.")]
        public string ArabicTitle { get; set; }
        public string? AttachmentLink { get; set; }

        [ForeignKey("LessonResourceType")]
        public long LessonResourceTypeID { get; set; }
        public LessonResourceType LessonResourceType { get; set; }

        [ForeignKey("Lesson")]
        public long LessonID { get; set; }
        public Lesson Lesson { get; set; }

        public ICollection<LessonResourceClassroom> LessonResourceClassrooms { get; set; } = new HashSet<LessonResourceClassroom>();
    }
}
