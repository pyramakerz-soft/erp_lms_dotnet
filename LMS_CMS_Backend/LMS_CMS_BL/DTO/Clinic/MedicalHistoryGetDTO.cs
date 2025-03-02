using LMS_CMS_DAL.Models.Domains.LMS;
using Microsoft.AspNetCore.Http;

namespace LMS_CMS_BL.DTO.Clinic
{
    public class MedicalHistoryGetDTO
    {
        public long ID { get; set; }
        public long? ClassRoomID { get; set; }
        public virtual Classroom? Classroom { get; set; }
        public long? StudentId { get; set; }
        public virtual Student? Student { get; set; }
        public string? Details { get; set; }
        public int? Attached { get; set; } = 0;
        public string? PermanentDrug { get; set; }
        public IFormFileCollection? Attachments { get; set; }
    }
}
