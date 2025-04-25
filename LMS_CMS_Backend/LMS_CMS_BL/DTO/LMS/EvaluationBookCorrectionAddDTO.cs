using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class EvaluationBookCorrectionAddDTO
    {

        [Required(ErrorMessage = "English Name is required")]
        public string EnglishName { get; set; }
        [Required(ErrorMessage = "Arabic Name is required")]
        public string ArabicName { get; set; }
    }
}
