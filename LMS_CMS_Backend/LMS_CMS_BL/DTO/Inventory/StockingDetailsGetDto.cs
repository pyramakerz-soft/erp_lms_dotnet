using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Inventory
{
    public class StockingDetailsGetDto
    {
        public long? ID { get; set; }
        public long CurrentStock { get; set; }
        public long ActualStock { get; set; }
        public long TheDifference { get; set; }
        public long ShopItemID { get; set; }
        public long StockingId { get; set; }
        public string? BarCode { get; set; }
        public string? ShopItemName { get; set; }
        public long? InsertedByUserId { get; set; }

    }
}
