using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS_CMS_DAL.Models.Domains.AccountingModule;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_DAL.Models.Domains.Inventory;

namespace LMS_CMS_DAL.Models.Domains.LMS
{
    public class Student : AuditableEntity
    {
        [Key]
        public long ID { get; set; }

        [Required(ErrorMessage = "User_Name is required")]
        [StringLength(100, ErrorMessage = "Username cannot be longer than 100 characters.")]
        public string User_Name { get; set; }


        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string en_name { get; set; }

        [StringLength(100, ErrorMessage = "لا يمكن أن يكون الاسم أطول من 100 حرف")]
        public string? ar_name { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        public string Password { get; set; }

        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Note { get; set; }
        public string? NationalID { get; set; }
        public long? Nationality { get; set; }

        [ForeignKey("Parent")]
        public long Parent_Id { get; set; }

        public Parent Parent { get; set; }

        [ForeignKey("AccountNumber")]
        public long? AccountNumberID { get; set; }

        public AccountingTreeChart AccountNumber { get; set; }
        public ICollection<BusStudent> BusStudents { get; set; } = new HashSet<BusStudent>();
        public ICollection<StudentAcademicYear> StudentAcademicYears { get; set; } = new HashSet<StudentAcademicYear>();
        public ICollection<EmployeeStudent> EmployeeStudents { get; set; } = new HashSet<EmployeeStudent>();
        public ICollection<InstallmentDeductionMaster> InstallmentDeductionMasters { get; set; } = new HashSet<InstallmentDeductionMaster>();
        public ICollection<FeesActivation> FeesActivations { get; set; } = new HashSet<FeesActivation>();
        public ICollection<Sales> Sales { get; set; } = new HashSet<Sales>();

    }
}
