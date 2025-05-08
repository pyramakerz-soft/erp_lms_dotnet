using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.LMS
{
    public class DragAndDropAnswer : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        public string Answer { get; set; }

        [ForeignKey("SubBankQuestion")]
        public long SubBankQuestionID { get; set; }
        public SubBankQuestion SubBankQuestion { get; set; }
    }
}
