using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_DAL.Models.Domains.LMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Domains.LMS
{
    public class StudentAcademicYear : AuditableEntity
    {
        [Key]
        public long ID { get; set; }
        [ForeignKey("Student")]
        public long StudentID { get; set; }
        [ForeignKey("School")]
        public long SchoolID { get; set; }
        [ForeignKey("Class")]
        public long ClassID { get; set; }
        [ForeignKey("Grade")]
        public long GradeID { get; set; }
        public Student Student { get; set; }
        public School School { get; set; }
        public Classroom Classroom { get; set; }
        public Grade Grade { get; set; }
    }
}

