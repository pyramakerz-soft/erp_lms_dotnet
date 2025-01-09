using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Registration
{
    public class RegistrationCategoryEditDTO
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "English Name is required")]
        [StringLength(100, ErrorMessage = "English Name cannot be longer than 100 characters.")]
        public string EnName { get; set; }
        [Required(ErrorMessage = "Arabic Name is required")]
        [StringLength(100, ErrorMessage = "Arabic Name cannot be longer than 100 characters.")]
        public string ArName { get; set; }
        public int OrderInForm { get; set; }
    }
}
