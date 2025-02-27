using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_BL.DTO.Clinic
{
    public class HygieneTypeGetDTO
    {
        public long ID { get; set; }
        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }
        public long InsertedByUserId { get; set; }
    }
}
