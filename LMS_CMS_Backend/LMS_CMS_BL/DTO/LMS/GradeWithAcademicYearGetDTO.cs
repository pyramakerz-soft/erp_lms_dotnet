using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class GradeWithAcademicYearGetDTO
    {
        public long GradeID { get; set; }
        public string GradeName { get; set; }
        public long AcademicYearID { get; set; }
        public string AcademicYearName { get; set; }
    }
}
