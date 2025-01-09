using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.RegisterationModule
{
    public class RegisterationFormSubmittion : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        public string Answer { get; set; }
        [ForeignKey("RegisterationFormParent")]
        public long RegisterationFormParentID { get; set; }
        public RegisterationFormParent RegisterationFormParent { get; set; }
        [ForeignKey("CategoryField")]
        public long CategoryFieldID { get; set; }
        public CategoryField CategoryField { get; set; }
    }
}
