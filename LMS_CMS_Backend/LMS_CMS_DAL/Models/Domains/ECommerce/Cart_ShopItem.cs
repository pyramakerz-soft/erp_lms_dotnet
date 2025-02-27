using LMS_CMS_DAL.Models.Domains.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.ECommerce
{
    public class Cart_ShopItem : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("ShopItem")]
        public long ShopItemID { get; set; }
        [ForeignKey("Cart")]
        public long CartID { get; set; }
        [ForeignKey("ShopItemSize")]
        public long? ShopItemSizeID { get; set; }
        [ForeignKey("ShopItemColor")]
        public long? ShopItemColorID { get; set; }

        public Cart Cart { get; set; }
        public ShopItem ShopItem { get; set; }
        public ShopItemColor ShopItemColor { get; set; }
        public ShopItemSize ShopItemSize { get; set; } 
    }
}
