using LMS_CMS_DAL.Models.Domains.ECommerce;
using LMS_CMS_DAL.Models.Domains.LMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.Inventory
{
    public class ShopItem : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string EnName { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Arabic Name cannot be longer than 100 characters.")]
        public string ArName { get; set; }
        public string EnDescription { get; set; }
        public string ArDescription { get; set; }
        public float PurchasePrice { get; set; }
        public float SalesPrice { get; set; }
        public float? VATForForeign { get; set; }
        public int Limit { get; set; }
        public bool AvailableInShop { get; set; }
        public string? MainImage { get; set; }
        public string? OtherImage { get; set; }
        public string BarCode { get; set; }

        [ForeignKey("Gender")]
        public long GenderID { get; set; }
        
        [ForeignKey("InventorySubCategories")]
        public long InventorySubCategoriesID { get; set; }

        [ForeignKey("School")]
        public long SchoolID { get; set; }
        
        [ForeignKey("Grade")]
        public long GradeID { get; set; }

        public Gender Gender { get; set; }
        public InventorySubCategories InventorySubCategories { get; set; }
        public School School { get; set; }
        public Grade Grade { get; set; }

        public ICollection<InventoryDetails> InventoryDetails { get; set; } = new HashSet<InventoryDetails>();
        public ICollection<ShopItemColor> ShopItemColor { get; set; } = new HashSet<ShopItemColor>();
        public ICollection<ShopItemSize> ShopItemSize { get; set; } = new HashSet<ShopItemSize>();
        public ICollection<Cart_ShopItem> Cart_ShopItems { get; set; } = new HashSet<Cart_ShopItem>();
    }
}
