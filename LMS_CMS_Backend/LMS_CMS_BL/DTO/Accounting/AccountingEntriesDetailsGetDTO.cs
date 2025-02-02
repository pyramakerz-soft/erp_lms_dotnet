using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Accounting
{
    public class AccountingEntriesDetailsGetDTO
    {
        [Key]
        public long ID { get; set; }
        public int CreditAmount { get; set; }
        public int DebitAmount { get; set; }
        public string? Note { get; set; }
        public long AccountingTreeChartID { get; set; }
        public string AccountingTreeChartName { get; set; }
        public long AccountingEntriesMasterID { get; set; }
        public string AccountingEntriesMasterName { get; set; }
        public long? SubAccountingID { get; set; }
        public string? SubAccountingName { get; set; }

    }
}
