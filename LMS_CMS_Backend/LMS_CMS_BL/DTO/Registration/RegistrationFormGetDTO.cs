using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Registration
{
    public class RegistrationFormGetDTO
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public List<RegistrationCategoryGetDTO> Categories { get; set; }
    }
}
