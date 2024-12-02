using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains
{
    public partial class Role : AuditableEntity
    {
        [Key]
        public long ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Role cannot be longer than 100 characters.")]
        public string Name { get; set; }
        [ForeignKey("Domain")]
        [Required]
        public long Domain_ID { get; set; }

        public Domain Domain { get; set; }
        public ICollection<Employee> Employess { get; set; } = new HashSet<Employee>();

        public ICollection<Role_Detailes> Role_Detailes { get; set; } = new HashSet<Role_Detailes>();

    }
}
