using LMS_CMS_DAL.Models.Domains.LMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class EvaluationEmployeeAddDTO
    {
        public DateOnly Date { get; set; }
        public int Period { get; set; }
        public string? Narration { get; set; }
        public long EvaluatorID { get; set; }
        public long EvaluatedID { get; set; }
        public long EvaluationTemplateID { get; set; }
        public long ClassroomID { get; set; }
        public long EvaluationBookCorrectionID { get; set; }

        public List<EvaluationEmployeeQuestionAddDTO> EvaluationEmployeeQuestions { get; set; }
        public List<EvaluationEmployeeStudentBookCorrectionAddDTO> EvaluationEmployeeStudentBookCorrections { get; set; }
    }
}
