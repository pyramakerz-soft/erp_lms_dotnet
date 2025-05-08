using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Octa
{
    public class Domain : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required]
        public string Name { get; set; }
        //public string ConnectionString { get; set; }
    }
}
