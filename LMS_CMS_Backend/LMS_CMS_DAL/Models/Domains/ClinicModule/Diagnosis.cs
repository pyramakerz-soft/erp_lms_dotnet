using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_DAL.Models.Domains.ClinicModule
{
    public class Diagnosis : AuditableEntity
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}
