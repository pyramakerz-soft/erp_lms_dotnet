using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_BL.DTO.Clinic
{
    public class DosePutDTO
    {
        [Required(ErrorMessage = "ID is required")]
        public long ID { get; set; }
        [Required(ErrorMessage = "Dose Times is required")]
        public string DoseTimes { get; set; }
    }
}
