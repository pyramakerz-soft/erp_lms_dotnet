using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_BL.DTO.Clinic
{
    public class StudentHygieneTypesAddDTO
    {
        [Required(ErrorMessage = "Student ID is required")]
        public long StudentId { get; set; }

        List<HygieneTypeAddDTO>? HygieneTypes { get; set; }

        public bool Attendance { get; set; }
        public string? Comment { get; set; }
        public string? ActionTaken { get; set; }
    }
}
