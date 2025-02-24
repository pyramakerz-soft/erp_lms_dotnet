using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.Inventory
{
    public class InventoryDetails : AuditableEntity
    {
        [Key]
        public long ID { get; set; } 
        //public string BarCode { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public float TotalPrice { get; set; }
        public float Amount { get; set; }
        public string Notes { get; set; }

        [ForeignKey("ShopItem")]
        public long ShopItemID { get; set; }
        
        [ForeignKey("InventoryMaster")]
        public long InventoryMasterId { get; set; }

        public ShopItem ShopItem { get; set; }
        public InventoryMaster InventoryMaster { get; set; }

        public ICollection<SalesItemAttachment> SalesItemAttachment { get; set; } = new HashSet<SalesItemAttachment>();

    }
}
