using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_BL.DTO.Clinic
{
    public class DoseGetDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public DateTime InsertedAt { get; set; }
    }
}
