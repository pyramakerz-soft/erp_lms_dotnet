using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_DAL.Models.Domains.ClinicModule
{
    public class HygieneType : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }
    }
}
