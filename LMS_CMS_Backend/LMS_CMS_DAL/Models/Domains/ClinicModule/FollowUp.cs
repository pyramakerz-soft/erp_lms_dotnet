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
        public virtual School? School { get; set; }

        [Required(ErrorMessage = "Grade ID is required")]
        [ForeignKey("Grade")]
        public long GradetId { get; set; }
        public virtual Grade? Grade { get; set; }

        [Required(ErrorMessage = "Grade ID is required")]
        [ForeignKey("Classromm")]
        public long ClassroomId { get; set; }
        public virtual Classroom? Classroom { get; set; }

        [Required(ErrorMessage = "Student ID is required")]
        [ForeignKey("Student")]
        public long StudentId { get; set; }
        public virtual Student? Student { get; set; }
        
        public string? Complains { get; set; }

        [Required(ErrorMessage = "Diagnosis ID is required")]
        [ForeignKey("Diagnosis")]
        public long DiagnosisId { get; set; }
        public virtual Diagnosis? Diagnosis { get; set; }

        [Required(ErrorMessage = "Drug ID is required")]
        [ForeignKey("Drug")]
        public long Drug { get; set; }
        public virtual Drug? Drugs { get; set; }

        [Required(ErrorMessage = "Dose ID is required")]
        [ForeignKey("Dose")]
        public long DoseId { get; set; }
        public virtual Dose? Doses { get; set; }

        public string? Recommendation { get; set; }
    }
}
