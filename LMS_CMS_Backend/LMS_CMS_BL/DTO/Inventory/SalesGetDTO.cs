using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Inventory
{
    public class SalesGetDTO
    {
        public long ID { get; set; }
        //public string Name { get; set; }
        public int InvoiceNumber { get; set; }
        public string Date { get; set; }
        public bool IsCash { get; set; }
        public bool IsVisa { get; set; }
        public int CashAmount { get; set; }
        public int VisaAmount { get; set; }
        public int Remaining { get; set; }
        public string Notes { get; set; }
        public long StoreID { get; set; }
        public long StudentID { get; set; }
        public long? SaveID { get; set; }
        public long? BankID { get; set; }
        public string? StoreName { get; set; }
        public string? StudentName { get; set; }
        public string? SaveName { get; set; }
        public string? BankName { get; set; }
    }
}
