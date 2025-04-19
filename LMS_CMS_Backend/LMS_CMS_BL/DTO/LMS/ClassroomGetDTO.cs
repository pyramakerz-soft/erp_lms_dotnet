using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class ClassroomGetDTO
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public long FloorID { get; set; }
        public string FloorName { get; set; }
        public long GradeID { get; set; }
        public string GradeName { get; set; }
        public long AcademicYearID { get; set; }
        public string AcademicYearName { get; set; }
        public long SchoolID { get; set; }
        public string SchoolName { get; set; }
        public long SectionID { get; set; }
        public string SectionName { get; set; }
        public long BuildingID { get; set; }
        public string BuildingName { get; set; }
        public long? InsertedByUserId { get; set; }
        public long HomeroomTeacherID { get; set; }
    }
}
