using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO
{
    public class Page_GetDTO
    {
        public long ID { get; set; }
        public string en_name { get; set; }
        public string ar_name { get; set; }
        public long? Page_ID { get; set; }
        public List<Page_GetDTO> Children { get; set; } = new List<Page_GetDTO>();
    }
}
