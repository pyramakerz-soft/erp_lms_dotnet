using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.AccountingModule
{
    public class Supplier : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }
        public string Website { get; set; }
        public string Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string? Phone3 { get; set; }
        public string? ContactPerson { get; set; }
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public long CountryID { get; set; }
        public string CommercialRegister { get; set; }
        public string TaxCard { get; set; }

        [ForeignKey("AccountNumber")]
        public long AccountNumberID { get; set; }
        public AccountingTreeChart AccountNumber { get; set; }
    }
}
