using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.ECommerce
{
    public class CartGetDTO
    {
        public long ID { get; set; }
        public float TotalPrice { get; set; }
        public long PromoCodeID { get; set; } 
        public string PromoCodeName { get; set; } 
        public int Percentage { get; set; } 
        public long StudentID { get; set; }
        public List<Cart_ShopItemGetDTO> Cart_ShopItems { get; set; }
    }
}
