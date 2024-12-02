using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains
{
    public class Domain_Page_Detailes : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [ForeignKey("Domain")]
        [Required]
        public long Domain_ID { get; set; }
        [ForeignKey("Page")]
        [Required]
        public long Page_ID { get; set; }

        public Domain Domain { get; set; }
        public Page Page { get; set; }
    }
}
