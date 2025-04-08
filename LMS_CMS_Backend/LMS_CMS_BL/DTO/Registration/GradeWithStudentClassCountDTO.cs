using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Registration
{
    public class GradeWithStudentClassCountDTO
    {
        public long ID { get; set; } 
        public string Name { get; set; }
        public int ClassCount { get; set; }
        public int SaudiCount { get; set; }
        public int NonSaudiCount { get; set; }
        public int StudentCount { get; set; }
        public int StudentsAssignedToNoorCount { get; set; }
    }
}
