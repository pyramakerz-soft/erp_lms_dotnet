using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models
{
    public partial class Master_Permissions
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Master_Permissions cannot be longer than 100 characters.")]
        [Unicode(false)]
        public string Name { get; set; }

        public ICollection<Detailed_Permissions> Detailed_Permissions { get; set; } = new HashSet<Detailed_Permissions>();
    }
}
