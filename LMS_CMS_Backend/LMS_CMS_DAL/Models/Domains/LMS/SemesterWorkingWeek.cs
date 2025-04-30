using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.LMS
{
    public class SemesterWorkingWeek : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "English Name is required")]
        [StringLength(100, ErrorMessage = "English Name cannot be longer than 100 characters.")]
        public string EnglishName { get; set; }
        [Required(ErrorMessage = "Arabic Name is required")]
        [StringLength(100, ErrorMessage = "Arabic Name cannot be longer than 100 characters.")]
        public string ArabicName { get; set; }

        [ForeignKey("Semester")]
        public long SemesterID { get; set; }
        public Semester Semester { get; set; }

        public DateOnly DateFrom { get; set; }
        public DateOnly DateTo { get; set; }


        public ICollection<Lesson> Lessons { get; set; } = new HashSet<Lesson>();

    }
}
