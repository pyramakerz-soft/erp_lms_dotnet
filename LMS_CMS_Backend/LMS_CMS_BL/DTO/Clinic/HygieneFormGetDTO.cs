namespace LMS_CMS_BL.DTO.Clinic
{
    public class HygieneFormGetDTO
    {
        public long ID { get; set; }
        public long SchoolId { get; set; }
        public long GradeId { get; set; }
        public long ClassRoomID { get; set; }
        public long InsertedAt { get; set; }
        public long StudentId { get; set; }
        public DateTime Date { get; set; }
    }
}
