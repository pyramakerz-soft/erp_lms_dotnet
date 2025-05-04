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
    public class LessonGetDTO
    {
        public long ID { get; set; } 
        public string EnglishTitle { get; set; } 
        public string ArabicTitle { get; set; }
        public string Details { get; set; }
        public int Order { get; set; }
        public long SubjectID { get; set; }
        public string SubjectEnglishName { get; set; }
        public string SubjectArabicName { get; set; }
        public long SemesterWorkingWeekID { get; set; }
        public string SemesterWorkingWeekEnglishName { get; set; }
        public string SemesterWorkingWeekArabicName { get; set; }
        public long SemesterID { get; set; }
        public long AcademicYearID { get; set; }
        public long GradeID { get; set; }
        public long SchoolID { get; set; }

        public List<TagGetDTO> Tags { get; set; }
    }
}
