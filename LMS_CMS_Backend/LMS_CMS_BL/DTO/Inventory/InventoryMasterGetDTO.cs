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
    public class InventoryMasterGetDTO
    {
        public long ID { get; set; }
        public string? InvoiceNumber { get; set; }
        public string Date { get; set; }
        public bool IsCash { get; set; }
        public bool IsVisa { get; set; }
        public int CashAmount { get; set; }
        public int VisaAmount { get; set; }
        public int Remaining { get; set; }
        public int Total { get; set; }
        public string Notes { get; set; }
        public List<string>? Attachments { get; set; }
        public long StoreID { get; set; }
        public long StudentID { get; set; }
        public long FlagId { get; set; }
        public string FlagEnName { get; set; }
        public string FlagArName { get; set; }
        public string? FlagEnTitle { get; set; }
        public string? FlagArTitle { get; set; }
        public int ItemInOut { get; set; }
        public int FlagValue { get; set; }
        public decimal? TotalWithVat { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? VatPercent { get; set; }
        public string? InvoiceHash { get; set; }
        public string? QRCode { get; set; }
        public string? uuid { get; set; }
        public string? XmlInvoiceFile { get; set; }
        public string? Status { get; set; }
        public byte? IsValid { get; set; }
        public string? QrImage { get; set; }
        public long? SaveID { get; set; }
        public long? SchoolId { get; set; }
        public long? SchoolPCs { get; set; }
        public long? SupplierId { get; set; }
        public long? BankID { get; set; }
        public long? StoreToTransformId { get; set; }
        public string? StoreName { get; set; }
        public string? StudentName { get; set; }
        public string? SaveName { get; set; }
        public string? BankName { get; set; }
        public long? InsertedByUserId { get; set; }
        public List<InventoryDetailsGetDTO> InventoryDetails { get; set; }
    }
}
