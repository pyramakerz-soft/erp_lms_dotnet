using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class LessonImportDTO
    {
        public long FromLessonID { get; set; }
        public long ToSemesterWorkingWeekID { get; set; }
        public long SubjectID { get; set; }
    }
}
