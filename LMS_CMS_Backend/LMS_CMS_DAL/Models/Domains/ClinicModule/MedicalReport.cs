using LMS_CMS_DAL.Models.Domains.LMS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_CMS_DAL.Models.Domains.ClinicModule
{
    public class MedicalReport : AuditableEntity
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "School ID is required")]
        [ForeignKey("School")]
        public long? SchoolId { get; set; }
        public virtual School? School { get; set; }

        [Required(ErrorMessage = "Grade ID is required")]
        [ForeignKey("Grade")]
        public long? GradetId { get; set; }
        public virtual Grade? Grade { get; set; }

        [Required(ErrorMessage = "Classroom ID is required")]
        [ForeignKey("Classroom")]
        public long? ClassRoomID { get; set; }
        public virtual Classroom? Classroom { get; set; }

        [Required(ErrorMessage = "Student ID is required")]
        [ForeignKey("Student")]
        public long? StudentId { get; set; }
        public Student? Student { get; set; }

        [ForeignKey("MedicalHistory")]
        public long? MedicalHistoryId { get; set; }
        public virtual MedicalHistory? MedicalHistory { get; set; }

        [ForeignKey("HygieneForm")]
        public long? HygieneFormId { get; set; }
        public virtual HygieneForm? HygieneForm { get; set; }

        [ForeignKey("FollowUp")]
        public long? FollowUpId { get; set; }
        public virtual FollowUp? FollowUp { get; set; }
    }
}
