using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_BL.DTO.Clinic
{
    public class DrugGetDTO
    {
        public long ID { get; set; }
        [Required(ErrorMessage = ("Name is required."))]
        public string Name { get; set; }
        public DateTime InsertedAt { get; set; }
    }
}
