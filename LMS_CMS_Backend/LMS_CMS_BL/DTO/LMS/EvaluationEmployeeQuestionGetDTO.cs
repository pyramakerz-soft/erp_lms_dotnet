using LMS_CMS_DAL.Models.Domains.LMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class EvaluationEmployeeQuestionGetDTO
    {
        public long ID { get; set; }
        public int Mark { get; set; }
        public string? Note { get; set; }
        public long EvaluationTemplateGroupQuestionID { get; set; }
        public string QuestionEnglishTitle { get; set; } 
        public string QuestionArabicTitle { get; set; }
    }
}
