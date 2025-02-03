using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Accounting
{
    public class ReceivableDetailsAddDTO
    {
        public int Amount { get; set; }
        public string? Notes { get; set; } 
        public long ReceivableMasterID { get; set; } 
        public long LinkFileID { get; set; }
        public long LinkFileTypeID { get; set; }
    }
}
