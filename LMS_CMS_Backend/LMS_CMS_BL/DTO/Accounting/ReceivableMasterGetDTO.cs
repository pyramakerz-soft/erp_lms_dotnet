using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Accounting
{
    public class ReceivableMasterGetDTO
    {
        public long ID { get; set; }
        public int DocNumber { get; set; }
        public string Date { get; set; }
        public string Notes { get; set; } 
        public long ReceivableDocTypesID { get; set; } 
        public string ReceivableDocTypesName { get; set; } 
        public long LinkFileID { get; set; }
        public string LinkFileName { get; set; }
        public long BankOrSaveID { get; set; }
        public string BankOrSaveName { get; set; }
        public long InsertedByUserId { get; set; }

    }
}
