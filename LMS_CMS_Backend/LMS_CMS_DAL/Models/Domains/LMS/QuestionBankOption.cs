using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.LMS
{
    public class QuestionBankOption : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        public string Option { get; set; }
        public int? Order { get; set; }

        [ForeignKey("QuestionBank")]
        public long QuestionBankID { get; set; }
        public QuestionBank QuestionBank { get; set; }
        public ICollection<QuestionBank>? QuestionBanks { get; set; } = new HashSet<QuestionBank>();

    }
}
