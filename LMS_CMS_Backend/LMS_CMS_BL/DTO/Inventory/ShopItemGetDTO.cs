using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS_CMS_DAL.Models.Domains.Inventory;

namespace LMS_CMS_BL.DTO.Inventory
{
    public class ShopItemGetDTO
    {
        public long ID { get; set; } 
        public string EnName { get; set; } 
        public string ArName { get; set; }
        public string EnDescription { get; set; }
        public string ArDescription { get; set; }
        public float PurchasePrice { get; set; }
        public float SalesPrice { get; set; }
        public float VATForForeign { get; set; }
        public int Limit { get; set; }
        public bool AvailableInShop { get; set; }
        public string MainImage { get; set; }
        public string OtherImage { get; set; }
        public long GenderID { get; set; }
        public string GenderName { get; set; }
        public long InventoryCategoriesID { get; set; }
        public long InventorySubCategoriesID { get; set; }
        public string InventorySubCategoriesName { get; set; }
        public long SchoolID { get; set; }
        public string SchoolName { get; set; }
        public long GradeID { get; set; }
        public string GradeName { get; set; }
        public List<ShopItemColorGetDTO> shopItemColors { get; set; }
        public List<ShopItemSizeGetDTO> shopItemSizes { get; set; }
        public string BarCode { get; set; }
        public long? InsertedByUserId { get; set; }
        public long CurrentStock { get; set; }
    }
}
