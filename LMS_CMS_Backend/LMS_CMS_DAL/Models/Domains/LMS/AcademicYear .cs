using LMS_CMS_DAL.Models.Domains.LMS;
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
    public class AcademicYear : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("School")]
        public long SchoolID { get; set; }
        public School School { get; set; }

        public ICollection<Semester> Semesters { get; set; } = new HashSet<Semester>();
        public ICollection<Classroom> Classrooms { get; set; } = new HashSet<Classroom>();
        public ICollection<Test> Tests { get; set; } = new HashSet<Test>();
        public ICollection<InterviewTime> InterviewTimes { get; set; } = new HashSet<InterviewTime>();
    }
}
