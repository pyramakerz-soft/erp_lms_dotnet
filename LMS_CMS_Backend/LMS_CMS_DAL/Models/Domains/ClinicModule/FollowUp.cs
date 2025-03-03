using LMS_CMS_DAL.Models.Domains.LMS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_CMS_DAL.Models.Domains.ClinicModule
{
    public class FollowUp : AuditableEntity
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

        [Required(ErrorMessage = "Classroom ID is required")]
        [ForeignKey("Classromm")]
        public long ClassroomId { get; set; }
        public Classroom? Classroom { get; set; }

        [Required(ErrorMessage = "Student ID is required")]
        [ForeignKey("Student")]
        public long StudentId { get; set; }
        public Student? Student { get; set; }

        public string? Complains { get; set; }

        [Required(ErrorMessage = "Diagnosis ID is required")]
        [ForeignKey("Diagnosis")]
        public long DiagnosisId { get; set; }
        public Diagnosis? Diagnosis { get; set; }

        public ICollection<FollowUpDrug>? FollowUpDrugs { get; set; } = new HashSet<FollowUpDrug>();

        public string? Recommendation { get; set; }
        public bool? SendSMS { get; set; }
    }
}
