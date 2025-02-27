using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Inventory
{
    public class InventoryFlagGetDTO
    {
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string enName { get; set; }
        public string arName { get; set; }
        public string? Title { get; set; }
        public string? arTitle { get; set; }
        public int ItemInOut { get; set; }
        public int FlagValue { get; set; }
    }
}
