﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.RegisterationModule
{
    public class FieldOption : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }
        [ForeignKey("CategoryField")]
        public long CategoryFieldID { get; set; }
        public CategoryField CategoryField { get; set; }
        public ICollection<RegisterationFormSubmittion> RegisterationFormSubmittions { get; set; } = new HashSet<RegisterationFormSubmittion>();
    }
}
