namespace LMS_CMS_BL.DTO.Zatca
{
    public class SchoolPCsGetDTO
    {
        public long ID { get; set; }
        public string PCName { get; set; }
        public string SerialNumber { get; set; }
        public string School { get; set; }
        public DateOnly? CertificateDate { get; set; }
    }
}
