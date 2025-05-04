using LMS_CMS_DAL.Models.Domains.LMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class StudentMedalGetDTO
    {
        public long ID { get; set; }
        public long StudentID { get; set; }
        public string StudentName { get; set; }
        public long MedalID { get; set; }
        public string MedalName { get; set; }
        public string ImageLink { get; set; }

    }
}
