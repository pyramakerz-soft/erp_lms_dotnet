using LMS_CMS_DAL.Models.Domains.ClinicModule;
using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_BL.DTO.Clinic
{
    public class StudentHygieneTypesGetDTO
    {
        public long ID { get; set; }
        public long HygieneFormId { get; set; }
        public long StudentId { get; set; }
        public string Student { get; set; }
        //public long HygieneTypeId { get; set; }
        //public string HygieneType { get; set; }n
        public ICollection<HygieneType>? HygieneTypes { get; set; }
        public bool Attendance { get; set; }
        public bool SelectAll { get; set; }
        public string? Comment { get; set; }
        public string? ActionTaken { get; set; }
    }
}
