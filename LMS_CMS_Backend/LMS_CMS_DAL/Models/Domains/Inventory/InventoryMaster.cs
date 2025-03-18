using LMS_CMS_DAL.Models.Domains.AccountingModule;
using LMS_CMS_DAL.Models.Domains.LMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.Inventory
{
    public class InventoryMaster : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        //[Required(ErrorMessage = "Name is required")]
        //[StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        //public string Name { get; set; }
        public string InvoiceHead { get; set; } = "فاتورة ضريبية مبسطة";
        public int InvoiceNumber { get; set; }
        public string Date { get; set; }
        public bool IsCash { get; set; }
        public bool IsVisa { get; set; }
        public decimal? CashAmount { get; set; }
        public decimal? VisaAmount { get; set; }
        public decimal Remaining { get; set; }
        public decimal Total { get; set; }
        public decimal? Vat { get; set; }
        public decimal? TotalWithVat { get; set; }
        public decimal? VatAmount { get; set; }
        public string? DigestValue { get; set; }
        public string? SignatureValue { get; set; }
        public string? PublicKeyCertificate { get; set; }
        public string? StampCertificate { get; set; }
        public string? XmlInvoiceFile { get; set; }
        public string? Notes { get; set; }
        public string? QRCode { get; set; }
        public List<string>? Attachments { get; set; }

        [ForeignKey("Store")]
        public long StoreID { get; set; }
        
        [ForeignKey("Student")]
        public long? StudentID { get; set; }
        
        [ForeignKey("Save")]
        public long? SaveID { get; set; }
        
        [ForeignKey("Bank")]
        public long? BankID { get; set; }

        [ForeignKey("InventoryFlags")]
        public long FlagId { get; set; }

        [ForeignKey("Supplier")]
        public long? SupplierId { get; set; }

        [ForeignKey("StoreToTransform")]
        public long? StoreToTransformId { get; set; }
        public InventoryFlags InventoryFlags { get; set; }
        public Store Store { get; set; }
        public Student? Student { get; set; }
        public Save? Save { get; set; }
        public Bank? Bank { get; set; }
        public Supplier? Supplier { get; set; }
        public Store? StoreToTransform { get; set; }
        public ICollection<InventoryDetails> InventoryDetails { get; set; } = new HashSet<InventoryDetails>();
    }
}
