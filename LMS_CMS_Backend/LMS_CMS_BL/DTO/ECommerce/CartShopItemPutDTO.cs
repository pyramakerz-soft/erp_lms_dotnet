using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.ECommerce
{
    public class CartShopItemPutDTO
    {
        public long ID { get; set; }
        public int Quantity { get; set; }
        public long CartID { get; set; }  
    }
}
