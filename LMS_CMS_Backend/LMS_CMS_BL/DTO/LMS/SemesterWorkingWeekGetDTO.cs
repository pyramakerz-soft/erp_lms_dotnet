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
    public class SemesterWorkingWeekGetDTO
    {
        public long ID { get; set; } 
        public string EnglishName { get; set; } 
        public string ArabicName { get; set; }
        public long SemesterID { get; set; }
        public string SemesterName { get; set; }

        public DateOnly DateFrom { get; set; }
        public DateOnly DateTo { get; set; }
        public long? InsertedByUserId { get; set; }

    }
}
