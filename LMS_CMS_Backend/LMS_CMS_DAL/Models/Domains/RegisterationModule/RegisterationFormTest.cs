using LMS_CMS_DAL.Models.Domains.LMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.RegisterationModule
{
    public class RegisterationFormTest
    {
        [Key]
        public long ID { get; set; }
        public decimal Mark { get; set; }
        public bool VisibleToParent { get; set; }

        [ForeignKey("Test")]
        [Required]
        public long TestID { get; set; }
        public Test Test { get; set; }

        [ForeignKey("TestState")]
        [Required]
        public long StateID { get; set; }
        public TestState TestState { get; set; }
        //FK
        //RegisterationFormParentID
    }
}
