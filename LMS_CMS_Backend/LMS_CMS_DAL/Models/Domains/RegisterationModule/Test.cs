using LMS_CMS_DAL.Models.Domains.LMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.RegisterationModule
{
    public class Test
    {
        [Key]
        public long ID { get; set; }
        [Required]
        public string Title { get; set; }
        public double TotalMark { get; set; }

        [ForeignKey("academicYear")]
        [Required]
        public long AcademicYearID { get; set; }
        public AcademicYear academicYear { get; set; }

        [ForeignKey("subject")]
        [Required]
        public long SubjectID { get; set; }
        public Subject subject { get; set; }

        public ICollection<Question> Questions { get; set; } = new HashSet<Question>();
        public ICollection<RegisterationFormTest> RegisterationFormTests { get; set; } = new HashSet<RegisterationFormTest>();



    }
}
