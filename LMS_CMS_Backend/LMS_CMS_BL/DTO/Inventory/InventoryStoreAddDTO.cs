using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Inventory
{
    public class InventoryStoreAddDTO
    {
        public string Name { get; set; }
        public List<long> categoriesIds { get; set; }
    }
}
