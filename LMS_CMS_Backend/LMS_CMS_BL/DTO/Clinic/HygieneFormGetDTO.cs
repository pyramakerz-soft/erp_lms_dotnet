namespace LMS_CMS_BL.DTO.Clinic
{
    public class HygieneFormGetDTO
    {
        public long ID { get; set; }
        public long SchoolId { get; set; }
        public string School { get; set; }
        public long GradeId { get; set; }
        public string Grade { get; set; }
        public long ClassRoomID { get; set; }
        public string ClassRoom { get; set; }
        public DateTime InsertedAt { get; set; }
        public DateTime Date { get; set; }

        public List<StudentHygieneTypesGetDTO> StudentHygieneTypes { get; set; }
    }
}
