using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class EvaluationTemplateGroupQuestionAddDTO
    {
        public string EnglishTitle { get; set; }
        public string ArabicTitle { get; set; }
        public int Mark { get; set; }
        public long EvaluationTemplateGroupID { get; set; }
    }
}
