using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.AccountingModule
{
    public class ReceivableMaster : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        public int? DocNumber { get; set; }
        public string Date { get; set; }
        public string Notes { get; set; }
        [ForeignKey("ReceivableDocType")]   
        public long ReceivableDocTypesID { get; set; }
        [ForeignKey("LinkFile")]
        public long LinkFileID { get; set; }
        public long BankOrSaveID { get; set; }

        public Bank Bank { get; set; }
        public Save Save { get; set; }
        public ReceivableDocType ReceivableDocType { get; set; }
        public LinkFile LinkFile { get; set; }

        public ICollection<ReceivableDetails> ReceivableDetails { get; set; } = new HashSet<ReceivableDetails>();
    }
}
