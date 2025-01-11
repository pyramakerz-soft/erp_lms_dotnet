using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Registration
{
    public class RegisterationFormSubmittionGetDTO
    {
        public long ID { get; set; }
        public string TextAnswer { get; set; }
        public long RegisterationFormParentID { get; set; }
        public string RegistrationFormParentName { get; set; }
        public long CategoryFieldID { get; set; }
        public string CategoryFieldName { get; set; }
        public int CategoryFieldOrderInForm { get; set; }
        public long RegistrationCategoryID { get; set; }
        public string RegistrationCategoryName { get; set; }
        public int RegistrationCategoryOrderInForm { get; set; }
        public long SelectedFieldOptionID { get; set; }
        public string SelectedFieldOptionName { get; set; }

    }
}
