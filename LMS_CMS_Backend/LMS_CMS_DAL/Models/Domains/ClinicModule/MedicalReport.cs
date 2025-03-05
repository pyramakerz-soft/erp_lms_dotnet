using LMS_CMS_DAL.Models.Domains.LMS;

namespace LMS_CMS_DAL.Models.Domains.ClinicModule
{
    public class MedicalReport
    {
        public long SchoolId { get; set; }
        public virtual School? School { get; set; }
        public long GradeId { get; set; }
        public virtual Grade? Grade { get; set; }
        public long ClassroomId { get; set; }
        public virtual Classroom? Classroom { get; set; }
        public long StudentId { get; set; }
        public virtual Student? Student { get; set; }

        public ICollection<MedicalHistory> MHByParent { get; set; } = new HashSet<MedicalHistory>();
        public ICollection<MedicalHistory> MHByDoctor { get; set; } = new HashSet<MedicalHistory>();
        public ICollection<HygieneForm> HygieneForms { get; set; } = new HashSet<HygieneForm>();
        public ICollection<FollowUp> FollowUps { get; set; } = new HashSet<FollowUp>();
    }
}
