using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models
{
    public class Role_Detailes
    {
        [Key]
        public long ID { get; set; }
        public bool Allow_Edit { get; set; }
        public bool Allow_Delete { get; set; }

        [ForeignKey("Role")]
        [Required]
        public long Role_ID { get; set; }
        [ForeignKey("Page")]
        [Required]
        public long Page_ID { get; set; }

        public Role Role { get; set; }
        public Page Page { get; set; }
    }
}