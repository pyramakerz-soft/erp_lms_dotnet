using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Octa
{
    public class OctaAddDTO
    {
        public string User_Name { get; set; }
        [Required(ErrorMessage = "Arabic_Name is required")]
        [StringLength(100, ErrorMessage = "Arabic name cannot be longer than 100 characters.")]
        public string Arabic_Name { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        public string Password { get; set; }
    }
}
