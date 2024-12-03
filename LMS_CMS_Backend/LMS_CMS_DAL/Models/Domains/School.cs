using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains
{
    public class School : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "School cannot be longer than 100 characters.")]
        public string Name { get; set; }

        public ICollection<AcademicYear> AcademicYears { get; set; } = new HashSet<AcademicYear>();

    }
}
