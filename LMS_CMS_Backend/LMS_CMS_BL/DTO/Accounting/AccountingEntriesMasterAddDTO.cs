using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Accounting
{
    public class AccountingEntriesMasterAddDTO
    {
        public int? DocNumber { get; set; }
        public string Date { get; set; }
        public string? Notes { get; set; }
        public long AccountingEntriesDocTypeID { get; set; } 
    }
}
