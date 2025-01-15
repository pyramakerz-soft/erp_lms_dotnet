using LMS_CMS_DAL.Models.Domains.RegisterationModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Registration
{
    public class RegisterationFormTestGetDTO
    {
        public long ID { get; set; }
        public double Mark { get; set; }
        public double TotalMark { get; set; }
        public bool VisibleToParent { get; set; }
        public long TestID { get; set; }
        public string TestName { get; set; }
        public long StateID { get; set; }
        public string StateName { get; set; }
        public string SubjectName { get; set; }
        public long RegisterationFormParentID { get; set; }
        public string StudentName { get; set; }
        public long? InsertedByUserId { get; set; }
    }
}
