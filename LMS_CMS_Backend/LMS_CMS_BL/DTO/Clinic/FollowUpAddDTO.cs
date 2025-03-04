using LMS_CMS_DAL.Models.Domains.ClinicModule;
using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_BL.DTO.Clinic
{
    public class FollowUpAddDTO
    {
        [Required(ErrorMessage = "School ID is required")]
        public long SchoolId { get; set; }

        [Required(ErrorMessage = "Grade ID is required")]
        public long GradeId { get; set; }

        [Required(ErrorMessage = "Classroom ID is required")]
        public long ClassroomId { get; set; }

        [Required(ErrorMessage = "Student ID is required")]
        public long StudentId { get; set; }

        public string? Complains { get; set; }

        [Required(ErrorMessage = "Diagnosis ID is required")]
        public long DiagnosisId { get; set; }

        public ICollection<FollowUpDrugAddDTO> FollowUpDrugs { get; set; } = new HashSet<FollowUpDrugAddDTO>();

        public string? Recommendation { get; set; }
        public bool SendSMSToParent { get; set; }
    }
}
