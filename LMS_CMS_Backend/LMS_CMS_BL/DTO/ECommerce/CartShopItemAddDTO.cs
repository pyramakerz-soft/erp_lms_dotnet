using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.ECommerce
{
    public class CartShopItemAddDTO
    {
        public int Quantity { get; set; }
        public long? CartID { get; set; }
        public long ShopItemID { get; set; }
        public long? ShopItemSizeID { get; set; }
        public long? ShopItemColorID { get; set; }
        public long StudentID { get; set; }
    }
}
