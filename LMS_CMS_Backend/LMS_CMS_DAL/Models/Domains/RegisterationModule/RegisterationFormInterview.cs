using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.RegisterationModule
{
    public class RegisterationFormInterview : AuditableEntity
    {
        [ForeignKey("InterviewState")]
        public long? InterviewStateID { get; set; }
        public InterviewState? InterviewState { get; set; }
        [ForeignKey("RegisterationFormParent")]
        public long RegisterationFormParentID { get; set; }
        public RegisterationFormParent RegisterationFormParent { get; set; }
        [ForeignKey("InterviewTime")]
        public long InterviewTimeID { get; set; }
        public InterviewTime InterviewTime { get; set; }
    }
}
