using LMS_CMS_DAL.Models.Domains;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.BusModule
{
    public class Bus : AuditableEntity
    {
        [Key]
        public long ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }
        public long Capacity { get; set; }
        public bool IsCapacityRestrict { get; set; }
        public int MorningPrice { get; set; }
        public int BackPrice { get; set; }
        public int TwoWaysPrice { get; set; }


        [ForeignKey("BusType")]
        public long? BusTypeID { get; set; }
        [ForeignKey("BusDistrict")]
        public long? BusDistrictID { get; set; }
        [ForeignKey("BusStatus")]
        public long? BusStatusID { get; set; }
        [ForeignKey("Driver")]
        public long? DriverID { get; set; }
        [ForeignKey("DriverAssistant")]
        public long? DriverAssistantID { get; set; }
        [ForeignKey("BusCompany")]
        public long? BusCompanyID { get; set; }


        public BusType? BusType { get; set; }
        public BusDistrict? BusDistrict { get; set; }
        public BusStatus? BusStatus { get; set; }

        public Employee? Driver { get; set; }
        public Employee? DriverAssistant { get; set; }

        public BusCompany? BusCompany { get; set; }
        public ICollection<BusStudent> BusStudents { get; set; } = new HashSet<BusStudent>();
    }
}
