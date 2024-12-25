using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public string? Email { get; set; }
        public string? LicenseNumber { get; set; }
        public string? ExpireDate { get; set; }
        public string? Address { get; set; }


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


        public ICollection<Bus> DrivenBuses { get; set; } = new HashSet<Bus>();
        public ICollection<Bus> DriverAssistant { get; set; } = new HashSet<Bus>();
        public ICollection<EmployeeAttachment> EmployeeAttachments { get; set; } = new HashSet<EmployeeAttachment>();
        public ICollection<Floor> Floors { get; set; } = new HashSet<Floor>();
    }
}
