using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS_CMS_DAL.Models.Domains.ECommerce;

namespace LMS_CMS_DAL.Models.Domains.Inventory
{
    public class ShopItemColor : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [ForeignKey("ShopItem")]
        public long ShopItemID { get; set; }

        public ShopItem ShopItem { get; set; }

        public ICollection<Cart_ShopItem> Cart_ShopItems { get; set; } = new HashSet<Cart_ShopItem>();
    }
}
