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
        public long? InsertedByOctaId { get; set; }
        public long? InsertedByUserId { get; set; }
        public long? UpdatedByOctaId { get; set; }
        public long? UpdatedByUserId { get; set; }
        public long? DeletedByOctaId { get; set; }
        public long? DeletedByUserId { get; set; }
    }
}
