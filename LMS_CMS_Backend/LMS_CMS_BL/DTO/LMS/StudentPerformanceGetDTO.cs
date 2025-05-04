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
    public class StudentPerformanceGetDTO
    {
        public long ID { get; set; }
        public long StudentID { get; set; }
        public string StudentName { get; set; }
        public long PerformanceTypeID { get; set; }
        public string PerformanceTypeName { get; set; }
        public long SubjectID { get; set; }
        public string SubjectName { get; set; }
    }
}
