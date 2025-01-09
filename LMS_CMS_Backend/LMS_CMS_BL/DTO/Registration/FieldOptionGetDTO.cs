using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Registration
{
    public class FieldOptionGetDTO
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public long CategoryFieldID { get; set; }
        public string CategoryFieldName { get; set; }
    }
}
