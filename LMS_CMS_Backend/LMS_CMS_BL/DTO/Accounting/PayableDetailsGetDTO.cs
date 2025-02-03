using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Accounting
{
    public class PayableDetailsGetDTO
    {
        public long ID { get; set; }
        public int Amount { get; set; }
        public string Notes { get; set; }
        public long PayableMasterID { get; set; }
        public long LinkFileID { get; set; }
        public string LinkFileName { get; set; }
        public long LinkFileTypeID { get; set; }
        public string LinkFileTypeName { get; set; }
        public long InsertedByUserId { get; set; }
    }
}
