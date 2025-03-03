using LMS_CMS_DAL.Models.Domains.LMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.ECommerce
{
    public class Order : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        public float TotalPrice { get; set; }

        [ForeignKey("OrderState")]
        public long OrderStateID { get; set; }
        [ForeignKey("Student")]
        public long StudentID { get; set; }
        [ForeignKey("Cart")]
        public long CartID { get; set; }

        public OrderState OrderState { get; set; }
        public Student Student { get; set; }
        public Cart Cart { get; set; }
    }
}
