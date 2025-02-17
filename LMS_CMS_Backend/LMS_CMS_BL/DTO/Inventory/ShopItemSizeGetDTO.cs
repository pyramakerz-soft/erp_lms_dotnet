using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Inventory
{
    public class ShopItemSizeGetDTO
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public long ShopItemID { get; set; }
        public long ShopItemName { get; set; }
    }
}
