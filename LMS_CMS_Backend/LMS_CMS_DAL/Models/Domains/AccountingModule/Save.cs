﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS_CMS_DAL.Models.Domains.Inventory;

namespace LMS_CMS_DAL.Models.Domains.AccountingModule
{
    public class Save : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [ForeignKey("AccountNumber")]
        public long AccountNumberID { get; set; }

        public AccountingTreeChart AccountNumber { get; set; }
        public ICollection<InventoryMaster> InventoryMasters { get; set; } = new HashSet<InventoryMaster>();

    }
}
