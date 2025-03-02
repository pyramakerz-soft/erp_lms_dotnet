using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_BL.DTO.Clinic
{
    public class DoseGetDTO
    {
        public long ID { get; set; }
        public string DoseTimes { get; set; }
        public DateTime InsertedAt { get; set; }
    }
}
