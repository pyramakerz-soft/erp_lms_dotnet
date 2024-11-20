using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models
{
    public class BusStudent
    {
        public long Id { get; set; }
        [ForeignKey("Bus")]
        public long BusID { get; set; }
        [ForeignKey("Student")]
        public long StudentID { get; set; }
        [ForeignKey("BusCategory")]
        public long? BusCategoryID { get; set; }
        [ForeignKey("Semester")]
        public long? SemseterID { get; set; }
        public bool IsException { get; set; }
        public string ExceptionFromDate { get; set; }
        public string ExceptionToDate { get; set; }

        public Bus Bus { get; set; }
        public Student Student { get; set; }
        public BusCategory? BusCategory { get; set; }   
        public Semester? Semester { get; set; }



    }
}


