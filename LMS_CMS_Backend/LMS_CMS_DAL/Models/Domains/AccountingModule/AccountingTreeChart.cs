using LMS_CMS_DAL.Models.Domains.LMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.AccountingModule
{
    public class AccountingTreeChart : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }
        public int Level { get; set; }

        [ForeignKey("SubType")]
        public long SubTypeID { get; set; }
        [ForeignKey("EndType")]
        public long EndTypeID { get; set; }
        [ForeignKey("Parent")]
        public long? MainAccountNumberID { get; set; }
        [ForeignKey("LinkFile")]
        public long? LinkFileID { get; set; }

        public AccountingTreeChart Parent { get; set; }
        public LinkFile LinkFile { get; set; }
        public SubType SubType { get; set; }
        public EndType EndType { get; set; }
        public ICollection<AccountingTreeChart> ChildAccountingTreeCharts { get; set; } = new List<AccountingTreeChart>();
        public ICollection<Credit> Credits { get; set; } = new List<Credit>();
        public ICollection<Debit> Debits { get; set; } = new List<Debit>();
        public ICollection<Income> Incomes { get; set; } = new List<Income>();
        public ICollection<Outcome> Outcomes { get; set; } = new List<Outcome>();
        public ICollection<Save> Saves { get; set; } = new List<Save>();
        public ICollection<Asset> Assets { get; set; } = new List<Asset>();
        public ICollection<TuitionFeesType> TuitionFeesTypes { get; set; } = new List<TuitionFeesType>();
        public ICollection<TuitionDiscountType> TuitionDiscountTypes { get; set; } = new List<TuitionDiscountType>();
        public ICollection<Bank> Banks { get; set; } = new List<Bank>();
        public ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
