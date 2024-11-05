using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO
{
    public class Employee_Role_AddDTO
    {
        [Required(ErrorMessage = "Role ID is required")]
        public int Role_ID { get; set; }

        [Required(ErrorMessage = "Employee ID is required")]
        public int Employee_ID { get; set; }
    }
}
