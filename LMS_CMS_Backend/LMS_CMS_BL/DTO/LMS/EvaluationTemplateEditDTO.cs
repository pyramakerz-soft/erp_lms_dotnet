using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class EvaluationTemplateEditDTO
    {
        public long ID { get; set; }
        [Required(ErrorMessage = "English Title is required")]
        public string EnglishTitle { get; set; }
        [Required(ErrorMessage = "Arabic Title is required")]
        public string ArabicTitle { get; set; }
        public int Weight { get; set; }
        public int AfterCount { get; set; }
    }
}
