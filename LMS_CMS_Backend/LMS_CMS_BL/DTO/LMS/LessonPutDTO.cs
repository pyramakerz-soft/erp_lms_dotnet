using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class LessonPutDTO
    {
        public long ID { get; set; }
        [Required(ErrorMessage = "English Title is required")]
        [StringLength(100, ErrorMessage = "English Title cannot be longer than 100 characters.")]
        public string EnglishTitle { get; set; }
        [Required(ErrorMessage = "Arabic Title is required")]
        [StringLength(100, ErrorMessage = "Arabic Title cannot be longer than 100 characters.")]
        public string ArabicTitle { get; set; }
        public string Details { get; set; }
        public int Order { get; set; }
        public long SubjectID { get; set; }
        public long SemesterWorkingWeekID { get; set; }

        public List<string>? TagNames { get; set; }
        public List<long>? TagIDs { get; set; }
    }
}
