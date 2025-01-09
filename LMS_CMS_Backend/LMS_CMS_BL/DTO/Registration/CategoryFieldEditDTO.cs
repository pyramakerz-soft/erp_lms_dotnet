using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Registration
{
    public class CategoryFieldEditDTO
    {
        public long ID { get; set; }
        public string EnName { get; set; }
        public string ArName { get; set; }
        public int OrderInForm { get; set; }
        public bool IsMandatory { get; set; }
        public long FieldTypeID { get; set; }
        public long RegistrationCategoryID { get; set; }
        public List<string>? Options { get; set; }
    }
}
