using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS_CMS_DAL.Models.Domains.LMS;

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
        [NotMapped]
        public Credit Credit { get; set; }
        [NotMapped]
        public Debit Debit { get; set; }
        [NotMapped]
        public Income Income { get; set; }
        [NotMapped]
        public Outcome Outcome { get; set; }
        [NotMapped]
        public Save Save { get; set; }
        [NotMapped]
        public Asset Asset { get; set; }
        [NotMapped]
        public TuitionFeesType TuitionFeesType { get; set; }
        [NotMapped]
        public TuitionDiscountType TuitionDiscountType { get; set; }
        [NotMapped]
        public Bank Bank { get; set; }
        [NotMapped]
        public Supplier Supplier { get; set; }
        [NotMapped]
        public Employee Employee { get; set; }
        [NotMapped]
        public Student Student { get; set; }
    }
}
