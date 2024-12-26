using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Violation
{
    public class ViolationAddDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }
    }
}
