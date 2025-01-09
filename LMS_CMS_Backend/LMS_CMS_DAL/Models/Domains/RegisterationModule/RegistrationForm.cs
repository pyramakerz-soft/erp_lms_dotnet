using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.RegisterationModule
{
    public class RegistrationForm : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }
        public ICollection<RegisterationFormParent> RegisterationFormParents { get; set; } = new HashSet<RegisterationFormParent>();
        public ICollection<RegistrationCategory> RegistrationCategorys { get; set; } = new HashSet<RegistrationCategory>();
    }
}
