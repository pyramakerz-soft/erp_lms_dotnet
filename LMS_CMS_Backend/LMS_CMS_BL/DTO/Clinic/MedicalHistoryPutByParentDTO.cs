using Microsoft.AspNetCore.Http;

namespace LMS_CMS_BL.DTO.Clinic
{
    public class MedicalHistoryPutByParentDTO
    {
        public long Id { get; set; }

        public string? Details { get; set; }

        public string? PermanentDrug { get; set; }

        public IFormFile? FirstReportFile { get; set; }
        public IFormFile? SecReportFile { get; set; }

        public string? FirstReport { get; set; }
        public string? SecReport { get; set; }
    }
}
