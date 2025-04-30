using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.LMS
{
    public class LessonLive : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        public int Period { get; set; }
        public string LiveLink { get; set; }
        public string? RecordLink { get; set; }

        [ForeignKey("WeekDay")]
        public long WeekDayID { get; set; }
        public Days WeekDay { get; set; }

        [ForeignKey("Classroom")]
        public long ClassroomID { get; set; }
        public Classroom Classroom { get; set; }

        [ForeignKey("Subject")]
        public long SubjectID { get; set; }
        public Subject Subject { get; set; }
    }
}
