using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.LMS
{
    public class Classroom : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }
        public int Number { get; set; }
        [ForeignKey("Floor")]
        public long FloorID { get; set; }
        public Floor Floor { get; set; }
        [ForeignKey("Grade")]
        public long GradeID { get; set; }
        public Grade Grade { get; set; }

    }
}
