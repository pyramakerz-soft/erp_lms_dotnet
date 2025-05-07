using LMS_CMS_DAL.Models.Domains.LMS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_CMS_DAL.Models.Domains.Zatca
{
    public class SchoolPCs : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        public string PCName { get; set; }
        public string SerialNumber { get; set; }

        [ForeignKey("School")]
        public long SchoolId { get; set; }
        public School? School { get; set; }
        public DateOnly? CertificateDate { get; set; }
    }
}
