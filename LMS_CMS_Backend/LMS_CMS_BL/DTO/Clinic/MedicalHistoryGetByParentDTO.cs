namespace LMS_CMS_BL.DTO.Clinic
{
    public class MedicalHistoryGetByParentDTO
    {
        public long ID { get; set; }
        public string? Details { get; set; }
        public string? PermanentDrug { get; set; }
        public string? FirstReport { get; set; }
        public string? SecReport { get; set; }
        public DateTime? InsertedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
