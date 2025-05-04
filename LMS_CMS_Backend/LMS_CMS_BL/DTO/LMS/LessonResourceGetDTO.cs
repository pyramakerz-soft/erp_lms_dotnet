using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class LessonResourceGetDTO
    {
        public long ID { get; set; }
        public string EnglishTitle { get; set; }
        public string ArabicTitle { get; set; }
        public string? AttachmentLink { get; set; } 
        public long LessonID { get; set; }
        public string LessonEnglishTitle { get; set; }
        public string LessonArabicTitle { get; set; }
        public long LessonResourceTypeID { get; set; }
        public string LessonResourceTypeEnglishName { get; set; }
        public string LessonResourceTypeArabicName { get; set; }
        public List<ClassroomGetDTO> Classrooms { get; set; }
    }
}
