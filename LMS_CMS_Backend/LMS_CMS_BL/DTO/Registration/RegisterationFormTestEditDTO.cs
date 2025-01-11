using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Registration
{
    public class RegisterationFormTestEditDTO
    {
        public long ID { get; set; }
        public double Mark { get; set; }
        public bool VisibleToParent { get; set; }
        public long StateID { get; set; }
    }
}
