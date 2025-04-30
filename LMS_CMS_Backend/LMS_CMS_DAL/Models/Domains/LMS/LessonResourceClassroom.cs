using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.LMS
{
    public class LessonResourceClassroom : AuditableEntity
    {
        [Key]
        public long ID { get; set; }

        [ForeignKey("Classroom")]
        public long ClassroomID { get; set; }
        public Classroom Classroom { get; set; }

        [ForeignKey("LessonResource")]
        public long LessonResourceID { get; set; }
        public LessonResource LessonResource { get; set; }
    }
}
