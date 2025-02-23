using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Inventory
{
    public class InventoryDetailsGetDTO
    {
        public long ID { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public float Amount { get; set; }
        public string Notes { get; set; }
        public long ShopItemID { get; set; }
        public long InventoryMasterId { get; set; }
        public string? ShopItemName { get; set; }
        public string? SalesName { get; set; }
    }
}
