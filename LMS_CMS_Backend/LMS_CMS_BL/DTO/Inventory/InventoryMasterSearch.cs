using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Inventory
{
    public class InventoryMasterSearch
    {
        public long storeId { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public List<long> FlagIds { get; set; }
        //public List<long>? ShopItemsIds { get; set; }

    }
}
