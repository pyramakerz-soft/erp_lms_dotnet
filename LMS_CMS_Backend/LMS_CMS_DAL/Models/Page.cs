﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models
{
    public class Page : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }
        [ForeignKey("Parent")]
        [Required]
        public long Page_ID { get; set; }
        public Page Parent { get; set; }
        public ICollection<Page> ChildPages { get; set; } = new List<Page>();
        public ICollection<Domain_Page_Detailes> Domain_Page_Detailes { get; set; } = new List<Domain_Page_Detailes>();
        public ICollection<Role_Detailes> Role_Detailes { get; set; } = new List<Role_Detailes>();



    }
}
