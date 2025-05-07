using LMS_CMS_DAL.Models.Domains.LMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class LessonActivityGetDTO
    {
        public long ID { get; set; } 
        public string EnglishTitle { get; set; } 
        public string ArabicTitle { get; set; }
        public string? AttachmentLink { get; set; }
        public int Order { get; set; }
        public string? Details { get; set; }
        public long LessonID { get; set; }
        public string LessonEnglishTitle { get; set; }
        public string LessonArabicTitle { get; set; }
        public long LessonActivityTypeID { get; set; }
        public string LessonActivityTypeEnglishName { get; set; }
        public string LessonActivityTypeArabicName { get; set; }

        public long? InsertedByUserId { get; set; }
    }
}
