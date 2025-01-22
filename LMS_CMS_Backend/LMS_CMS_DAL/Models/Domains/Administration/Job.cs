using LMS_CMS_DAL.Models.Domains.AccountingModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.Administration
{
    public class Job : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [ForeignKey("JobCategory")]
        public long JobCategoryID { get; set; }
        public JobCategory JobCategory { get; set; }


        public ICollection<Employee> Employees { get; set; } = new List<Employee>();

    }
}
