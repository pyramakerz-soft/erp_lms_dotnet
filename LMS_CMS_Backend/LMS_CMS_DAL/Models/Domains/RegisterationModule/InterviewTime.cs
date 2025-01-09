using LMS_CMS_DAL.Models.Domains.LMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.RegisterationModule
{
    public class InterviewTime : AuditableEntity
    {
        public string Date { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public int Capacity { get; set; }
        public int Reserved { get; set; }
        [ForeignKey("AcademicYear")]
        public long AcademicYearID { get; set; }
        public AcademicYear AcademicYear { get; set; }
        public ICollection<RegisterationFormInterview> RegisterationFormInterviews { get; set; } = new HashSet<RegisterationFormInterview>();
    }
}
