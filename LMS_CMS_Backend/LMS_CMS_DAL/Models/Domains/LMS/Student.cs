using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS_CMS_DAL.Models.Domains.AccountingModule;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_DAL.Models.Domains.ClinicModule;
using LMS_CMS_DAL.Models.Domains.ECommerce;
using LMS_CMS_DAL.Models.Domains.Inventory;
using LMS_CMS_DAL.Models.Domains.RegisterationModule;

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
        public string? NationalIDExpiredDate { get; set; }
        public long? Nationality { get; set; }

        [ForeignKey("Parent")]
        public long? Parent_Id { get; set; } 
        public Parent Parent { get; set; }

        [ForeignKey("AccountNumber")]
        public long? AccountNumberID { get; set; }

        [ForeignKey("Gender")]
        public long GenderId { get; set; }
        public Gender Gender { get; set; }

        [ForeignKey("RegistrationFormParent")]
        public long? RegistrationFormParentID { get; set; }
        public RegisterationFormParent RegistrationFormParent { get; set; }
        
        [ForeignKey("StartAcademicYear")]
        public long? StartAcademicYearID { get; set; }
        public AcademicYear StartAcademicYear { get; set; }

        public string? DateOfBirth {  get; set; }
        public string? PlaceOfBirth { get; set; }
        public string? Religion {  get; set; } 
        public string? PassportNo { get; set; }
        public string? PassportExpiredDate { get; set; }
        public string? PreviousSchool { get; set; } 
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactRelation { get; set; }
        public string? EmergencyContactMobile { get; set; }
        public string? PickUpContactName { get; set; }
        public string? PickUpContactRelation { get; set; }
        public string? PickUpContactMobile { get; set; } 
        public bool? IsRegisteredInNoor { get; set; } 
        public string? MotherName { get; set; }
        public string? MotherPassportNo { get; set; }
        public string? MotherPassportExpireDate { get; set; }
        public string? MotherNationalID { get; set; }
        public string? MotherNationalIDExpiredDate { get; set; }
        public string? MotherQualification { get; set; }
        public string? MotherWorkPlace { get; set; }
        public string? MotherEmail { get; set; }
        public string? MotherExperiences { get; set; }
        public string? MotherProfession { get; set; }
        public string? MotherMobile { get; set; }
        public string? GuardianRelation { get; set; }
        public string? AdmissionDate { get; set; }

        public AccountingTreeChart AccountNumber { get; set; }
        public ICollection<BusStudent> BusStudents { get; set; } = new HashSet<BusStudent>();
        public ICollection<StudentAcademicYear> StudentAcademicYears { get; set; } = new HashSet<StudentAcademicYear>();
        public ICollection<EmployeeStudent> EmployeeStudents { get; set; } = new HashSet<EmployeeStudent>();
        public ICollection<InstallmentDeductionMaster> InstallmentDeductionMasters { get; set; } = new HashSet<InstallmentDeductionMaster>();
        public ICollection<FeesActivation> FeesActivations { get; set; } = new HashSet<FeesActivation>();
        public ICollection<InventoryMaster> InventoryMaster { get; set; } = new HashSet<InventoryMaster>();
        public ICollection<Cart> Carts { get; set; } = new HashSet<Cart>();
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public ICollection<StudentHygieneTypes> StudentHygieneTypes { get; set; } = new HashSet<StudentHygieneTypes>();
        public ICollection<EvaluationEmployeeStudentBookCorrection> EvaluationEmployeeStudentBookCorrections { get; set; } = new HashSet<EvaluationEmployeeStudentBookCorrection>();
        public ICollection<StudentMedal> StudentMedals { get; set; } = new HashSet<StudentMedal>();
        public ICollection<StudentPerformance> StudentPerformances { get; set; } = new HashSet<StudentPerformance>();
        public ICollection<DailyPerformance> DailyPerformance { get; set; } = new HashSet<DailyPerformance>();

    }
}
