using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Accounting
{
    public class PayableDetailsAddDTO
    {
        public int Amount { get; set; }
        public string? Notes { get; set; }
        public long PayableMasterID { get; set; }
        public long LinkFileID { get; set; }
        public long LinkFileTypeID { get; set; }
    }
}
