using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS_CMS_DAL.Models.Domains.AccountingModule;
using LMS_CMS_DAL.Models.Domains.Administration;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_DAL.Models.Domains.LMS;

namespace LMS_CMS_DAL.Models.Domains
{
    public class Employee : AuditableEntity
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
        public string? Mobile { get; set; }
        public string? Phone { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? LicenseNumber { get; set; }
        public string? ExpireDate { get; set; }
        public string? Address { get; set; }
        public string? NationalID { get; set; }
        public string? PassportNumber { get; set; }
        public string? ResidenceNumber { get; set; }
        public string? BirthdayDate { get; set; }
        public long? Nationality { get; set; }
        public string? DateOfAppointment { get; set; }
        public string? DateOfLeavingWork { get; set; }
        public int? MonthSalary { get; set; }
        public bool? HasAttendance { get; set; }
        public string? AttendanceTime { get; set; }
        public string? DepartureTime { get; set; }
        public float? DelayAllowance { get; set; }
        public int? AnnualLeaveBalance { get; set; }
        public int? CasualLeavesBalance { get; set; }
        public int? MonthlyLeaveRequestBalance { get; set; }
        public int? GraduationYear { get; set; }
        public string? Note { get; set; }
        public bool? CanReceiveRequest { get; set; }
        public bool? CanReceiveMessage { get; set; }


        [ForeignKey("Role")]
        [Required]
        public long Role_ID { get; set; }
        public Role Role { get; set; }


        [ForeignKey("BusCompany")]
        public long? BusCompanyID { get; set; }
        public BusCompany? BusCompany { get; set; }


        [ForeignKey("EmployeeType")]
        public long EmployeeTypeID { get; set; }
        public EmployeeType EmployeeType { get; set; }


        [ForeignKey("ReasonForLeavingWork")]
        public long? ReasonOfLeavingID { get; set; }
        public ReasonForLeavingWork ReasonForLeavingWork { get; set; }


        [ForeignKey("AccountNumber")]
        public long? AccountNumberID { get; set; }
        public AccountingTreeChart AccountNumber { get; set; }


        [ForeignKey("Department")]
        public long? DepartmentID { get; set; }
        public Department Department { get; set; }
        

        [ForeignKey("Job")]
        public long? JobID { get; set; }
        public Job Job { get; set; }


        [ForeignKey("AcademicDegree")]
        public long? AcademicDegreeID { get; set; }
        public AcademicDegree AcademicDegree { get; set; }


        public ICollection<Bus> DrivenBuses { get; set; } = new HashSet<Bus>();
        public ICollection<Bus> DriverAssistant { get; set; } = new HashSet<Bus>();
        public ICollection<EmployeeAttachment> EmployeeAttachments { get; set; } = new HashSet<EmployeeAttachment>();
        public ICollection<Floor> Floors { get; set; } = new HashSet<Floor>();
        public ICollection<Student> Students { get; set; } = new HashSet<Student>();
        public ICollection<EmployeeDays> EmployeeDays { get; set; } = new HashSet<EmployeeDays>();
    }
}
