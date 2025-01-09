using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.RegisterationModule
{
    public class CategoryField : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "English Name is required")]
        [StringLength(100, ErrorMessage = "English Name cannot be longer than 100 characters.")]
        public string EnName { get; set; }
        [Required(ErrorMessage = "Arabic Name is required")]
        [StringLength(100, ErrorMessage = "Arabic Name cannot be longer than 100 characters.")]
        public string ArName { get; set; }
        public int OrderInForm { get; set; }
        public bool IsMandatory { get; set; }
        [ForeignKey("RegistrationCategory")]
        public long RegistrationCategoryID { get; set; }
        public RegistrationCategory RegistrationCategory { get; set; }
        [ForeignKey("FieldType")]
        public long FieldTypeID { get; set; }
        public FieldType FieldType { get; set; }
        public ICollection<RegisterationFormSubmittion> RegisterationFormSubmittions { get; set; } = new HashSet<RegisterationFormSubmittion>();
        public ICollection<FieldOption> FieldOptions { get; set; } = new HashSet<FieldOption>();
    }
}
