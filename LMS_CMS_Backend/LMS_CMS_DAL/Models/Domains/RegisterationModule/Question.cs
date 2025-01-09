using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.RegisterationModule
{
    public class Question: AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public string? Video { get; set; }


        [ForeignKey("mCQQuestionOption")]
        [Required]
        public long? CorrectAnswerID { get; set; }
        public MCQQuestionOption? mCQQuestionOption { get; set; }

        [ForeignKey("QuestionType")]
        [Required]
        public long QuestionTypeID { get; set; }
        public QuestionType QuestionType { get; set; }

        [ForeignKey("test")]
        [Required]
        public long TestID { get; set; }
        public Test test { get; set; }

        public ICollection<RegisterationFormTestAnswer> RegisterationFormTestAnswers { get; set; } = new HashSet<RegisterationFormTestAnswer>();
        public ICollection<MCQQuestionOption> MCQQuestionOptions { get; set; } = new HashSet<MCQQuestionOption>();

    }
}
