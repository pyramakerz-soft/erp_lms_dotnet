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

        [Required]
        [StringLength(100, ErrorMessage = "Detailed_Permissions cannot be longer than 100 characters.")]
        [Unicode(false)]
        public string Name { get; set; }

        [Required]
        [ForeignKey("Master_Permissions")]
        public int Master_Permission_ID {  get; set; }

        public Master_Permissions Master_Permissions { get; set; }
    }
}
