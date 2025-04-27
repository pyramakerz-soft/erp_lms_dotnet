using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class EvaluationEmployeeStudentBookCorrectionAddDTO
    {
        public int State { get; set; }
        public string? Note { get; set; }
        public long StudentID { get; set; }
    }
}
