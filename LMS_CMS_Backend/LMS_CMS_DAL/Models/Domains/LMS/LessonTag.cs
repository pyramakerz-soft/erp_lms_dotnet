using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.LMS
{
    public class LessonTag : AuditableEntity
    {
        [Key]
        public long ID { get; set; }

        [ForeignKey("Lesson")]
        public long LessonID { get; set; }
        public Lesson Lesson { get; set; }

        [ForeignKey("Tag")]
        public long TagID { get; set; }
        public Tag Tag { get; set; }
    }
}
