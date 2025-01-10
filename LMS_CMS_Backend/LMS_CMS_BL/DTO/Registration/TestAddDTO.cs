using LMS_CMS_DAL.Models.Domains.LMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Registration
{
    public class TestAddDTO
    {
        [Required]
        public string Title { get; set; }
        public double TotalMark { get; set; }

        [Required]
        public long AcademicYearID { get; set; }

        [Required]
        public long SubjectID { get; set; }

        [Required]
        public long GradeID { get; set; }
    }
}
