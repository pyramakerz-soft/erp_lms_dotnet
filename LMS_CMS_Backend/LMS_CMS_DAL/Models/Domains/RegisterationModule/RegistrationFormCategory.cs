using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.RegisterationModule
{
    public class RegistrationFormCategory : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [ForeignKey("RegistrationForm")]
        public long RegistrationFormID { get; set; }
        public RegistrationForm RegistrationForm { get; set; }
        [ForeignKey("RegistrationCategory")]
        public long RegistrationCategoryID { get; set; }
        public RegistrationCategory RegistrationCategory { get; set; }
    }
}
