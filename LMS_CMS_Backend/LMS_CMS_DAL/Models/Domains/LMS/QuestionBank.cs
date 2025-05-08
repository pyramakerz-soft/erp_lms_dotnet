using LMS_CMS_DAL.Models.Domains.RegisterationModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.LMS
{
    public class QuestionBank : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public int DifficultyLevel { get; set; }
        public double Mark { get; set; }
        public string? EssayAnswer { get; set; }

        [ForeignKey("Lesson")]
        public long LessonID { get; set; }
        public Lesson Lesson { get; set; }

        [ForeignKey("BloomLevel")]
        public long BloomLevelID { get; set; }
        public BloomLevel BloomLevel { get; set; }


        [ForeignKey("DokLevel")]
        public long DokLevelID { get; set; }
        public DokLevel DokLevel { get; set; }

        [ForeignKey("QuestionType")]
        public long QuestionTypeID { get; set; }
        public QuestionBankType QuestionType { get; set; }

        [ForeignKey("QuestionBankOption")]
        public long? CorrectAnswerID { get; set; }
        public QuestionBankOption? QuestionBankOption { get; set; }
        public ICollection<QuestionBankTags>? QuestionBankTags { get; set; } = new HashSet<QuestionBankTags>();
        public ICollection<QuestionBankOption>? QuestionBankOptions { get; set; } = new HashSet<QuestionBankOption>();
        public ICollection<SubBankQuestion>? SubBankQuestions { get; set; } = new HashSet<SubBankQuestion>();
    }
}
