using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.Inventory
{
    public class StockingDetails : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        public long CurrentStock { get; set; }
        public long ActualStock { get; set; }
        public long TheDifference { get; set; }

        [ForeignKey("ShopItem")]
        public long ShopItemID { get; set; }

        [ForeignKey("Stocking")]
        public long StockingId { get; set; }

        public ShopItem ShopItem { get; set; }
        public Stocking Stocking { get; set; }

    }
}
