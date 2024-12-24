using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class FloorGetDTO
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public long BuildingID { get; set; }
        public string BuildingName { get; set; }
    }
}
