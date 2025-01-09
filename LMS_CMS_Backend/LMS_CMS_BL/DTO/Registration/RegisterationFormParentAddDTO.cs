using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Registration
{
    public class RegisterationFormParentAddDTO
    {
        public long RegistrationFormID { get; set; }
        public List<RegisterationFormSubmittionGetDTO> RegisterationFormSubmittions { get; set; }
    }
}
