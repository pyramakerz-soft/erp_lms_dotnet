using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class SubjectAddDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string en_name { get; set; }
        [Required(ErrorMessage = "الاسم مطلوب")]
        [StringLength(100, ErrorMessage = "لا يمكن أن يكون الاسم أطول من 100 حرف")]
        public string ar_name { get; set; }
        public int OrderInCertificate { get; set; }
        public double CreditHours { get; set; }
        public string SubjectCode { get; set; }
        public int PassByDegree { get; set; }
        public int TotalMark { get; set; }
        public bool HideFromGradeReport { get; set; }
        public IFormFile IconFile { get; set; }
        public int NumberOfSessionPerWeek { get; set; }
        public long GradeID { get; set; }
        public long SubjectCategoryID { get; set; }
    }
}
