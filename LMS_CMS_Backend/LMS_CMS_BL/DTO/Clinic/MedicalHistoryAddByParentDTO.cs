using Microsoft.AspNetCore.Http;

namespace LMS_CMS_BL.DTO.Clinic
{
    public class MedicalHistoryAddByParentDTO
    {
        public string Details { get; set; }
        public string Permanent { get; set; }
        public IFormFile? FirstReport { get; set; }
        public IFormFile? SecReport { get; set; }
    }
}
