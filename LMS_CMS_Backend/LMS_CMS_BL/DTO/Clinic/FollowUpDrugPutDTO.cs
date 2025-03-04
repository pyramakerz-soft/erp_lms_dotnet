using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_BL.DTO.Clinic
{
    public class FollowUpDrugPutDTO
    {
        [Required(ErrorMessage = "ID is required")]
        public long ID { get; set; }

        [Required(ErrorMessage = "Drug ID is required")]
        public long DrugId { get; set; }

        [Required(ErrorMessage = "Dose ID is required")] 
        public long DoseId { get; set; }
    }
}
