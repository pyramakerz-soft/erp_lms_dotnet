using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Inventory
{
    public class InventoryMasterEditDTO
    {
        public long ID { get; set; }
        public int InvoiceNumber { get; set; }
        public string Date { get; set; }
        public bool IsCash { get; set; }
        public bool IsVisa { get; set; }
        public int? CashAmount { get; set; }
        public int? VisaAmount { get; set; }
        public int Remaining { get; set; }
        public int Total { get; set; }
        public string? Notes { get; set; }
        public List<IFormFile>? NewAttachments { get; set; }
        public List<string>? DeletedAttachments { get; set; }
        public long StoreID { get; set; }
        public long FlagId { get; set; }
        public long StudentID { get; set; }
        public long? SaveID { get; set; }
        public long? BankID { get; set; }
    }
}
