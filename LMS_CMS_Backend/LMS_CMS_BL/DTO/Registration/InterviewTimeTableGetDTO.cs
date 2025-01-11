using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Registration
{
    public class InterviewTimeTableGetDTO
    {
        public long ID { get; set; }
        public string Date { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public int Capacity { get; set; }
        public int Reserved { get; set; }
        public long AcademicYearID { get; set; }
        public string AcademicYearName { get; set; }
        public long InsertedByUserId { get; set; }
    }
}
