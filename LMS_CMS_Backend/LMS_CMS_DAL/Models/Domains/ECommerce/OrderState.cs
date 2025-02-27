using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.ECommerce
{
    public class OrderState
    {
        [Key]
        public long ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        public ICollection<Cart> Carts { get; set; } = new HashSet<Cart>();
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
