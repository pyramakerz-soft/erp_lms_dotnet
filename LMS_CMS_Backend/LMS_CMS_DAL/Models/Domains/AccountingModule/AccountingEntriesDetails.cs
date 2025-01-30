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
        public string Note { get; set; }
        [ForeignKey("AccountingTreeChart")]
        public long AccountingTreeChartID { get; set; }
        [ForeignKey("AccountingEntriesMaster")]
        public long AccountingEntriesMasterID { get; set; }
        public long SubAccountingID { get; set; }

        public AccountingTreeChart AccountingTreeChart { get; set; }
        public AccountingEntriesMaster AccountingEntriesMaster { get; set; }
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
