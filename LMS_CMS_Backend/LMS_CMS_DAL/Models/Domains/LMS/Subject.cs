using LMS_CMS_DAL.Models.Domains.RegisterationModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.LMS
{
    public class Subject : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string en_name { get; set; }
        [Required(ErrorMessage = "الاسم مطلوب")]
        [StringLength(100, ErrorMessage = "لا يمكن أن يكون الاسم أطول من 100 حرف")]
        public string ar_name { get; set; }
        public int OrderInCertificate { get; set; }
        public double CreditHours { get; set; }
        public string SubjectCode { get; set; }
        public int PassByDegree { get; set; }
        public int TotalMark {  get; set; }
        public bool HideFromGradeReport { get; set; }
        public string IconLink { get; set; }
        public int NumberOfSessionPerWeek { get; set; }

        [ForeignKey("Grade")]
        public long GradeID { get; set; }
        public Grade Grade { get; set; }

        [ForeignKey("SubjectCategory")]
        public long SubjectCategoryID { get; set; }
        public SubjectCategory SubjectCategory { get; set; }

        public ICollection<Test> Tests { get; set; } = new HashSet<Test>();
        public ICollection<StudentPerformance> StudentPerformances { get; set; } = new HashSet<StudentPerformance>();
        public ICollection<SubjectWeightType> SubjectWeightTypes { get; set; } = new HashSet<SubjectWeightType>();
        public ICollection<Lesson> Lessons { get; set; } = new HashSet<Lesson>();
        public ICollection<LessonLive> LessonLives { get; set; } = new HashSet<LessonLive>();
        public ICollection<DailyPerformance> DailyPerformance { get; set; } = new HashSet<DailyPerformance>();

    }
}

