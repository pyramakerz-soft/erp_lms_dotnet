using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Accounting
{
    public class ReceivableMasterAddDTO
    {
        public int? DocNumber { get; set; }
        public string Date { get; set; }
        public string? Notes { get; set; } 
        public long ReceivableDocTypesID { get; set; } 
        public long LinkFileID { get; set; }
        public long BankOrSaveID { get; set; }
    }
}
