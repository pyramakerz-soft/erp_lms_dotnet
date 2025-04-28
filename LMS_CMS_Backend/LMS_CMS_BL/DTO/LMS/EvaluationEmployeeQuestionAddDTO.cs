using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class EvaluationEmployeeQuestionAddDTO
    {
        public long EvaluationTemplateGroupQuestionID { get; set; }
        public int Mark { get; set; }
        public string? Note { get; set; }
    }
}
