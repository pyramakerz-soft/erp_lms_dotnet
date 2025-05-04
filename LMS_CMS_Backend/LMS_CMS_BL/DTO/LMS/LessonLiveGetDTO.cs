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
    public class LessonLiveGetDTO
    {
        public long ID { get; set; }
        public int Period { get; set; }
        public string LiveLink { get; set; }
        public string RecordLink { get; set; }
        public long WeekDayID { get; set; }
        public string WeekDayName { get; set; }
        public long ClassroomID { get; set; }
        public string ClassroomName { get; set; }
        public long SubjectID { get; set; }
        public string SubjectEnglishName { get; set; }
        public string SubjectArabicName { get; set; }
    }
}
