using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.LMS
{
    public class EvaluationEmployeeQuestionAddDTO
    {
        public long EvaluationTemplateGroupQuestionID { get; set; }
        public int Mark { get; set; }
        public string? Note { get; set; }
    }
}
