using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_DAL.Models.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class LessonLiveAddDTO
    {
        public int Period { get; set; }
        public string LiveLink { get; set; }
        public string? RecordLink { get; set; } 
        public long WeekDayID { get; set; }  
        public long ClassroomID { get; set; } 
        public long SubjectID { get; set; }
    }
}
