using LMS_CMS_DAL.Models.Domains.BusModule;
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
    public class Grade : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        [ForeignKey("Section")]
        public long SectionID { get; set; }
        public Section Section { get; set; }
        public ICollection<StudentAcademicYear> StudentAcademicYears { get; set; } = new HashSet<StudentAcademicYear>();
        public ICollection<Subject> Subjects { get; set; } = new HashSet<Subject>();
        public ICollection<Classroom> Classrooms { get; set; } = new HashSet<Classroom>();
        public ICollection<Test> Tests { get; set; } = new HashSet<Test>();
    }
}
