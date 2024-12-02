using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS_CMS_DAL.Models.Domains.BusModule;


namespace LMS_CMS_DAL.Models.Domains
{
    public class Domain : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        public ICollection<School> Schools { get; set; } = new HashSet<School>();
        public ICollection<Role> Roles { get; set; } = new HashSet<Role>();
        public ICollection<Employee> Employess { get; set; } = new HashSet<Employee>();
        public ICollection<Domain_Page_Detailes> Domain_Page_Detailes { get; set; } = new HashSet<Domain_Page_Detailes>();
        public ICollection<BusType> BusTypes { get; set; } = new HashSet<BusType>();
        public ICollection<BusRestrict> BusRestricts { get; set; } = new HashSet<BusRestrict>();
        public ICollection<BusStatus> BusStatus { get; set; } = new HashSet<BusStatus>();
        public ICollection<BusCategory> BusCategories { get; set; } = new HashSet<BusCategory>();
        public ICollection<BusCompany> BusCompanies { get; set; } = new HashSet<BusCompany>();
        public ICollection<Bus> Buses { get; set; } = new HashSet<Bus>();




    }
}
