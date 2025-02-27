using LMS_CMS_DAL.Models.Domains.LMS;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_CMS_DAL.Models.Domains.ClinicModule
{
    public class MedicalHistory : AuditableEntity
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
        public virtual Student? Student { get; set; }

        public string? Details { get; set; }

        public int? Attached { get; set; }

        public string? PermanentDrug { get; set; }

        public IFormFile? Drug1 { get; set; }
        public IFormFile? Drug2 { get; set; }
    }
}
