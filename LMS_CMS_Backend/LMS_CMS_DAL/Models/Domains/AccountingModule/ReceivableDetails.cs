using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.AccountingModule
{
    public class ReceivableDetails : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        public int Amount { get; set; }
        public string? Notes { get; set; }
        [ForeignKey("ReceivableMaster")]
        public long ReceivableMasterID { get; set; }
        [ForeignKey("LinkFile")]
        public long LinkFileID { get; set; }
        public long LinkFileTypeID { get; set; }
         
        public ReceivableMaster ReceivableMaster { get; set; }
        public LinkFile LinkFile { get; set; }
        public Credit Credit { get; set; }
        public Debit Debit { get; set; }
        public Income Income { get; set; }
        public Outcome Outcome { get; set; }
        public Save Save { get; set; }
        public Asset Asset { get; set; }
        public TuitionFeesType TuitionFeesType { get; set; }
        public TuitionDiscountType TuitionDiscountType { get; set; }
        public Bank Bank { get; set; }
        public Supplier Supplier { get; set; }
    }
}
