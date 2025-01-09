using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.RegisterationModule
{
    public class RegistrationCategory : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "English Name is required")]
        [StringLength(100, ErrorMessage = "English Name cannot be longer than 100 characters.")]
        public string EnName { get; set; }
        [Required(ErrorMessage = "Arabic Name is required")]
        [StringLength(100, ErrorMessage = "Arabic Name cannot be longer than 100 characters.")]
        public string ArName { get; set; }
        public string OrderInForm { get; set; }
        [ForeignKey("RegistrationForm")]
        public long RegistrationFormID { get; set; }
        public RegistrationForm RegistrationForm { get; set; }
        public ICollection<CategoryField> CategoryFields { get; set; } = new HashSet<CategoryField>();
        public ICollection<RegistrationFormCategory> RegistrationFormCategories { get; set; } = new HashSet<RegistrationFormCategory>();
    }
}
