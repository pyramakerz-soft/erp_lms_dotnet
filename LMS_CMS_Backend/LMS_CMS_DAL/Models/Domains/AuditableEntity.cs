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
        [ForeignKey("InsertedByPyramakerz")]
        public long? InsertedByPyramakerzId { get; set; }
        public Employee? InsertedByEmployee { get; set; }
        public Pyramakerz? InsertedByPyramakerz { get; set; }
        public DateTime? InsertedAt { get; set; }

        [ForeignKey("UpdatedByEmployee")]
        public long? UpdatedByUserId { get; set; }
        [ForeignKey("UpdatedByPyramakerz")]
        public long? UpdatedByPyramakerzId { get; set; }
        public Employee? UpdatedByEmployee { get; set; }
        public Pyramakerz? UpdatedByPyramakerz { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("DeletedByEmployee")]
        public long? DeletedByUserId { get; set; }
        [ForeignKey("DeletedByPyramakerz")]
        public long? DeletedByPyramakerzId { get; set; }
        public Employee? DeletedByEmployee { get; set; }
        public Pyramakerz? DeletedByPyramakerz { get; set; }
        public DateTime? DeletedAt { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
