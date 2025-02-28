using LMS_CMS_DAL.Models.Domains.ClinicModule;
using LMS_CMS_DAL.Models.Domains.LMS;

namespace LMS_CMS_BL.DTO.Clinic
{
    public class HygieneFormGetDTO
    {
        public long ID { get; set; }
        public long GradeId { get; set; }
        public long ClassRoomID { get; set; }
        public long InsertedByUserId { get; set; }
        public DateTime Date { get; set; }

        public bool Attendance { get; set; }
        public bool SelectAll { get; set; }

        public string? Comment { get; set; }
        public string? ActionTaken { get; set; }

        public ICollection<Student>? Students { get; set; } = new HashSet<Student>();
        public ICollection<HygieneType>? HygieneTypes { get; set; } = new HashSet<HygieneType>();
    }
}
