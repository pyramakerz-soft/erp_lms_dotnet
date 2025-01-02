using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class Semester_GetDTO
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public long AcademicYearID { get; set; }
        public string AcademicYearName { get; set; }
        public long? InsertedByUserId { get; set; }
    }
}
