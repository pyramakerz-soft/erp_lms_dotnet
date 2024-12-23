using LMS_CMS_DAL.Models.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Octa
{
    public class AuditableEntity
    {
        [ForeignKey("InsertedBy")]
        public long? InsertedByUserId { get; set; }
        public Octa? InsertedBy { get; set; }
        public DateTime? InsertedAt { get; set; }

        [ForeignKey("UpdatedBy")]
        public long? UpdatedByUserId { get; set; }
        public Octa? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("DeletedBy")]
        public long? DeletedByUserId { get; set; }
        public Octa? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
