using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.Inventory
{
    public class InventorySubCategories : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [ForeignKey("InventoryCategories")]
        public long InventoryCategoriesID { get; set; }

        public InventoryCategories InventoryCategories { get; set; }
        public ICollection<ShopItem> ShopItem { get; set; } = new HashSet<ShopItem>(); 

    }
}
