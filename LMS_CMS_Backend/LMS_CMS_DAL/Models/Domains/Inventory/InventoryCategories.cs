﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.Inventory
{
    public class InventoryCategories : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        public ICollection<InventorySubCategories> InventorySubCategories { get; set; } = new HashSet<InventorySubCategories>();
        public ICollection<StoreCategories> StoreCategories { get; set; } = new HashSet<StoreCategories>();
    }
}
