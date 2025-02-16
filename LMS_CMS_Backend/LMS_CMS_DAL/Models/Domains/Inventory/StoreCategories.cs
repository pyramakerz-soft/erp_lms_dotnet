using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.Inventory
{
    public class StoreCategories : AuditableEntity
    {
        [Key]
        public long ID { get; set; } 

        [ForeignKey("InventoryCategories")]
        public long InventoryCategoriesID { get; set; }

        [ForeignKey("Store")]
        public long StoreID { get; set; }

        public InventoryCategories InventoryCategories { get; set; }
        public Store Store { get; set; }
    }
}
