using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Bus
{
    public class Bus_GetDTO
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public long Capacity { get; set; }
        public bool IsCapacityRestricted { get; set; }
        public string? BusTypeName { get; set; }
        public string? BusRestrictName { get; set; }
        public string? BusStatusName { get; set; }
        public string? DriverName { get; set; }
        public string? DriverAssistantName { get; set; }
        public string? BusCompanyName { get; set; }
    }
}
