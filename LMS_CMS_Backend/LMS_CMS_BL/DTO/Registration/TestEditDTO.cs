using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Registration
{
    public class TestEditDTO
    {
        [Key]
        public long ID { get; set; }
        [Required]
        public string Title { get; set; }
        public double TotalMark { get; set; }
        public long SubjectID { get; set; }
        public long GradeID { get; set; }
        public long AcademicYearID { get; set; }
        public long SchoolID { get; set; }
    }
}
