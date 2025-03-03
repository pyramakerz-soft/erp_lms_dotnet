using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_BL.DTO.Clinic
{
    public class MedicalHistoryPutByDoctorDTO
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "School ID is required")]
        public long SchoolId { get; set; }

        [Required(ErrorMessage = "Grade ID is required")]
        public long GradeId { get; set; }

        [Required(ErrorMessage = "Classroom ID is required")]
        public long ClassRoomID { get; set; }

        [Required(ErrorMessage = "Student ID is required")]
        public long StudentId { get; set; }

        public string? Details { get; set; }

        public string? PermanentDrug { get; set; }

        public IFormFile? FirstReportFile { get; set; }
        public IFormFile? SecReportFile { get; set; }

        public string? FirstReport { get; set; }
        public string? SecReport { get; set; }
    }
}
