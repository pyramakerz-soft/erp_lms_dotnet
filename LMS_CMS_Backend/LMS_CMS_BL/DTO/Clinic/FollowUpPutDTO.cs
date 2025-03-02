using LMS_CMS_DAL.Models.Domains.ClinicModule;
using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_BL.DTO.Clinic
{
    public class FollowUpPutDTO
    {
        public long ID { get; set; }
        public long SchoolId { get; set; }
        public long ClassroomId { get; set; }
        public string? Complains { get; set; }
        public long DiagnosisId { get; set; }

        public ICollection<FollowUpDrugPutDTO> FollowUpDrugs { get; set; } = new HashSet<FollowUpDrugPutDTO>();

        public string? Recommendation { get; set; }
        public bool SendSMSToParent { get; set; }
    }
}
