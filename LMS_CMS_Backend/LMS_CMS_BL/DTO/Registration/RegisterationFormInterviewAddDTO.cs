using LMS_CMS_DAL.Models.Domains.RegisterationModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Registration
{
    public class RegisterationFormInterviewAddDTO
    {
        public long RegisterationFormParentID { get; set; }
        public long InterviewTimeID { get; set; }
    }
}
