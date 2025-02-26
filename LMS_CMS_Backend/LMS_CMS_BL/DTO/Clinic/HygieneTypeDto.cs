using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_BL.DTO.Clinic
{
    public class HygieneTypeDto
    {
        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }
    }
}
