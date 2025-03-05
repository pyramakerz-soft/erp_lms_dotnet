using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.ECommerce
{
    public class Cart_ShopItemGetDTO
    {
        public long ID { get; set; }
        public int Quantity { get; set; }
        public long ShopItemID { get; set; }
        public string ShopItemEnName { get; set; }
        public string ShopItemArName { get; set; }
        public int ShopItemLimit { get; set; }
        public float SalesPrice { get; set; }
        public float VATForForeign { get; set; }
        public string MainImage { get; set; }
        public long CartID { get; set; }
        public long ShopItemSizeID { get; set; }
        public string ShopItemSizeName { get; set; }
        public long ShopItemColorID { get; set; }
        public string ShopItemColorName { get; set; }
    }
}
