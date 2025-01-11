using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Registration
{
    public class InterviewTimeTablePutDTO
    {
        public long ID { get; set; }
        public long AcademicYearID { get; set; }
        public TimeOnly FromTime { get; set; } // 05:30:00
        public TimeOnly ToTime { get; set; }
        public int Capacity { get; set; }
    }
}
