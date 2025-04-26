using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class EvaluationEmployeeStudentBookCorrectionsGetDTO
    {
        public long ID { get; set; }
        public int State { get; set; }
        public string? Note { get; set; }
        public long StudentID { get; set; }
        public string StudentEnglishName { get; set; }
        public string StudentArabicName { get; set; }
        public long EvaluationBookCorrectionID { get; set; }
        public string EvaluationBookCorrectionEnglishName { get; set; }
        public string EvaluationBookCorrectionArabicName { get; set; }
    }
}
