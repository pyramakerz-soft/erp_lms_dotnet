using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Registration
{
    public class RegisterationFormSubmittionGetDTO
    {
        public string? TextAnswer { get; set; }
        public long CategoryFieldID { get; set; }
        public long? SelectedFieldOptionID { get; set; }
    }
}
