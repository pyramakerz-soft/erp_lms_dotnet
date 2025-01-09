using LMS_CMS_DAL.Models.Domains.LMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.RegisterationModule
{
    public class RegisterationFormParent : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        // Those 3 fields are only to make it easy for us when retrieving the data
        public string StudentName { get; set; }
        public string Phone { get; set; }
        public string GradeID { get; set; }
        [ForeignKey("RegisterationFormState")]
        public long? RegisterationFormStateID { get; set; }
        public RegisterationFormState? RegisterationFormState { get; set; }
        [ForeignKey("Parent")]
        public long ParentID { get; set; }
        public Parent Parent { get; set; }
        [ForeignKey("RegistrationForm")]
        public long RegistrationFormID { get; set; }
        public RegistrationForm RegistrationForm { get; set; }
        public ICollection<RegisterationFormSubmittion> RegisterationFormSubmittions { get; set; } = new HashSet<RegisterationFormSubmittion>();
        public ICollection<RegisterationFormInterview> RegisterationFormInterviews { get; set; } = new HashSet<RegisterationFormInterview>();
        public ICollection<RegisterationFormTest> RegisterationFormTests { get; set; } = new HashSet<RegisterationFormTest>();
        public ICollection<RegisterationFormTestAnswer> RegisterationFormTestAnswers { get; set; } = new HashSet<RegisterationFormTestAnswer>();

    }
}
