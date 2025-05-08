using LMS_CMS_DAL.Models.Domains.RegisterationModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.LMS
{
    public class QuestionBankAddDTO
    {
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Video { get; set; }
        public int DifficultyLevel { get; set; }
        public double Mark { get; set; }
        public string? EssayAnswer { get; set; }
        public long LessonID { get; set; }
        public long BloomLevelID { get; set; }
        public long DokLevelID { get; set; }
        public long QuestionTypeID { get; set; }
        public long? CorrectAnswerID { get; set; }
    }
}
