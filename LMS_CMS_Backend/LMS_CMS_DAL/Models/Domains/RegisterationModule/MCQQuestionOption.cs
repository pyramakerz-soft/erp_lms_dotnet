using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.RegisterationModule
{
    public class MCQQuestionOption
    {
        [Key]
        public long ID { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("Question")]
        [Required]
        public long Question_ID { get; set; }
        public Question Question { get; set; }
        public ICollection<RegisterationFormTestAnswer> RegisterationFormTestAnswers { get; set; } = new HashSet<RegisterationFormTestAnswer>();

    }
}
