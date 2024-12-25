using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO
{
    public class RoleAddDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Role cannot be longer than 100 characters.")]
        public string Name { get; set; }
        public long pageId { get; set; }
        public bool Allow_Edit { get; set; }
        public bool Allow_Delete { get; set; }
        public bool Allow_Edit_For_Others { get; set; }
        public bool Allow_Delete_For_Others { get; set; }
    }
}
