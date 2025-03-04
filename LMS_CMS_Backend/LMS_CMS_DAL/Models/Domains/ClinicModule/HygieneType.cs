using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_DAL.Models.Domains.ClinicModule
{
    public class HygieneType : AuditableEntity
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }

        public ICollection<StudentHygieneTypes> StudentHygieneTypes { get; set; } = new HashSet<StudentHygieneTypes>();
    }
}
