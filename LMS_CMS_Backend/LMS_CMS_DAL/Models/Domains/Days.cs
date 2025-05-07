using LMS_CMS_DAL.Models.Domains.LMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains
{
    public class Days
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        public ICollection<EmployeeDays> EmployeeDays { get; set; } = new HashSet<EmployeeDays>();
        public ICollection<Semester> StartDaySemesters { get; set; } = new HashSet<Semester>();
        public ICollection<Semester> EndDaySemesters { get; set; } = new HashSet<Semester>();
        public ICollection<LessonLive> LessonLives { get; set; } = new HashSet<LessonLive>();
    }
}
