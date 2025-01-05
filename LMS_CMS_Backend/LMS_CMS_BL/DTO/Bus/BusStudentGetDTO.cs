using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Bus
{
    public class BusStudentGetDTO
    {
        public long ID { get; set; }
        public long BusID { get; set; }
        public string BusName { get; set; }
        public long StudentID { get; set; }
        public string StudentName { get; set; }
        public long BusCategoryID { get; set; }
        public string BusCategoryName { get; set; }
        public long SemseterID { get; set; }
        public string SemseterName { get; set; }
        public bool IsException { get; set; }
        public string ExceptionFromDate { get; set; }
        public string ExceptionToDate { get; set; }
        public long? InsertedByOctaId { get; set; }
        public long? InsertedByUserId { get; set; }
        public long? UpdatedByOctaId { get; set; }
        public long? UpdatedByUserId { get; set; }
        public long? DeletedByOctaId { get; set; }
        public long? DeletedByUserId { get; set; }
        public long SchoolID { get; set; }
        public string SchoolName { get; set; }
        public long SectionID { get; set; }
        public string SectionName { get; set; }
        public long GradeID { get; set; }
        public string GradeName { get; set; }
        public long ClassID { get; set; }
        public string ClassName { get; set; }
        public string StudentAcademicYear { get; set; }
    }
}
