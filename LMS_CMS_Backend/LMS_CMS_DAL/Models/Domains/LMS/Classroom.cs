﻿using System;
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


        [ForeignKey("HomeroomTeacher")]
        public long? HomeroomTeacherID { get; set; }
        public Employee HomeroomTeacher { get; set; }


        [ForeignKey("Grade")]
        public long GradeID { get; set; }
        public Grade Grade { get; set; }


        [ForeignKey("AcademicYear")]
        public long AcademicYearID { get; set; }
        public AcademicYear AcademicYear { get; set; }

        public ICollection<StudentAcademicYear> StudentAcademicYears { get; set; } = new HashSet<StudentAcademicYear>();
        public ICollection<EvaluationEmployee> EvaluationEmployees { get; set; } = new HashSet<EvaluationEmployee>();
        public ICollection<LessonResourceClassroom> LessonResourceClassrooms { get; set; } = new HashSet<LessonResourceClassroom>();
        public ICollection<LessonLive> LessonLives { get; set; } = new HashSet<LessonLive>();
    }
}
