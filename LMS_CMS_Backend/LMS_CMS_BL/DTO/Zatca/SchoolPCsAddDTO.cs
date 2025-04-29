using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_BL.DTO.Zatca
{
    public class SchoolPCsAddDTO
    {
        [Required]
        public string PCName { get; set; }
        public long? SchoolId { get; set; }
    }
}
