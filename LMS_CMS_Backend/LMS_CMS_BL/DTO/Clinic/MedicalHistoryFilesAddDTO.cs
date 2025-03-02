using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_BL.DTO.Clinic
{
    public class MedicalHistoryFilesAddDTO
    {
        [Required(ErrorMessage = "Medical History Id is required")]
        public long MedicalHistoryId { get; set; }

        public string? FileName { get; set; }

        public IFormFile? Report { get; set; }
    }
}
