using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models
{
    public class Domain
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        public ICollection<School> Schools { get; set; } = new HashSet<School>();
        public ICollection<Role> Roles { get; set; } = new HashSet<Role>();
        public ICollection<Employee> Employess { get; set; } = new HashSet<Employee>();
        public ICollection<Domain_Page_Detailes> Domain_Page_Detailes { get; set; } = new HashSet<Domain_Page_Detailes>();
    }
}
