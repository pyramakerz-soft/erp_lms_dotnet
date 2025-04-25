using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class EvaluationTemplateGroupEditDTO
    {
        public long ID { get; set; }
        public string EnglishTitle { get; set; }
        public string ArabicTitle { get; set; }
        public long EvaluationTemplateID { get; set; }
    }
}
