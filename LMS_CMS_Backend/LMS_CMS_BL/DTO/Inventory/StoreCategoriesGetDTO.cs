using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Inventory
{
    public class StoreCategoriesGetDTO
    {
        public long ID { get; set; }
        public long InventoryCategoriesID { get; set; }
        public long StoreID { get; set; }
        public string InventoryCategoriesName { get; set; }
        public string StoreName { get; set; }
        public long? InsertedByUserId { get; set; }

    }
}
