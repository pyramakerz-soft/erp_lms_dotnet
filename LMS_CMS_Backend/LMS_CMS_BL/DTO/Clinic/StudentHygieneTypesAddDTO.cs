using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_BL.DTO.Clinic
{
    public class StudentHygieneTypesAddDTO
    {
        [Required(ErrorMessage = "Student ID is required")]
        public long StudentId { get; set; }

        [Required(ErrorMessage = "Student ID is required")]
        public long HygieneId { get; set; }
    }
}
