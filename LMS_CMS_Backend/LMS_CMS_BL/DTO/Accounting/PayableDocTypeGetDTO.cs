using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Accounting
{
    public class PayableDocTypeGetDTO
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public long InsertedByUserId { get; set; }
    }
}
