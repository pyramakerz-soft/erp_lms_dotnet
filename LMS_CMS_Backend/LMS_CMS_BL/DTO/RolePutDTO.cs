using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO
{
    public class RolePutDTO
    {
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Role cannot be longer than 100 characters.")]
        public string Name { get; set; }
        public List<RolePageDTO> Pages { get; set; }
    }
}
