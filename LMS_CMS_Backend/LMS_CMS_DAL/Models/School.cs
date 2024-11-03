using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Domain")]
        public int Domain_id { get; set; }
        public Domain Domain { get; set; }
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
        public ICollection<School_Roles> School_Roles { get; set; } = new HashSet<School_Roles>();

    }
}
