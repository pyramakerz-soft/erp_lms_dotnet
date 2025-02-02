using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.AccountingModule
{
    public class PayableMaster : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        public int? DocNumber { get; set; }
        public string Date { get; set; }
        public string? Notes { get; set; }
        [ForeignKey("PayableDocType")]
        public long PayableDocTypeID { get; set; }
        [ForeignKey("LinkFile")]
        public long LinkFileID { get; set; }
        public long BankOrSaveID { get; set; }

        [NotMapped]
        public Bank Bank { get; set; }
        [NotMapped]
        public Save Save { get; set; }
        public PayableDocType PayableDocType { get; set; }
        public LinkFile LinkFile { get; set; }

        public ICollection<PayableDetails> PayableDetails { get; set; } = new HashSet<PayableDetails>();
    }
}
