using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "User_Name is required")]
        [StringLength(100, ErrorMessage = "Username cannot be longer than 100 characters.")]
        public string User_Name { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [ForeignKey("School")]
        public int School_id { get; set; }

        public School School { get; set; }

        public  ICollection<Employee_Role> Employee_Roles { get; set; } = new HashSet<Employee_Role>();

    }
}
