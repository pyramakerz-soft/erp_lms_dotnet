using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_DAL.Models.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class EvaluationEmployeeGetDTO
    {
        public long ID { get; set; }
        public DateOnly Date { get; set; }
        public int Period { get; set; }
        public string? Narration { get; set; }
        public string? Feedback { get; set; }
        public long EvaluatorID { get; set; }
        public String EvaluatorArabicName { get; set; }
        public String EvaluatorEnglishName { get; set; }
        public long EvaluatedID { get; set; }
        public String EvaluatedArabicName { get; set; }
        public String EvaluatedEnglishName { get; set; }
        public long EvaluationTemplateID { get; set; }
        public String EvaluationTemplateArabicTitle { get; set; }
        public String EvaluationTemplateEnglishTitle { get; set; }
        public long ClassroomID { get; set; }
        public String ClassroomName { get; set; }
    }
}
