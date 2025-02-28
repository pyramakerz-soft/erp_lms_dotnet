using LMS_CMS_DAL.Models.Domains.LMS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_CMS_DAL.Models.Domains.ClinicModule
{
    public class HygieneForm : AuditableEntity
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "ClassRoom ID is required")]
        [ForeignKey("Classroom")]
        public long ClassRoomID { get; set; }
        public Classroom? Classroom { get; set; }

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
