using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.LMS
{
    public class School : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "School cannot be longer than 100 characters.")]
        public string Name { get; set; }
       public string?  Address { get; set; }
        [ForeignKey("SchoolType")]
        public long SchoolTypeID { get; set; }
        public SchoolType SchoolType { get; set; }
        public ICollection<AcademicYear> AcademicYears { get; set; } = new HashSet<AcademicYear>();
        public ICollection<StudentAcademicYear> StudentAcademicYears { get; set; } = new HashSet<StudentAcademicYear>();
        public ICollection<Section> Sections { get; set; } = new HashSet<Section>();
        public ICollection<Building> Buildings { get; set; } = new HashSet<Building>();

    }
}
