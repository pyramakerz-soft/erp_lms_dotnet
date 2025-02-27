using LMS_CMS_DAL.Models.Domains.ClinicModule;
using LMS_CMS_DAL.Models.Domains.LMS;
using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_BL.DTO.Clinic
{
    public class HygieneFormAddDTO
    {
        [Required(ErrorMessage = "Classroom ID is required")]
        public long ClassRoomID { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }

        public bool Attendance { get; set; }
        public bool SelectAll { get; set; }

        public string? Comment { get; set; }
        public string? ActionTaken { get; set; }

        public ICollection<Student>? Students { get; set; } = new HashSet<Student>();
        public ICollection<HygieneType>? HygieneTypes { get; set; } = new HashSet<HygieneType>();
    }
}
