using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS_CMS_DAL.Models.Domains;

namespace LMS_CMS_DAL.Models.Domains.BusModule
{
    public class BusCompany : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        public ICollection<Bus> Buses { get; set; } = new HashSet<Bus>();
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();


    }
}
