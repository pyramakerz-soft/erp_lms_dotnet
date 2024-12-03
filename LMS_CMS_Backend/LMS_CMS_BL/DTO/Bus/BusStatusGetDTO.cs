using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Bus
{
    public class BusStatusGetDTO
    {
        public long ID { get; set; }

        public string Name { get; set; }
        public long? DeletedByUserId { get; set; }
        public long? InsertedByUserId { get; set; }
        public long? UpdatedByUserId { get; set; }
    }
}
