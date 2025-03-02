using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_BL.DTO.Clinic
{
    public class DoseAddDTO
    {
        [Required(ErrorMessage = "Dose Times is required")]
        public string DoseTimes { get; set; }
    }
}
