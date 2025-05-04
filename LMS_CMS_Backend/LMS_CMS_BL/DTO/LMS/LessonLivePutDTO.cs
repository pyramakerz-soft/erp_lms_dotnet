using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class LessonLivePutDTO
    {
        public long ID { get; set; }
        public int Period { get; set; }
        public string LiveLink { get; set; }
        public string? RecordLink { get; set; }
        public long WeekDayID { get; set; }
        public long ClassroomID { get; set; }
        public long SubjectID { get; set; }
    }
}
