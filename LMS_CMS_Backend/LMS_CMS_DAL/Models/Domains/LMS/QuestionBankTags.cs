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
    public class QuestionBankTags : AuditableEntity
    {
        [Key]
        public long ID { get; set; }

        [ForeignKey("QuestionBank")]
        public long QuestionBankID { get; set; }
        public QuestionBank QuestionBank { get; set; }

        [ForeignKey("Tag")]
        public long TagID { get; set; }
        public Tag Tag { get; set; }
    }
}
