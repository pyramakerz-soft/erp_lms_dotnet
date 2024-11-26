using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models
{
    public class AuditableEntity
    {
        [ForeignKey("InsertedByUser")]
        public long? InsertedByUserId { get; set; }
        public Employee? InsertedByUser { get; set; }
        public string? InsertedByUserRole { get; set; }
        public DateTime ? InsertedAt { get; set; }

        [ForeignKey("UpdatedByUser")]
        public long? UpdatedByUserId { get; set; }
        public Employee? UpdatedByUser { get; set; }
        public string? UpdatedByUserRole { get; set; }
        public DateTime? UpdatedAt { get; set; }


        [ForeignKey("DeletedByUser")]
        public long? DeletedByUserId { get; set; }
        public Employee? DeletedByUser { get; set; }
        public string? DeletedByUserRole { get; set; }
        public DateTime? DeletedAt { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
