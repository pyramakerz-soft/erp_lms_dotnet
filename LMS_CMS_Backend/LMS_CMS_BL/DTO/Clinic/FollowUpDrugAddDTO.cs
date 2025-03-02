using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_BL.DTO.Clinic
{
    public class FollowUpDrugAddDTO
    {

        [Required(ErrorMessage = "Drug ID is required")]
        public long DrugId { get; set; }

        [Required(ErrorMessage = "Dose ID is required")] 
        public long DoseId { get; set; }
    }
}
