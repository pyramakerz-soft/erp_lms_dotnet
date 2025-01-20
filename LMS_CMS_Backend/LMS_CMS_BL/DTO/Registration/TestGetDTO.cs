using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Registration
{
    public class TestGetDTO
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
        public string SchoolName { get; set; }
        public string SubjectName { get; set; }
        public string GradeName { get; set; }
        public string AcademicYearName { get; set; }
        public string? RegistrationTestState { get; set; }
        public long? RegistrationTestStateId { get; set; }
        public double? RegistrationTestMark { get; set; }
        public bool? RegistrationTestVisibleToParent { get; set; }
        public long? RegistrationTestID { get; set; }
        public long? InsertedByUserId { get; set; }

    }
}
