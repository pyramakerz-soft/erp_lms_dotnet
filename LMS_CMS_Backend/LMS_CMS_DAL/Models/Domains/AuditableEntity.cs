using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains
{
    [NotMapped]
    public class AuditableEntity
    {
        [ForeignKey("InsertedByEmployee")]
        public long? InsertedByUserId { get; set; }
        public long? InsertedByOctaId { get; set; }
        public Employee? InsertedByEmployee { get; set; }
        public DateTime? InsertedAt { get; set; }

        [ForeignKey("UpdatedByEmployee")]
        public long? UpdatedByUserId { get; set; }
        public long? UpdatedByOctaId { get; set; }
        public Employee? UpdatedByEmployee { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("DeletedByEmployee")]
        public long? DeletedByUserId { get; set; }
        public long? DeletedByOctaId { get; set; }
        public Employee? DeletedByEmployee { get; set; }
        public DateTime? DeletedAt { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
