using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models
{
    public class Modules
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        public ICollection<Modules_Master_permissions> Modules_Master_permissions { get; set; } = new HashSet<Modules_Master_permissions>(); // Navigation property

    }
}
