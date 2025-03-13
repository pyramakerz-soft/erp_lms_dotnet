using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class School_GetDTO
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public string SchoolTypeName { get; set; }
        public string? ReportHeaderOneEn { get; set; }
        public string? ReportHeaderOneAr { get; set; }
        public string? ReportHeaderTwoEn { get; set; }
        public string? ReportHeaderTwoAr { get; set; }
        public string? ReportImage { get; set; }
        public long SchoolTypeID { get; set; } 
        public string? VatNumber { get; set; }
        public long? InsertedByUserId { get; set; }
    }
}
