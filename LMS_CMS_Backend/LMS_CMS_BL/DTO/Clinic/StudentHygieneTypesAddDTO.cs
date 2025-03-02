using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_BL.DTO.Clinic
{
    public class StudentHygieneTypesAddDTO
    {
        [Required(ErrorMessage = "Hygiene Form ID is required")]
        public long HygieneFormId { get; set; }

        [Required(ErrorMessage = "Student ID is required")]
        public long StudentId { get; set; }

        public ICollection<long> HygieneTypeIds { get; set; }
    }
}
