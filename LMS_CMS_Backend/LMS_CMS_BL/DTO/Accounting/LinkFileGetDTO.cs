using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Accounting
{
    public class LinkFileGetDTO
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string ArName { get; set; }
    }
}
