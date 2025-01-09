using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.RegisterationModule
{
    public class RegisterationFormTestAnswer : AuditableEntity
    {
        [Key]
        public int ID { get; set; }
        public string? EssayAnswer { get; set; }
        [ForeignKey("Question")]
        [Required]
        public long QuestionID { get; set; }
        public Question Question { get; set; }

        [ForeignKey("MCQQuestionOption")]
        [Required]
        public long? AnswerID { get; set; }
        public MCQQuestionOption MCQQuestionOption { get; set; }

        [ForeignKey("RegisterationFormParent")]
        [Required]
        public long RegisterationFormParentID { get; set; }
        public RegisterationFormParent RegisterationFormParent { get; set; }

    }
}
