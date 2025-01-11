using LMS_CMS_DAL.Models.Domains.RegisterationModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Registration
{
    public class RegisterationFormInterviewGetDTO
    {
        public long ID { get; set; }
        public long InterviewStateID { get; set; }
        public string InterviewStateName { get; set; }
        public long InterviewTimeID { get; set; }
        public string InterviewTimeDate { get; set; }
        public long RegisterationFormParentID { get; set; }
        public string StudentName { get; set; }
        public string Phone { get; set; }
        public string GradeID { get; set; }
        public string GradeName { get; set; }
        public string Email { get; set; }
    }
}
