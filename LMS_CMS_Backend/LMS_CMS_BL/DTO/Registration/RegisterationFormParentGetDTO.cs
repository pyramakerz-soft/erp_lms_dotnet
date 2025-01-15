using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Registration
{
    public class RegisterationFormParentGetDTO
    {
        public long ID { get; set; }
        public string StudentName { get; set; }
        public string Phone { get; set; }
        public string GradeID { get; set; }
        public string GradeName { get; set; }
        public string AcademicYearID { get; set; }
        public string AcademicYearName { get; set; }
        public string Email { get; set; }
        public long RegisterationFormStateID { get; set; }
        public string RegisterationFormStateName { get; set; }
        public long ParentID { get; set; }
        public string ParentName { get; set; }
        public long RegistrationFormID { get; set; }
        public string RegistrationFormName { get; set; }
        public long InsertedByUserId { get; set; }
    }
}
