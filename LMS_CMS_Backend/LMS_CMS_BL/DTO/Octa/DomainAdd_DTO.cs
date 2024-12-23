using LMS_CMS_DAL.Models.Octa;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Octa
{
    public class DomainAdd_DTO
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string DomainName { get; set; }
        public long[] Pages { get; set; }
    }
}
