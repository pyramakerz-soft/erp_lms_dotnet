using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models
{
    public partial class Role
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Role cannot be longer than 100 characters.")]
        [Unicode(false)]
        public string Name { get; set; }

        public ICollection<Role_Permissions> Role_Permissions { get; set; } = new HashSet<Role_Permissions>(); // Navigation property

        public ICollection<Employee_Role> Employee_Roles { get; set; } = new HashSet<Employee_Role>();

    }
}
