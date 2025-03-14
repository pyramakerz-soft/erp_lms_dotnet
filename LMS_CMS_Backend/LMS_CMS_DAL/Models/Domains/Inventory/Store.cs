﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.Inventory
{
    public class Store : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        public ICollection<StoreCategories> StoreCategories { get; set; } = new HashSet<StoreCategories>();
        public ICollection<InventoryMaster> InventoryMasters { get; set; } = new HashSet<InventoryMaster>();
        public ICollection<Stocking> Stocking { get; set; } = new HashSet<Stocking>();
        public ICollection<InventoryMaster> InventoryMastersStoreToTransform { get; set; } = new HashSet<InventoryMaster>();

    }
}
