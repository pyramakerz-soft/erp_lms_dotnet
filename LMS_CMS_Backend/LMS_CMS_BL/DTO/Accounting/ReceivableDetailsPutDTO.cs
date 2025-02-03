using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Accounting
{
    public class ReceivableDetailsPutDTO
    {
        public long ID { get; set; }
        public int Amount { get; set; }
        public string? Notes { get; set; }
        public long ReceivableMasterID { get; set; }
        public long LinkFileID { get; set; }
        public long LinkFileTypeID { get; set; }
    }
}
