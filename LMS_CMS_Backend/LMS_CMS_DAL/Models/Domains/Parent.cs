using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_DAL.Models.Domains.RegisterationModule;

namespace LMS_CMS_DAL.Models.Domains
{
    public class Parent : AuditableEntity
    {
        [Key]
        public long ID { get; set; }

        [Required(ErrorMessage = "User_Name is required")]
        [StringLength(100, ErrorMessage = "Username cannot be longer than 100 characters.")]
        public string User_Name { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string en_name { get; set; }

        [StringLength(100, ErrorMessage = "لا يمكن أن يكون الاسم أطول من 100 حرف")]
        public string ar_name { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        public string? ConfirmationCode { get; set; }
        public string? PassportNo { get; set; }
        public string? PassportNoExpiredDate { get; set; }
        public string? NationalID { get; set; }
        public string? NationalIDExpiredDate { get; set; }
        public string? Qualification { get; set; } 
        public string? Profession { get; set; }
        public string? WorkPlace { get; set; }

        public ICollection<Student> Students { get; set; } = new HashSet<Student>();
        public ICollection<RegisterationFormParent> RegisterationFormParents { get; set; } = new HashSet<RegisterationFormParent>();
    }
}
