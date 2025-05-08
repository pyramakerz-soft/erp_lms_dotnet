using LMS_CMS_DAL.Models.Domains.LMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using LMS_CMS_BL.DTO.LMS;

namespace LMS_CMS_BL.DTO.LMS
{
    public class QuestionBankAddDTO
    {
        public string? Description { get; set; }
        public string? Image { get; set; }
        public IFormFile? ImageForm { get; set; }
        public int DifficultyLevel { get; set; }
        public double Mark { get; set; }
        public string? EssayAnswer { get; set; }
        public long LessonID { get; set; }
        public long BloomLevelID { get; set; }
        public long DokLevelID { get; set; }
        public long QuestionTypeID { get; set; }
        public string? CorrectAnswerName { get; set; }
        public List<int> QuestionBankTagsDTO { get; set; } = new();
        public List<QuestionBankOptionAddDTO>? QuestionBankOptionsDTO { get; set; }
        public List<SubBankQuestionAddDTO> SubBankQuestionsDTO { get; set; } = new();
    }
}
