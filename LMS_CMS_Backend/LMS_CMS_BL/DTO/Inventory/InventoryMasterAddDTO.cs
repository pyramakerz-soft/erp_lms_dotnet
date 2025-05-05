using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LMS_CMS_BL.DTO.Inventory
{
    public class InventoryMasterAddDTO
    {
        //[Required(ErrorMessage = "Name is required")]
        //[StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        //public string Name { get; set; }
        public string? InvoiceNumber { get; set; }
        public string Date { get; set; }
        public bool IsCash { get; set; }
        public bool IsVisa { get; set; }
        public int? CashAmount { get; set; }
        public int? VisaAmount { get; set; }
        public int? Remaining { get; set; }
        public int Total { get; set; }
        public string? Notes { get; set; }
        public List<IFormFile>? Attachment { get; set; }
        public long StoreID { get; set; }
        public long FlagId { get; set; }
        public long? StudentID { get; set; }
        public long? SaveID { get; set; }
        public long? SupplierId { get; set; }
        public long? BankID { get; set; }
        public long? SchoolId { get; set; }
        public long? StoreToTransformId { get; set; }
        public long? SchoolPCId { get; set; }
        public List<InventoryDetailsAddDTO> InventoryDetails { get; set; }
    }
}
