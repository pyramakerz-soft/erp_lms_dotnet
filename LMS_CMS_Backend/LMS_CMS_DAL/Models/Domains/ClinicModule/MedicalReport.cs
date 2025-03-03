using LMS_CMS_DAL.Models.Domains.LMS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_CMS_DAL.Models.Domains.ClinicModule
{
    public class MedicalReport : AuditableEntity
    {
        [Key]
        public long Id { get; set; }

        public ICollection<MedicalHistory> MHByParent { get; set; } = new HashSet<MedicalHistory>();
        public ICollection<MedicalHistory> MHByDoctor { get; set; } = new HashSet<MedicalHistory>();
        public ICollection<HygieneForm> HygieneForms { get; set; } = new HashSet<HygieneForm>();
        public ICollection<FollowUp> FollowUps { get; set; } = new HashSet<FollowUp>();
    }
}
