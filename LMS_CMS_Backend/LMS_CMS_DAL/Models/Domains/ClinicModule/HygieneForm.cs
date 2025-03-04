using LMS_CMS_DAL.Models.Domains.LMS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_CMS_DAL.Models.Domains.ClinicModule
{
    public class HygieneForm : AuditableEntity
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "School ID is required")]
        [ForeignKey("School")]
        public long SchoolId { get; set; }
        public School? School { get; set; }

        [Required(ErrorMessage = "Grade ID is required")]
        [ForeignKey("Grade")]
        public long GradeId { get; set; }
        public Grade? Grade { get; set; }

        [Required(ErrorMessage = "ClassRoom ID is required")]
        [ForeignKey("Classroom")]
        public long ClassRoomID { get; set; }
        public Classroom? Classroom { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }

        public ICollection<StudentHygieneTypes> StudentHygieneTypes { get; set; } = new HashSet<StudentHygieneTypes>();
    }
}
