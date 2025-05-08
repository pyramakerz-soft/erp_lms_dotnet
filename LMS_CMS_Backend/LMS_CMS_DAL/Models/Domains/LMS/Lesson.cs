using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.LMS
{
    public class Lesson : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "English Title is required")]
        [StringLength(100, ErrorMessage = "English Title cannot be longer than 100 characters.")]
        public string EnglishTitle { get; set; }
        [Required(ErrorMessage = "Arabic Title is required")]
        [StringLength(100, ErrorMessage = "Arabic Title cannot be longer than 100 characters.")]
        public string ArabicTitle { get; set; }
        public string Details { get; set; }
        public int Order { get; set; }

        [ForeignKey("Subject")]
        public long SubjectID { get; set; }
        public Subject Subject { get; set; }

        [ForeignKey("SemesterWorkingWeek")]
        public long SemesterWorkingWeekID { get; set; }
        public SemesterWorkingWeek SemesterWorkingWeek { get; set; }

        public ICollection<LessonTag> LessonTags { get; set; } = new HashSet<LessonTag>();
        public ICollection<LessonActivity> LessonActivities { get; set; } = new HashSet<LessonActivity>();
        public ICollection<LessonResource> LessonResources { get; set; } = new HashSet<LessonResource>();
        public ICollection<QuestionBank> QuestionBanks { get; set; } = new HashSet<QuestionBank>();
    }
}
