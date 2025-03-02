using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace LMS_CMS_BL.DTO.Clinic
{
    public class MedicalHistoryAddDTO
    {
        [Required(ErrorMessage = "School ID is required")]
        public long? SchoolId { get; set; }

        [Required(ErrorMessage = "Grade ID is required")]
        public long? GradeId { get; set; }

        [Required(ErrorMessage = "Classroom ID is required")]
        public long? ClassRoomID { get; set; }

        [Required(ErrorMessage = "Student ID is required")]
        public long? StudentId { get; set; }

        public string? Details { get; set; }

        public int? Attached { get; set; } = 0;

        public string? PermanentDrug { get; set; }

        public IFormFileCollection? Attachments { get; set; }
    }
}
