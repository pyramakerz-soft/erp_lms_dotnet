using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Inventory
{
    public class InventoryDetailsAddDTO
    {
        public int Quantity { get; set; }
        public float Price { get; set; }
        public float Amount { get; set; }
        public string Notes { get; set; }
        public long ShopItemID { get; set; }
        public long InventoryMasterId { get; set; }
    }
}
