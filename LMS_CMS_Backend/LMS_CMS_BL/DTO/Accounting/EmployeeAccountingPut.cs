using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Accounting
{
    public class EmployeeAccountingPut
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "User_Name is required")]
        [StringLength(100, ErrorMessage = "Username cannot be longer than 100 characters.")]
        public string User_Name { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        public string? Mobile { get; set; }
        public string? Phone { get; set; }
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
        public long? AcademicDegreeID { get; set; }
        public long? JobID { get; set; }
        public long? DepartmentID { get; set; }
        public long? AccountNumberID { get; set; }
        public long? ReasonOfLeavingID { get; set; }
        public List<int> Days { get; set; }
        public List<int> Students { get; set; }

    }
}
