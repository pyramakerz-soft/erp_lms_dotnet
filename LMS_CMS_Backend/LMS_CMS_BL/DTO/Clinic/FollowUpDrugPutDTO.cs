using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_BL.DTO.Clinic
{
    public class FollowUpDrugPutDTO
    {
        [Required(ErrorMessage = "Foolow Up ID is required")]
        public long FollowUpId { get; set; }

        [Required(ErrorMessage = "Drug ID is required")]
        public long DrugId { get; set; }

        [Required(ErrorMessage = "Dose ID is required")] 
        public long DoseId { get; set; }
    }
}
