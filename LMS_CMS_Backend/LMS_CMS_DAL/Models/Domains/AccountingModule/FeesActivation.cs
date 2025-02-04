using LMS_CMS_DAL.Models.Domains.LMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.AccountingModule
{
    public class FeesActivation : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        public float Amount { get; set; }
        public float Discount { get; set; }
        public float Net { get; set; }
        public string Date { get; set; }
        [ForeignKey("TuitionFeesType")]
        public long FeeTypeID { get; set; }
        [ForeignKey("TuitionDiscountType")]
        public long? FeeDiscountTypeID { get; set; }
        [ForeignKey("Student")]
        public long StudentID { get; set; }
        [ForeignKey("AcademicYear")]
        public long? AcademicYearId { get; set; }
        public AcademicYear? AcademicYear { get; set; }

        public TuitionFeesType TuitionFeesType { get; set; }
        public TuitionDiscountType? TuitionDiscountType { get; set; }
        public Student Student { get; set; }
    }
}
