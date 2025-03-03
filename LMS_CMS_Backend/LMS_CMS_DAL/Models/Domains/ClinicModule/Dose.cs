using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_DAL.Models.Domains.ClinicModule
{
    public class Dose : AuditableEntity
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Dose Times is required")]
        public string DoseTimes { get; set; }
    }
}
