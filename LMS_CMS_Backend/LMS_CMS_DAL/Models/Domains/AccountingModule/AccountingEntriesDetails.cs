using LMS_CMS_DAL.Models.Domains.LMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.AccountingModule
{
    public class AccountingEntriesDetails : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        public int CreditAmount { get; set; }
        public int DebitAmount { get; set; }
        public string? Note { get; set; }
        [ForeignKey("AccountingTreeChart")]
        public long AccountingTreeChartID { get; set; }
        [ForeignKey("AccountingEntriesMaster")]
        public long AccountingEntriesMasterID { get; set; }
        public long? SubAccountingID { get; set; }

        public AccountingTreeChart AccountingTreeChart { get; set; }
        public AccountingEntriesMaster AccountingEntriesMaster { get; set; }
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
