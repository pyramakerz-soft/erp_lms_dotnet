using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models
{
    public class AuditableEntity
    {
        public int? UpdatedByUserId { get; set; }
        public Employee? UpdatedByUser { get; set; }

        public int? DeletedByUserId { get; set; }
        public Employee? DeletedByUser { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
