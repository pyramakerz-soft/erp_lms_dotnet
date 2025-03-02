using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_BL.DTO.Clinic
{
    public class MedicalHistoryPutDTO
    {
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
