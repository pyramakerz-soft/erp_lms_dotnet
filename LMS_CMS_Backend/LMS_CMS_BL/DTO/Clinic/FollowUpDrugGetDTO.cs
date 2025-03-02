using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_BL.DTO.Clinic
{
    public class FollowUpDrugGetDTO
    {
        public long ID { get; set; }
        public long FollowUpId { get; set; }
        public long DrugId { get; set; }
        public long DoseId { get; set; }
    }
}
