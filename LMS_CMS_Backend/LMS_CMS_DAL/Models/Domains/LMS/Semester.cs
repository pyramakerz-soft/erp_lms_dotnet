using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_DAL.Models.Domains.LMS;

namespace LMS_CMS_DAL.Models.Domains.LMS
{
    public class Semester : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public bool? IsCurrent { get; set; }
        public bool? IsActive { get; set; }

        [ForeignKey("WeekStartDay")]
        public long? WeekStartDayID { get; set; }
        public WeekDay? WeekStartDay { get; set; }

        [ForeignKey("WeekEndDay")]
        public long? WeekEndDayID { get; set; }
        public WeekDay? WeekEndDay { get; set; }

        [ForeignKey("AcademicYear")]
        public long? AcademicYearID { get; set; }
        public AcademicYear? AcademicYear { get; set; }
        public ICollection<BusStudent> BusStudents { get; set; } = new HashSet<BusStudent>();
        public ICollection<SemesterWorkingWeek> SemesterWorkingWeeks { get; set; } = new HashSet<SemesterWorkingWeek>();
    }
}
