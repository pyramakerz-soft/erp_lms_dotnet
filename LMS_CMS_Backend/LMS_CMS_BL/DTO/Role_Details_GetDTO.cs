using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO
{
    public class Role_Details_GetDTO
    {
        public long ID { get; set; }
        public string en_name { get; set; }
        public string ar_name { get; set; }
        public long? Page_ID { get; set; }
        public bool Allow_Edit { get; set; }
        public bool Allow_Delete { get; set; }

        public List<Role_Details_GetDTO> Children { get; set; } = new List<Role_Details_GetDTO>();
    }
}
