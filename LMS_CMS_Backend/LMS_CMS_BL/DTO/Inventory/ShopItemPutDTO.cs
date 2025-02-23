using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Inventory
{
    public class ShopItemPutDTO
    {
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
        public float VATForForeign { get; set; }
        public int Limit { get; set; }
        public bool AvailableInShop { get; set; }
        public long GenderID { get; set; }
        public long InventorySubCategoriesID { get; set; }
        public long SchoolID { get; set; }
        public long GradeID { get; set; }
        public IFormFile? MainImageFile { get; set; }
        public IFormFile? OtherImageFile { get; set; }
        public string? MainImage { get; set; }
        public string? OtherImage { get; set; }
        public string[]? ShopItemColors { get; set; }
        public string[]? ShopItemSizes { get; set; }
        public string BarCode { get; set; }
    }
}
