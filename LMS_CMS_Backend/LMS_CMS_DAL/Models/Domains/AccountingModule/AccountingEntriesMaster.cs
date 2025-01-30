using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.AccountingModule
{
    public class AccountingEntriesMaster : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        public int? DocNumber { get; set; }
        public string Date { get; set; }
        public string Notes { get; set; }
        [ForeignKey("AccountingEntriesDocType")]
        public long AccountingEntriesDocTypeID { get; set; }
         
        public AccountingEntriesDocType AccountingEntriesDocType { get; set; } 

        public ICollection<AccountingEntriesDetails> AccountingEntriesDetails { get; set; } = new HashSet<AccountingEntriesDetails>();
    }
}
