using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Registration
{
    public class InterviewTimeTableAddDTO
    {
        public long AcademicYearID { get; set; }
        public DateOnly FromDate { get; set; } // "2025-01-11"
        public DateOnly ToDate { get; set; }
        public List<DaysEnum> Days { get; set; } // Note (Saturday OR saturday OR 0)
        public TimeOnly FromTime { get; set; } // 05:30:00
        public TimeOnly ToTime { get; set; }
        public int Capacity { get; set; }
    }
}
