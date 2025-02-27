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
    public class Cart : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        public int TotalPrice { get; set; }

        [ForeignKey("PromoCode")]
        public long? PromoCodeID { get; set; }
        [ForeignKey("Student")]
        public long StudentID { get; set; }

        public PromoCode? PromoCode { get; set; }
        public Student Student { get; set; }

        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public ICollection<Cart_ShopItem> Cart_ShopItems { get; set; } = new HashSet<Cart_ShopItem>();
    }
}
