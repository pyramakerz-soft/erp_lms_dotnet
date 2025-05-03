using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_BL.DTO.Zatca
{
    public class SchoolPCsAddDTO
    {
        public long ID { get; set; }
        [Required]
        public string PCName { get; set; }
        public string SerialNumber { get; set; }
        public long? SchoolId { get; set; }
    }
}
