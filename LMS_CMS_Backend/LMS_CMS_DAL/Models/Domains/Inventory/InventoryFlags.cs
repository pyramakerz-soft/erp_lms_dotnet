using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.Inventory
{
    public class InventoryFlags 
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string enName { get; set; }
        public string arName { get; set; }
        public string? en_Title { get; set; }
        public string? ar_Title { get; set; }
        public int ItemInOut { get; set; }
        public int FlagValue { get; set; }
        public ICollection<InventoryMaster> InventoryMaster { get; set; } = new HashSet<InventoryMaster>();

    }
}
