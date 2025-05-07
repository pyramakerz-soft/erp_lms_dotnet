using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.LMS
{
    public class StudentPerformance : AuditableEntity
    {
        [Key]
        public long ID { get; set; }

        //[ForeignKey("Student")]
        //public long StudentID { get; set; }
        //public Student Student { get; set; }

        [ForeignKey("PerformanceType")]
        public long PerformanceTypeID { get; set; }
        public PerformanceType PerformanceType { get; set; }
      
        public int Stars { get; set; }

        [ForeignKey("DailyPerformance")]
        public long DailyPerformanceID { get; set; }
        public DailyPerformance DailyPerformance { get; set; }
    }
}
