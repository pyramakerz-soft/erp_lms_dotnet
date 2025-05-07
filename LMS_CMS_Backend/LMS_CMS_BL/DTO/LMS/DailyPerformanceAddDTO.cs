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
    public class DailyPerformanceAddDTO
    {
        public string? Comment { get; set; }
        public long SubjectID { get; set; }
        public long StudentID { get; set; }
        public List<StudentPerformanceAddDTO> StudentPerformance { get; set; } 
    }
}
