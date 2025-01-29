using LMS_CMS_DAL.Models.Domains.AccountingModule;
using LMS_CMS_DAL.Models.Domains.Administration;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_DAL.Models.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Accounting
{
    public class EmployeeAccountingGetDTO
    {
        public long ID { get; set; }
        public string User_Name { get; set; }
        public string en_name { get; set; }
        public string? ar_name { get; set; }
        public string Password { get; set; }
        public string? Mobile { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? LicenseNumber { get; set; }
        public string? ExpireDate { get; set; }
        public string? Address { get; set; }
        public string? NationalID { get; set; }
        public string? PassportNumber { get; set; }
        public string? ResidenceNumber { get; set; }
        public string? BirthdayDate { get; set; }
        public long? Nationality { get; set; }
        public string? NationalityName { get; set; }
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
        public long? ReasonOfLeavingID { get; set; }
        public string? ReasonForLeavingWork{ get; set; }
        public long? AccountNumberID { get; set; }
        public string? AccountNumberName { get; set; }
        public long? DepartmentID { get; set; }
        public string? DepartmentName { get; set; }
        public long? JobID { get; set; }
        public string JobName { get; set; }
        public long? JobCategoryId { get; set; }
        public long? AcademicDegreeID { get; set; }
        public string AcademicDegreeName { get; set; }
        public List<long>? Days { get; set; }

    }
}
