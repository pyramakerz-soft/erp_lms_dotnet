using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_CMS_DAL.Models.Domains.ClinicModule
{
    public class MedicalHistoryFiles : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Medical History Id is required")]
        [ForeignKey("MedicalHistory")]
        public long MedicalHistoryId { get; set; }
        public MedicalHistory? MedicalHistory { get; set; }

        public string? FileName { get; set; }
    }
}
