using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Bus
{
    public class Bus_AddDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }
        public long Capacity { get; set; }
        public bool IsCapacityRestricted { get; set; }
        public long? BusTypeID { get; set; }
        public long? BusRestrictID { get; set; }
        public long? BusStatusID { get; set; }
        public long? DriverID { get; set; }
        public long? DriverAssistantID { get; set; }
        public long? BusCompanyID { get; set; }
    }
}
