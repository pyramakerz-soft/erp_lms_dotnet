using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class StudentAcademicYearGetDTO
    {
        public long ID { get; set; }
        public long StudentID { get; set; }
        public string StudentName { get; set; }
        public long SchoolID { get; set; }
        public string SchoolName { get; set; }
        public long ClassID { get; set; }
        public string ClassName { get; set; }
        public long GradeID { get; set; }
        public string GradeName { get; set; }
        public long SectionId { get; set; }
        public string SectionName { get; set; }
    }
}
