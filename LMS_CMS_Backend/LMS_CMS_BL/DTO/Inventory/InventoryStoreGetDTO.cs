using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Inventory
{
    public class InventoryStoreGetDTO
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public List<InventoryCategoriesGetDto> StoreCategories { get; set; }

    }
}
