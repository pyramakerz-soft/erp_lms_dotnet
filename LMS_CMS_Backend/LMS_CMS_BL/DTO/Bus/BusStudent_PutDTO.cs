using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Bus
{
    public class BusStudent_PutDTO
    {
        public long ID { get; set; }
        public long BusID { get; set; }
        public long StudentID { get; set; }
        public long BusCategoryID { get; set; }
        public long SemseterID { get; set; }
        public bool IsException { get; set; }
        public string? ExceptionFromDate { get; set; }
        public string? ExceptionToDate { get; set; }
    }
}
