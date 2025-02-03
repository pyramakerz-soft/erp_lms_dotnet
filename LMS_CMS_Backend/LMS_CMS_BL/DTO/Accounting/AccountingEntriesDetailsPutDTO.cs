using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Accounting
{
    public class AccountingEntriesDetailsPutDTO
    {
        public long ID { get; set; }
        public int CreditAmount { get; set; }
        public int DebitAmount { get; set; }
        public string? Note { get; set; }
        public long AccountingTreeChartID { get; set; }
        public long AccountingEntriesMasterID { get; set; }
        public long? SubAccountingID { get; set; }
    }
}
