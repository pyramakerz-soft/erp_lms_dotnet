using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models
{
    public partial class Detailed_Permissions
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Detailed_Permissions cannot be longer than 100 characters.")]
        [Unicode(false)]
        public string Name { get; set; }



        public ICollection<Master_Detailes_Permissions> Master_Detailes_Permissions { get; set; } = new HashSet<Master_Detailes_Permissions>();

    }
}
