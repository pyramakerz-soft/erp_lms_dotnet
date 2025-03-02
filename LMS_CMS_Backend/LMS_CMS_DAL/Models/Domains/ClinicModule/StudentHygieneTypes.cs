using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LMS_CMS_DAL.Models.Domains.LMS;

namespace LMS_CMS_DAL.Models.Domains.ClinicModule
{
    public class StudentHygieneTypes : AuditableEntity
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Hygiene Form ID is required")]
        [ForeignKey("HygieneForm")]
        public long HygieneFormId { get; set; }
        public HygieneForm HygieneForm { get; set; }

        [Required(ErrorMessage = "Student ID is required")]
        [ForeignKey("Student")]
        public long StudentId { get; set; }
        public Student? Student { get; set; }

        public ICollection<HygieneType>? HygieneTypes { get; set; }
    }
}
