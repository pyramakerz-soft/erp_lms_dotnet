using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO
{
    public class RolePageDTO
    {
        public long pageId { get; set; }
        public bool Allow_Edit { get; set; }
        public bool Allow_Delete { get; set; }
        public bool Allow_Edit_For_Others { get; set; }
        public bool Allow_Delete_For_Others { get; set; }
    }
}
