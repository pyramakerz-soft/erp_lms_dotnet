namespace LMS_CMS_DAL.Models.Domains.ClinicModule
{
    public class StudentHygienes : AuditableEntity
    {
        public long ID { get; set; }
        public long StudentId { get; set; }
        public long HygieneTypeId { get; set; }
    }
}
