using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class SectionWithGradeAndCountsDTO
    {
        public long ID { get; set; } 
        public string Name { get; set; }
        public List<GradeWithStudentClassCountDTO> GradeWithStudentClassCount { get; set; }
        public GradeWithStudentClassCountDTO TotalCounts { get; set; }
    }
}
