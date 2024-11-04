using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO
{
    public class Role_Permissions_AddDTO
    {
        [Required(ErrorMessage = "Role ID is required")]
        public int Role_ID { get; set; }

        [Required(ErrorMessage = "Master ID is required")]
        public int Master_ID { get; set; }

        [Required(ErrorMessage = "Detailed ID is required")]
        public int Detailed_ID { get; set; }
    }
}
